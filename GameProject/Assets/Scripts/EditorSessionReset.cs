using UnityEngine;

public static class EditorSessionReset
{
#if UNITY_EDITOR
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void ResetEveryPlay()
    {
        // sterge salvarea persistenta veche
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        // reseteaza si sesiunea in RAM (daca folosesti varianta RAM)
        SaveService.ResetSession();
    }
#endif
}
