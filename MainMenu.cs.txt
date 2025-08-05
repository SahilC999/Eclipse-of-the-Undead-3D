using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the main menu functionality (Play, Quit).
/// Attach this to an empty GameObject in the MainMenu scene.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource buttonClickSound;

    // Play button
    public void PlayGame()
    {
        if (buttonClickSound) buttonClickSound.Play();
        Debug.Log("Starting Game...");
        SceneManager.LoadScene("Game");
    }

    // Quit button
    public void QuitGame()
    {
        if (buttonClickSound) buttonClickSound.Play();
        Debug.Log("Exiting Game...");
        Application.Quit();
    }
}
