using UnityEngine;

public class CoinRotator : MonoBehaviour
{
    [Header("Setări Rotație")]
    // Cu cât numărul e mai mare, cu atât se învârte mai repede
    public float vitezaRotatie = 150f;

    void Update()
    {
        transform.Rotate(0, vitezaRotatie * Time.deltaTime, 0, Space.Self);
    }

    //detectează când Moșul atinge moneda
    private void OnTriggerEnter(Collider other)
    {
        // Verificăm dacă obiectul care a atins moneda este Player-ul
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}