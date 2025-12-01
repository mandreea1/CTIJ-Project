using UnityEngine;

[ExecuteInEditMode]
public class CurvedCamera : MonoBehaviour
{
    public Material curvedMaterial;

    [Range(-1f, 1f)]
    public float verticalCurve = 0.1f;

    [Range(-1f, 1f)]
    public float horizontalCurve = 0.0f;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        curvedMaterial.SetFloat("_Vertical", verticalCurve);
        curvedMaterial.SetFloat("_Horizontal", horizontalCurve);

        Graphics.Blit(src, dst, curvedMaterial);
    }
}
