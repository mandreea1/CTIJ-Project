using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopController : MonoBehaviour
{
    public CharacterDatabase db;

    public TMP_Text coinsText;
    public TMP_Text nameText;
    public TMP_Text priceText;
    public TMP_Text buyButtonText;

    public Transform previewAnchor;

    [Header("Menu animator controller (MenuLoop)")]
    public RuntimeAnimatorController menuController;

    int index = 0;
    GameObject current;

    void Start()
    {
        if (!SaveService.IsOwned("SANTA CLAUS")) SaveService.SetOwned("SANTA CLAUS", true);
        RefreshAll();
    }

    void RefreshAll()
    {
        RefreshCoins();
        SpawnPreview();
        RefreshBuyUI();
    }

    void RefreshCoins()
    {
        if (coinsText) coinsText.text = SaveService.GetCoins().ToString();
    }

    void SpawnPreview()
    {
        if (current) Destroy(current);

        var c = db.characters[index];
        if (nameText) nameText.text = c.id.ToUpper();
        if (priceText) priceText.text = c.price == 0 ? "FREE" : c.price.ToString();

        current = Instantiate(c.modelPrefab, previewAnchor);

        SetLayerRecursively(current, LayerMask.NameToLayer("CharacterPreview"));
        //current.transform.localPosition = Vector3.zero;
        //current.transform.localRotation = Quaternion.identity;
        //current.transform.localScale = Vector3.one;
        current.transform.localPosition = c.previewLocalPosition;
        current.transform.localRotation = Quaternion.Euler(c.previewLocalEuler);
        current.transform.localScale = c.previewLocalScale;


        var anim = current.GetComponentInChildren<Animator>();
        if (anim != null && menuController != null)
        {
            anim.runtimeAnimatorController = menuController;
            anim.applyRootMotion = false;
        }
    }

    static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;
        foreach (Transform t in obj.transform)
            SetLayerRecursively(t.gameObject, layer);
    }


    void RefreshBuyUI()
    {
        var c = db.characters[index];
        bool owned = SaveService.IsOwned(c.id);
        if (buyButtonText) buyButtonText.text = owned ? "OWNED" : $"BUY";
    }

    public void Next()
    {
        index = (index + 1) % db.characters.Count;
        RefreshAll();
    }

    public void Prev()
    {
        index = (index - 1 + db.characters.Count) % db.characters.Count;
        RefreshAll();
    }

    public void Buy()
    {
        var c = db.characters[index];
        if (SaveService.IsOwned(c.id)) return;

        int coins = SaveService.GetCoins();
        if (coins < c.price) return;

        SaveService.SetCoins(coins - c.price);
        SaveService.SetOwned(c.id, true);
        RefreshAll();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
