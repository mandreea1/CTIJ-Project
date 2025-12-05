using UnityEngine;

public class GenerareSectiuniDrum : MonoBehaviour
{
    [Header("Configurare")]
    public GameObject roadSection; 
    public PatternGenerator arhitect; 

    // Distanta la care se spawneaza urmatoarea bucata
    public float lungimeDrum = 80f;

    // reminder unde a fost ultima platforma ca sa o lipim pe urmatoarea
    private Vector3 pozitieUrmatoare = new Vector3(0, 0, 0); // Start

    void Start()
    {
        //Generam primele 2-3 bucati la start ca sa nu cadem in gol
        SpawnPlatforma();
        SpawnPlatforma();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Playerul loveste triggerul invizibil de la finalul unei sectiuni
        if (other.gameObject.CompareTag("Trigger") || other.gameObject.CompareTag("Player"))
        {
            SpawnPlatforma();
        }
    }

    void SpawnPlatforma()
    {
        //Cream platforma goala la pozitia calculata
        GameObject platformaNoua = Instantiate(roadSection, pozitieUrmatoare, Quaternion.identity);

        // Calculam unde va fi urmatoarea (mutam cursorul cu 80m in fata)
        pozitieUrmatoare += new Vector3(0, 0, lungimeDrum);

        //APELAM SCRIPTUL
        if (arhitect != null)
        {
            arhitect.DecoreazaPlatforma(platformaNoua.transform);
        }
    }
}