using System.Collections.Generic;
using UnityEngine;

public static class SaveService
{
    // ===== DATE PE SESIUNE (RAM) =====
    static int coins = 0;
    static int highScore = 0;
    static string selected = "Santa Claus";
    static HashSet<string> owned = new HashSet<string>();

    // CHEILE SECRETE (Trebuie sa fie identice cu cele din GameManager)
    const string COINS_KEY = "TOTAL_COINS";
    const string HIGHSCORE_KEY = "HIGH_SCORE";
    const string SELECTED_CHAR_KEY = "SELECTED_CHAR";
    const string OWNED_PREFIX = "OWNED_";

    // ===== INIT SESIUNE (AICI ESTE SECRETUL) =====
    static SaveService()
    {
        LoadData();
    }

    static void LoadData()
    {
        coins = PlayerPrefs.GetInt(COINS_KEY, 0);
        highScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);
        selected = PlayerPrefs.GetString(SELECTED_CHAR_KEY, "Santa Claus");

        owned.Clear();
        owned.Add("Santa Claus");
    }

    public static void ResetSession()
    {
        LoadData(); 
    }

    // ===== COINS =====
    public static int GetCoins()
    {
        return coins;
    }

    public static void SetCoins(int value)
    {
        coins = Mathf.Max(0, value);

        PlayerPrefs.SetInt(COINS_KEY, coins);
        PlayerPrefs.Save();
    }

    // ===== HIGHSCORE =====
    public static int GetHighScore()
    {
        return highScore;
    }

    public static void SetHighScore(int value)
    {
        highScore = Mathf.Max(highScore, value);
        PlayerPrefs.SetInt(HIGHSCORE_KEY, highScore);
        PlayerPrefs.Save();
    }

    // ===== OWNED CHARACTERS =====
    public static bool IsOwned(string id)
    {
        // Verificam si memoria RAM si Discul
        if (owned.Contains(id)) return true;
        if (PlayerPrefs.GetInt(OWNED_PREFIX + id, 0) == 1)
        {
            owned.Add(id);
            return true;
        }
        return false;
    }

    public static void SetOwned(string id, bool value)
    {
        if (id == "Santa Claus") return; // Santa e mereu owned

        if (value)
        {
            owned.Add(id);
            PlayerPrefs.SetInt(OWNED_PREFIX + id, 1);
        }
        else
        {
            owned.Remove(id);
            PlayerPrefs.SetInt(OWNED_PREFIX + id, 0);
        }
        PlayerPrefs.Save();
    }

    // ===== SELECTED CHARACTER =====
    public static string GetSelected(string fallback = "Santa Claus")
    {
        return string.IsNullOrEmpty(selected) ? fallback : selected;
    }

    public static void SetSelected(string id)
    {
        selected = id;
        PlayerPrefs.SetString(SELECTED_CHAR_KEY, selected);
        PlayerPrefs.Save();
    }
}