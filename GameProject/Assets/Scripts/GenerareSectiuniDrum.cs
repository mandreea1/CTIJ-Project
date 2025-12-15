using UnityEngine;
using System.Collections.Generic;

public class GenerareSectiuniDrum : MonoBehaviour
{
    [Header("Setari Generare")]
    public GameObject[] roadPrefabs;
    public Transform player;

    public float lungimeDrum = 80f;
    public int bucatiInitiale = 4;

    private List<GameObject> activeRoads = new List<GameObject>();

    void Start()
    {
        if (player == null)
            player = transform;

        float zCurent = -40f;

        for (int i = 0; i < bucatiInitiale; i++)
        {
            bool genereazaDecor = (i > 0);
            SpawnRoad(zCurent, genereazaDecor);
            zCurent += lungimeDrum;
        }
    }

    void Update()
    {
        if (activeRoads.Count == 0)
            return;

        //GENERARE DRUM NOU 
        GameObject ultimaBucata = activeRoads[activeRoads.Count - 1];

        if (ultimaBucata != null &&
            ultimaBucata.transform.position.z <
            (bucatiInitiale - 1) * lungimeDrum + player.position.z)
        {
            float zNou = ultimaBucata.transform.position.z + lungimeDrum;
            SpawnRoad(zNou, true);
        }

        // STERGERE DRUM VECHI 
        GameObject primaBucata = activeRoads[0];

        if (primaBucata != null &&
            primaBucata.transform.position.z <
            player.position.z - lungimeDrum - 20f)
        {
            StergeDrumVechi();
        }
    }

    void SpawnRoad(float zPosition, bool cuDecor)
    {
        if (roadPrefabs == null || roadPrefabs.Length == 0)
            return;

        GameObject go = Instantiate(roadPrefabs[0]);
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
        if (activeRoads.Count == 0)
            return;

        GameObject drum = activeRoads[0];
        activeRoads.RemoveAt(0);

        if (drum != null)
            Destroy(drum);
    }
}
