using UnityEngine;

public class PlayerAvatarSwapper : MonoBehaviour
{
    public CharacterDatabase db;
    public Transform modelAnchor;

    [Header("Fallback (DOAR mesh-ul vechi, NU root-ul playerului)")]
    public GameObject defaultModelToHide;

    [Header("Gameplay Animator Controller (acelasi pentru toti)")]
    public RuntimeAnimatorController gameplayController;

    GameObject currentModel;

    void Start()
    {
        if (!SaveService.IsOwned("Santa Claus")) SaveService.SetOwned("Santa Claus", true);

        string selectedId = SaveService.GetSelected("Santa Claus");

        CharacterDefinition def = null;
        if (db != null) def = db.characters.Find(c => c.id == selectedId);
        if (def == null && db != null) def = db.characters.Find(c => c.id == "Santa Claus");

        // IMPORTANT: ascunde doar mesh-ul vechi, NU root-ul playerului
        if (defaultModelToHide != null)
            defaultModelToHide.SetActive(false);

        Spawn(def);
    }

    void Spawn(CharacterDefinition def)
    {
        if (def == null || def.modelPrefab == null || modelAnchor == null) return;

        if (currentModel != null)
            Destroy(currentModel);

        currentModel = Instantiate(def.modelPrefab, modelAnchor);
        currentModel.transform.localPosition = Vector3.zero;
        currentModel.transform.localRotation = Quaternion.identity;
        currentModel.transform.localScale = Vector3.one;

        var anim = currentModel.GetComponentInChildren<Animator>(true);
        var playerController = GetComponent<PlayerController>(); // Obtinem controller-ul de pe root

        if (anim != null)
        {
            anim.enabled = true;
            anim.applyRootMotion = false;
            anim.cullingMode = AnimatorCullingMode.AlwaysAnimate;

            if (gameplayController != null)
                anim.runtimeAnimatorController = gameplayController;

            // 2. Transmite referinta Animator-ului NOU catre PlayerController
            if (playerController != null)
            {
                playerController.SetAnimator(anim);
            }
        }
    }

    void OnDestroy()
    {
        // siguranta
        if (currentModel != null)
            Destroy(currentModel);
    }
}
