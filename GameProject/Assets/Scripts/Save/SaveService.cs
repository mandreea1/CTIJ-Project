using UnityEngine;

public static class SaveService
{
    // Daca in proiectul tau coins se salveaza cu alt key, schimba aici.
    const string TOTAL_COINS = "TOTAL_COINS";
    const string SELECTED_CHARACTER = "SELECTED_CHARACTER";

    public static int GetCoins() => PlayerPrefs.GetInt(TOTAL_COINS, 0);
    public static void SetCoins(int v) { PlayerPrefs.SetInt(TOTAL_COINS, v); PlayerPrefs.Save(); }

    public static bool IsOwned(string id) => PlayerPrefs.GetInt($"OWNED_{id}", 0) == 1;
    public static void SetOwned(string id, bool owned) { PlayerPrefs.SetInt($"OWNED_{id}", owned ? 1 : 0); PlayerPrefs.Save(); }

    public static string GetSelected(string fallback) => PlayerPrefs.GetString(SELECTED_CHARACTER, fallback);
    public static void SetSelected(string id) { PlayerPrefs.SetString(SELECTED_CHARACTER, id); PlayerPrefs.Save(); }
}
