using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public static float vitezaGlobala = 15f;

    void Update()
    {
        transform.Translate(Vector3.back * vitezaGlobala * Time.deltaTime, Space.World);

        if (transform.position.z < -100)
        {
            Destroy(gameObject);
        }
    }
}