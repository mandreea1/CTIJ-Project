using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    bool paused = false;
    float prevTimeScale = 1f;

    void Start()
    {
        if (pausePanel) pausePanel.SetActive(false);
        paused = false;
        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (paused) Resume();
        else Pause();
    }

    public void Pause()
    {
        if (paused) return;

        paused = true;
        prevTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        if (pausePanel) pausePanel.SetActive(true);
    }

    public void Resume()
    {
        if (!paused) return;

        paused = false;
        Time.timeScale = prevTimeScale <= 0f ? 1f : prevTimeScale;

        if (pausePanel) pausePanel.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
