using UnityEngine;

public class UIPulseMove : MonoBehaviour
{
    [Header("Pulse Settings")]
    public float scaleIntensity = 0.06f;   // 6% max (vizibil, dar curat)
    public float moveIntensity = 6f;       // pixeli sus-jos
    public float speed = 2f;

    Vector3 baseScale;
    Vector3 basePos;

    void Awake()
    {
        baseScale = transform.localScale;
        basePos = transform.localPosition;
    }

    void Update()
    {
        float t = Mathf.Sin(Time.unscaledTime * speed);

        // scale pulse
        transform.localScale = baseScale * (1f + t * scaleIntensity);

        // vertical pulse
        transform.localPosition = basePos + Vector3.up * (t * moveIntensity);
    }
}
