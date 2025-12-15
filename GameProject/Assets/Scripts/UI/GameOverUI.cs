using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject panel;
    public TMP_Text finalScoreText;
    public TMP_Text finalCoinsText;

    bool showing = false;

    void Start()
    {
        if (panel) panel.SetActive(false);
    }

    void Update()
    {
        if (!showing) return;

        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            GoToMenu();
        }
    }

    public void Show(int finalScoreMeters, int coinsRun)
    {
        showing = true;

        if (panel) panel.SetActive(true);
        if (finalScoreText) finalScoreText.text = $"Score: {finalScoreMeters} m";
        if (finalCoinsText) finalCoinsText.text = $"Coins: {coinsRun}";
    }


    public void GoToMenu()
    {
        StartCoroutine(LoadMenuAfterRelease());
    }

    IEnumerator LoadMenuAfterRelease()
    {
        while (Input.GetMouseButton(0) || Input.touchCount > 0)
            yield return null;

        Time.timeScale = 1f;
        GameState.IsPaused = false;
        GameState.IsGameOver = false;
        SceneManager.LoadScene("MainMenu");
    }
}
