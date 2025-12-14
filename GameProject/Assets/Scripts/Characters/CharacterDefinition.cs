using UnityEngine;

[CreateAssetMenu(menuName = "WinterRush/Character Definition")]
public class CharacterDefinition : ScriptableObject
{
    public string id;              // "santa", "axe"
    public GameObject modelPrefab; // prefab humanoid (visual)
    public int price;              // 0 pentru santa

    [Header("Shop Preview Transform (relative to PreviewAnchor)")]
    public Vector3 previewLocalPosition = Vector3.zero;
    public Vector3 previewLocalEuler = Vector3.zero;
    public Vector3 previewLocalScale = Vector3.one;
}
