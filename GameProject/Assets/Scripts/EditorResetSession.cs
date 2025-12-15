using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorSessionReset : MonoBehaviour
{
#if UNITY_EDITOR

     [MenuItem("Tools/Reset Save Data (Sterge Tot)")]
    public static void DeleteAllData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        SaveService.ResetSession();

        Debug.Log(" <color=red>TOATE DATELE AU FOST STERSE!</color> Urmatorul Play va fi ca o instalare noua.");
    }

#endif
}