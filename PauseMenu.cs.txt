using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the in-game pause menu (Escape key to toggle).
/// Attach to a Canvas GameObject in the Game scene.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    [Header("Pause UI")]
    public GameObject pauseMenuUI;

    [Header("Optional")]
    public AudioSource pauseSound;
    public AudioSource resumeSound;

    void Start()
    {
        if (pauseMenuUI) pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused) Resume();
            else Pause();
        }
    }

    // Resume game
    public void Resume()
    {
        if (resumeSound) resumeSound.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Pause game
    void Pause()
    {
        if (pauseSound) pauseSound.Play();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Load main menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("MainMenu");
    }

    // Quit game
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
