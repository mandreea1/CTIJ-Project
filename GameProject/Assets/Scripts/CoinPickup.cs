using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instanta != null)
        {
            GameManager.instanta.AdaugaMoneda(coinValue);
        }

        Destroy(gameObject);
    }
}