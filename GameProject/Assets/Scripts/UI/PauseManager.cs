using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);

        GameState.IsPaused = false;
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (GameState.IsGameOver) return;

        if (GameState.IsPaused) Resume();
        else Pause();
    }

    public void Pause()
    {
        GameState.IsPaused = true;
        Time.timeScale = 0f;

        if (pausePanel) pausePanel.SetActive(true);
    }

    public void Resume()
    {
        GameState.IsPaused = false;
        Time.timeScale = 1f;

        if (pausePanel) pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        GameState.IsPaused = false;
        GameState.IsGameOver = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}
