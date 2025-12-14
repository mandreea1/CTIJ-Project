using UnityEngine;

public class UICountBounce : MonoBehaviour
{
    public float duration = 0.25f;   // cât durează hop-ul
    public float intensity = 0.2f;   // cât de mult se mărește (0.2 = +20%)

    private Vector3 baseScale;
    private float timer = 0f;
    private bool isBouncing = false;

    void Awake()
    {
        baseScale = transform.localScale;
    }

    void Update()
    {
        if (!isBouncing) return;

        timer += Time.unscaledDeltaTime;   // UI independent de Time.timeScale
        float t = timer / duration;

        if (t >= 1f)
        {
            // gata animația, revine la normal
            isBouncing = false;
            transform.localScale = baseScale;
            return;
        }

        // sin(0..pi) pornește de la 0, urcă la 1 și revine la 0
        float s = 1f + Mathf.Sin(t * Mathf.PI) * intensity;
        transform.localScale = baseScale * s;
    }

    public void Bump()
    {
        // pornește animația de la capăt
        timer = 0f;
        isBouncing = true;
    }
}
