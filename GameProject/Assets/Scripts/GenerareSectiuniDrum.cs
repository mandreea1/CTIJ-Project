using UnityEngine;
using System.Collections.Generic;

public class GenerareSectiuniDrum : MonoBehaviour
{
    [Header("Setari Generare")]
    public GameObject[] roadPrefabs;
    public Transform player;

    public float lungimeDrum = 80f;
    public int bucatiInatiale = 4;

    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        if (player == null) player = transform;

        // -60f = 20m safe zone 
        // -40f = 40m safe zone 
        // -30f = 50m safe zone

        float zCurent = -40f;

        for (int i = 0; i < bucatiInatiale; i++)
        {
            bool genereazaDecor = (i > 0);

            SpawnRoad(zCurent, genereazaDecor);
            zCurent += lungimeDrum;
        }
    }

    void Update()
    {
        if (activeRoads.Count > 0)
        {
            GameObject ultimaBucata = activeRoads[activeRoads.Count - 1];

            // Generare infinită
            if (ultimaBucata.transform.position.z < (bucatiInatiale - 1) * lungimeDrum + player.position.z)
            {
                float zNou = ultimaBucata.transform.position.z + lungimeDrum;
                SpawnRoad(zNou, true);
            }
        }

        if (activeRoads.Count > 0)
        {
            GameObject primaBucata = activeRoads[0];
            if (primaBucata.transform.position.z < player.position.z - lungimeDrum - 20f)
            {
                StergeDrumVechi();
            }
        }
    }

    void SpawnRoad(float zPosition, bool cuDecor)
    {
        GameObject go;
        if (roadPrefabs.Length > 0) go = Instantiate(roadPrefabs[0]);
        else return;

        go.transform.position = Vector3.forward * zPosition;
        activeRoads.Add(go);

        if (cuDecor)
        {
            PatternGenerator generator = FindFirstObjectByType<PatternGenerator>();
            if (generator != null)
            {
                generator.DecoreazaPlatforma(go.transform);
            }
        }
    }

    void StergeDrumVechi()
    {
        Destroy(activeRoads[0]);
        activeRoads.RemoveAt(0);
    }
}