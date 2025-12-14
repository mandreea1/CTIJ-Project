using UnityEngine;
using TMPro; // pentru TMP_Text

public class GameScoreManager : MonoBehaviour
{
    public static GameScoreManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text distanceText;
    [SerializeField] private TMP_Text coinsText;

    [Header("Score Settings")]
    [SerializeField] private float distanceMultiplier = 5f; // cât de repede crește distanța

    public float CurrentDistance { get; private set; }
    public int CurrentRunCoins { get; private set; }
    public int TotalCoins { get; private set; }

    const string TotalCoinsKey = "TOTAL_COINS";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // load total coins salvate
        TotalCoins = PlayerPrefs.GetInt(TotalCoinsKey, 0);
    }

    void Update()
    {
        // crește distanța în timp; poți ajusta multiplierul
        CurrentDistance += Time.deltaTime * distanceMultiplier;
        UpdateDistanceUI();
    }

    void UpdateDistanceUI()
    {
        if (distanceText != null)
        {
            distanceText.text = Mathf.FloorToInt(CurrentDistance).ToString();
        }
    }

    void UpdateCoinsUI()
    {
        if (coinsText != null)
        {
            coinsText.text = CurrentRunCoins.ToString();
        }
    }

    public void AddCoin(int amount)
    {
        CurrentRunCoins += amount;
        TotalCoins += amount;
        UpdateCoinsUI();

        // salvăm totalul pentru Main Menu / Shop
        PlayerPrefs.SetInt(TotalCoinsKey, TotalCoins);
        PlayerPrefs.Save();
    }
}
