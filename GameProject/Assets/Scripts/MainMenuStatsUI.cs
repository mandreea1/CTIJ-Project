using UnityEngine;
using TMPro;

public class MainMenuStatsUI : MonoBehaviour
{
    const string TOTAL_COINS_KEY = "TOTAL_COINS";
    const string HIGH_SCORE_KEY = "HIGH_SCORE";

    [Header("UI")]
    public TMP_Text totalCoinsText;
    public TMP_Text highScoreText;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        int totalCoins = PlayerPrefs.GetInt(TOTAL_COINS_KEY, 0);
        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);

        if (totalCoinsText != null)
            totalCoinsText.text = totalCoins.ToString();

        if (highScoreText != null)
            highScoreText.text = $"Best: {highScore}";
    }
}
