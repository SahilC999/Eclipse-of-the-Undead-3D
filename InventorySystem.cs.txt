using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Manages player's inventory including weapons, ammo, medkits, and grenades.
/// Attach to Player GameObject.
/// </summary>
public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance;

    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    public Text inventoryListText;
    public Text selectedItemText;

    [Header("Inventory Settings")]
    public int maxItems = 10;
    private List<Item> inventoryItems = new List<Item>();
    private int selectedIndex = 0;

    [Header("References")]
    public PlayerCombat playerCombat;
    public PlayerHealth playerHealth;

    // Item types
    public enum ItemType { Ammo, Medkit, Grenade, Weapon }

    [System.Serializable]
    public class Item
    {
        public string itemName;
        public ItemType type;
        public int quantity;
    }

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (inventoryItems.Count > 0 && inventoryPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) Navigate(-1);
            if (Input.GetKeyDown(KeyCode.DownArrow)) Navigate(1);
            if (Input.GetKeyDown(KeyCode.Return)) UseSelectedItem();
        }
    }

    // Toggle inventory visibility
    void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        UpdateInventoryUI();
    }

    // Navigate inventory items
    void Navigate(int direction)
    {
        selectedIndex += direction;
        if (selectedIndex < 0) selectedIndex = inventoryItems.Count - 1;
        if (selectedIndex >= inventoryItems.Count) selectedIndex = 0;
        HighlightSelectedItem();
    }

    // Highlight selected item
    void HighlightSelectedItem()
    {
        if (inventoryItems.Count == 0) return;
        selectedItemText.text = "Selected: " + inventoryItems[selectedIndex].itemName;
    }

    // Add item to inventory
    public void AddItem(string name, ItemType type, int quantity)
    {
        // If inventory full
        if (inventoryItems.Count >= maxItems)
        {
            Debug.Log("Inventory Full!");
            return;
        }

        // Check if item already exists
        Item existing = inventoryItems.Find(i => i.itemName == name);
        if (existing != null)
        {
            existing.quantity += quantity;
        }
        else
        {
            Item newItem = new Item { itemName = name, type = type, quantity = quantity };
            inventoryItems.Add(newItem);
        }

        UpdateInventoryUI();
    }

    // Use selected item
    void UseSelectedItem()
    {
        if (inventoryItems.Count == 0) return;

        Item item = inventoryItems[selectedIndex];
        switch (item.type)
        {
            case ItemType.Ammo:
                if (playerCombat) playerCombat.currentAmmo += item.quantity;
                Debug.Log("Used Ammo Pack: +" + item.quantity);
                break;

            case ItemType.Medkit:
                if (playerHealth) playerHealth.Heal(item.quantity);
                Debug.Log("Used Medkit: +" + item.quantity + " HP");
                break;

            case ItemType.Grenade:
                Debug.Log("Used Grenade!");
                // Grenade throw logic can be added here
                break;

            case ItemType.Weapon:
                Debug.Log("Picked up new weapon: " + item.itemName);
                // Integration with PlayerCombat weapon switching
                break;
        }

        // Reduce quantity or remove item
        item.quantity--;
        if (item.quantity <= 0)
        {
            inventoryItems.RemoveAt(selectedIndex);
            selectedIndex = 0;
        }

        UpdateInventoryUI();
    }

    // Update inventory UI list
    void UpdateInventoryUI()
    {
        if (!inventoryListText) return;

        inventoryListText.text = "";
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            string line = inventoryItems[i].itemName + " x" + inventoryItems[i].quantity;
            if (i == selectedIndex) line = "> " + line + " <";
            inventoryListText.text += line + "\n";
        }
        HighlightSelectedItem();
    }

    // Remove item manually
    public void RemoveItem(string name)
    {
        Item item = inventoryItems.Find(i => i.itemName == name);
        if (item != null)
        {
            inventoryItems.Remove(item);
            UpdateInventoryUI();
        }
    }
}
