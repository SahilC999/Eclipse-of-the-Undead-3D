using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// Handles overall game state, score, pause menu, and saving progress.
/// Attach this to a GameManager GameObject in the scene.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Elements")]
    public GameObject pauseMenuUI;
    public GameObject gameOverUI;
    public Text scoreText;
    public Text highScoreText;
    public Text waveText;

    [Header("Game State")]
    public bool isPaused = false;
    public bool isGameOver = false;

    [Header("Score Settings")]
    public int score = 0;
    private int highScore = 0;
    public int zombieKillPoints = 50;

    [Header("Events")]
    public static Action<int> OnScoreUpdated;
    public static Action<int> OnWaveChanged;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        LoadHighScore();
        UpdateScoreUI();
        if (waveText) waveText.text = "Wave 1";
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (isGameOver) return;

        // Pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    // Add points when zombie is killed
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
        OnScoreUpdated?.Invoke(score);
    }

    // Called when wave changes (from WaveSpawner)
    public void UpdateWave(int wave)
    {
        if (waveText) waveText.text = "Wave " + wave;
        OnWaveChanged?.Invoke(wave);
    }

    // Update score UI
    void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (highScoreText) highScoreText.text = "High Score: " + highScore;
    }

    // Pause game
    public void PauseGame()
    {
        isPaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    // Resume game
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Game over logic
    public void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        // Save high score if beaten
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    // Restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Quit to main menu
    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    // Load saved high score
    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // For other scripts to check if game is over
    public bool IsGameOver()
    {
        return isGameOver;
    }

    // For other scripts to check if paused
    public bool IsPaused()
    {
        return isPaused;
    }
}
