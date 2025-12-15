using UnityEngine;



public class CoinPickup : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    public AudioClip pickupSound;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (GameManager.instanta != null)
        {
            GameManager.instanta.AdaugaMoneda(coinValue);
        }



        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
        Destroy(gameObject);
    }
}