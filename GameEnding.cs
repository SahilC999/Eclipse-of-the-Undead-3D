using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the game ending sequence when conditions are met (all survivors safe + car escape).
/// Attach this to an empty GameObject in the scene.
/// </summary>
public class GameEnding : MonoBehaviour
{
    public static GameEnding instance;

    [Header("Ending Settings")]
    public GameObject victoryUI;
    public AudioSource victoryMusic;
    public float delayBeforeUI = 2f;
    public float delayBeforeMenu = 8f;

    [Header("Optional Camera")]
    public Camera cinematicCamera;
    public Transform cinematicView;
    private bool endingTriggered = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (victoryUI) victoryUI.SetActive(false);
    }

    // Called by CarSystem when player starts the escape
    public void EndGameWin()
    {
        if (endingTriggered) return;
        endingTriggered = true;

        Debug.Log("Game Ending Triggered: Player Escaped with Survivors!");

        // Stop enemy spawns
        WaveSpawner spawner = FindObjectOfType<WaveSpawner>();
        if (spawner) spawner.enabled = false;

        // Play music
        victoryMusic?.Play();

        // Switch to cinematic view if available
        if (cinematicCamera && cinematicView)
        {
            StartCoroutine(SwitchToCinematicView());
        }

        // Show victory UI after a delay
        Invoke("ShowVictoryUI", delayBeforeUI);

        // Return to main menu after a while
        Invoke("LoadMainMenu", delayBeforeMenu);

        // Save progress
        PlayerPrefs.SetInt("GameCompleted", 1);
        PlayerPrefs.Save();
    }

    System.Collections.IEnumerator SwitchToCinematicView()
    {
        yield return new WaitForSeconds(1f);
        Camera.main.enabled = false;
        cinematicCamera.enabled = true;
        cinematicCamera.transform.position = cinematicView.position;
        cinematicCamera.transform.rotation = cinematicView.rotation;
    }

    void ShowVictoryUI()
    {
        if (victoryUI) victoryUI.SetActive(true);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // For debugging
    public bool HasGameEnded()
    {
        return endingTriggered;
    }
}
