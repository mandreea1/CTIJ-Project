using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacol"))
        {
            LovesteJucator();
            Destroy(other.gameObject);
        }
    }

    void LovesteJucator()
    {
        GameManager.instanta.PierdeViata();
    }
}