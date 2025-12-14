using System.Collections.Generic;
using UnityEngine;

public class MainMenuCharacterSelector : MonoBehaviour
{
    public CharacterDatabase db;
    public Transform previewAnchor;
    public GameObject defaultModelToHide;

    [Header("Menu animator controller (MenuLoop)")]
    public RuntimeAnimatorController menuController;

    List<CharacterDefinition> owned = new();
    int index = 0;
    GameObject current;

    void Start()
    {
        if (!SaveService.IsOwned("santa")) SaveService.SetOwned("santa", true);
        RebuildOwned();

        string selected = SaveService.GetSelected("santa");
        int found = owned.FindIndex(c => c.id == selected);
        index = found >= 0 ? found : 0;

        Spawn();
    }

    void RebuildOwned()
    {
        owned.Clear();
        foreach (var c in db.characters)
            if (SaveService.IsOwned(c.id))
                owned.Add(c);

        if (owned.Count == 0 && db.characters.Count > 0)
            owned.Add(db.characters[0]);
    }

    void Spawn()
    {
        if (defaultModelToHide) defaultModelToHide.SetActive(false);
        if (current) Destroy(current);

        var c = owned[index];
        current = Instantiate(c.modelPrefab, previewAnchor);
        //current.transform.localPosition = Vector3.zero;
        //current.transform.localRotation = Quaternion.identity;
        //current.transform.localScale = Vector3.one;

        // APLICA valorile din CharacterDefinition
        current.transform.localPosition = c.previewLocalPosition;
        current.transform.localRotation = Quaternion.Euler(c.previewLocalEuler);
        current.transform.localScale = c.previewLocalScale;

        var anim = current.GetComponentInChildren<Animator>();
        if (anim != null && menuController != null)
        {
            anim.runtimeAnimatorController = menuController;
            anim.applyRootMotion = false;
        }

        SaveService.SetSelected(c.id);
    }

    public void Next()
    {
        if (owned.Count == 0) return;
        index = (index + 1) % owned.Count;
        Spawn();
    }

    public void Prev()
    {
        if (owned.Count == 0) return;
        index = (index - 1 + owned.Count) % owned.Count;
        Spawn();
    }
}
