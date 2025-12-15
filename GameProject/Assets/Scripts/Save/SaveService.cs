using System.Collections.Generic;
using UnityEngine;

public static class SaveService
{
    // ===== DATE PE SESIUNE (RAM) =====
    static int coins = 0;
    static int highScore = 0;

    static string selected = "Santa Claus";
    static HashSet<string> owned = new HashSet<string>();

    // ===== INIT SESIUNE =====
    static SaveService()
    {
        ResetSession();
    }

    public static void ResetSession()
    {
        coins = 0;
        highScore = 0;
        selected = "Santa Claus";

        owned.Clear();
        owned.Add("Santa Claus"); // Santa Claus Claus FREE mereu
    }

    // ===== COINS =====
    public static int GetCoins()
    {
        return coins;
    }

    public static void SetCoins(int value)
    {
        coins = Mathf.Max(0, value);
    }

    // ===== HIGHSCORE =====
    public static int GetHighScore()
    {
        return highScore;
    }

    public static void SetHighScore(int value)
    {
        highScore = Mathf.Max(highScore, value);
    }

    // ===== OWNED CHARACTERS =====
    public static bool IsOwned(string id)
    {
        return owned.Contains(id);
    }

    public static void SetOwned(string id, bool value)
    {
        if (id == "Santa Claus") return; // nu se dez-cumpara

        if (value) owned.Add(id);
        else owned.Remove(id);
    }

    // ===== SELECTED CHARACTER =====
    public static string GetSelected(string fallback)
    {
        return string.IsNullOrEmpty(selected) ? fallback : selected;
    }

    public static void SetSelected(string id)
    {
        selected = id;
    }
}
