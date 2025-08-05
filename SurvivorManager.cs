using UnityEngine;
using System;

/// <summary>
/// Manages all survivors, tracks how many are safe, and notifies other systems.
/// Attach to an empty GameObject in the scene.
/// </summary>
public class SurvivorManager : MonoBehaviour
{
    public static SurvivorManager instance;

    [Header("Survivor Settings")]
    public AISurvivor[] survivors;
    private int totalSurvivors;
    private int rescuedSurvivors = 0;

    [Header("UI Display (Optional)")]
    public UnityEngine.UI.Text survivorCountText;

    // Events to notify CarSystem and GameEnding
    public static event Action OnSurvivorReachedCar;
    public static event Action<int> OnSurvivorCountSet;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (survivors.Length == 0)
            survivors = FindObjectsOfType<AISurvivor>();

        totalSurvivors = survivors.Length;
        Debug.Log("Total survivors in mansion: " + totalSurvivors);

        OnSurvivorCountSet?.Invoke(totalSurvivors);
        UpdateUI();
    }

    // Called by AISurvivor when they reach the car
    public static void ReportSurvivorReachedCar()
    {
        if (instance == null) return;
        instance.rescuedSurvivors++;
        Debug.Log("Survivor rescued: " + instance.rescuedSurvivors + "/" + instance.totalSurvivors);

        OnSurvivorReachedCar?.Invoke();
        instance.UpdateUI();

        if (instance.rescuedSurvivors >= instance.totalSurvivors)
        {
            Debug.Log("All survivors are safe! The car can now leave!");
        }
    }

    void UpdateUI()
    {
        if (survivorCountText)
            survivorCountText.text = "Survivors: " + rescuedSurvivors + "/" + totalSurvivors;
    }

    public bool AllSurvivorsSafe()
    {
        return rescuedSurvivors >= totalSurvivors;
    }

    public int GetRemainingSurvivors()
    {
        return totalSurvivors - rescuedSurvivors;
    }
}
