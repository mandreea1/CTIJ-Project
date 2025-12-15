using UnityEngine;

public class InputBlocker : MonoBehaviour
{
    public static bool Blocked { get; private set; }

    [SerializeField] float blockSeconds = 0.25f;

    void OnEnable()
    {
        Blocked = true;
        Invoke(nameof(Unblock), blockSeconds);
    }

    void Unblock()
    {
        Blocked = false;
    }
}
