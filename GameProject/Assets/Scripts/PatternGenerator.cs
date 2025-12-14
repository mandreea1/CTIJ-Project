using UnityEngine;
using System.Collections.Generic;

public class PatternGenerator : MonoBehaviour
{
    [Header("Resurse Obstacole")]
    public GameObject[] obstaclePrefabs;
    public GameObject coinPrefab;

    [Header("Resurse Decor (Afara drumului)")]
    public GameObject[] decorPrefabs;

    [Header("Setari")]
    public float lungimeDrum = 70f;

    private List<int> pachetModele = new List<int>();
    private int ultimulObstacolIndex = -1;

    //            PLASARE UNIVERSALA
    void PlaseazaWorld(GameObject prefab, Transform parent, float x, float z)
    {
        if (prefab == null) return;

        GameObject obj = Instantiate(prefab, parent);
        Vector3 scaleFinal = obj.transform.localScale;
        Quaternion rotatieFinala = obj.transform.localRotation;

        float yPos = 0.15f;

        if (prefab == coinPrefab || prefab.name.Contains("Coin") || prefab.CompareTag("Coin"))
        {
            yPos = 1.5f;
            scaleFinal = scaleFinal * 0.35f;

            Collider col = obj.GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
        }
        else
        {
            yPos = 0.15f;
        }

        obj.transform.localPosition = new Vector3(x, yPos, z);
        obj.transform.localRotation = rotatieFinala;
        obj.transform.localScale = scaleFinal;
    }

    //            FUNCTIE PRINCIPALA
    public void DecoreazaPlatforma(Transform platforma)
    {
        GenerareDecorLateral(platforma);

        if (pachetModele.Count == 0) RefacePachetul();
        int modelAles = pachetModele[0];
        pachetModele.RemoveAt(0);

        switch (modelAles)
        {
            case 0: GenerareSlalom(platforma); break;
            case 1: GenerareZiduri(platforma); break;
            case 2: GenerareCasino(platforma); break;
            case 3: GenerarePista(platforma); break;
            case 4: GenerareTunel(platforma); break; 
        }
    }

    void RefacePachetul()
    {
        pachetModele.Clear();
        int deCateOriPunemNormale = 4;
        for (int i = 0; i < deCateOriPunemNormale; i++) pachetModele.Add(i);
        pachetModele.Add(4);

        for (int i = 0; i < pachetModele.Count; i++)
        {
            int temp = pachetModele[i];
            int rand = Random.Range(i, pachetModele.Count);
            pachetModele[i] = pachetModele[rand];
            pachetModele[rand] = temp;
        }
    }

    //            GENERARE SEGMENTE

    void GenerareTunel(Transform parent)
    {
        // Obstacole pe laterale (-3,3)
        for (float z = 10f; z < lungimeDrum - 10f; z += 10f)
        {
            GameObject brad = GetObstacolBrad();
            if (brad != null)
            {
                PlaseazaWorld(brad, parent, -3f, z);
                PlaseazaWorld(brad, parent, 3f, z);
            }
        }

        SpawnMonedeDoarCentru(parent, 5f, lungimeDrum - 5f);
    }

    void GenerareCasino(Transform parent)
    {
        // Obstacole pe centru (0)
        for (int i = 0; i < 3; i++)
        {
            float zPos = 20f + (i * 20f);
            PlaseazaWorld(GetObstacolVariat(), parent, 0f, zPos);
        }

        // Monede doar pe laterale (-3 și 3)
        SpawnMonedeDoarLaterale(parent, 5f, 75f);
    }

    void GenerareSlalom(Transform parent)
    {
        // Obstacole random
        for (int i = 0; i < 4; i++)
        {
            float xPos = ((i % 3) - 1) * 3f;
            float zPos = 15f + (i * 15f);
            PlaseazaWorld(GetObstacolVariat(), parent, xPos, zPos);
        }

        // Aici e safe zone in jurul obstacolelor
        SpawnMonedeDinamic(parent, 2f, 10f);
        SpawnMonedeDinamic(parent, 20f, 25f);
        SpawnMonedeDinamic(parent, 35f, 40f);
        SpawnMonedeDinamic(parent, 50f, 55f);
        SpawnMonedeDinamic(parent, 65f, 75f);
    }

    void GenerareZiduri(Transform parent)
    {
        CreareZidUnic(parent, 20f, Random.Range(-1, 2));
        CreareZidUnic(parent, 60f, Random.Range(-1, 2));
        SpawnMonedeDinamic(parent, 5f, 15f);
        SpawnMonedeDinamic(parent, 25f, 55f);
        SpawnMonedeDinamic(parent, 65f, 75f);
    }

    void GenerarePista(Transform parent)
    {
        for (int i = 0; i < 5; i++)
        {
            float zPos = 10f + (i * 12f);
            float xPos = Random.Range(-1, 2) * 3f;
            PlaseazaWorld(GetObstacolVariat(), parent, xPos, zPos);
        }

        SpawnMonedeDinamic(parent, 2f, 7f);
        SpawnMonedeDinamic(parent, 14f, 19f);
        SpawnMonedeDinamic(parent, 26f, 31f);
        SpawnMonedeDinamic(parent, 38f, 43f);
        SpawnMonedeDinamic(parent, 50f, 55f);
        SpawnMonedeDinamic(parent, 62f, 75f);
    }

    // 1. DOAR CENTRU 
    void SpawnMonedeDoarCentru(Transform parent, float zStart, float zEnd)
    {
        if (coinPrefab == null) return;
        float currentZ = zStart;
        while (currentZ < zEnd)
        {
            int lungime = Random.Range(3, 5);
            for (int i = 0; i < lungime; i++)
            {
                if (currentZ > zEnd) break;
                PlaseazaWorld(coinPrefab, parent, 0f, currentZ); // X = 0
                currentZ += 2.5f;
            }
            currentZ += Random.Range(5f, 10f); // Pauză
        }
    }

    // 2. DOAR LATERALE
    void SpawnMonedeDoarLaterale(Transform parent, float zStart, float zEnd)
    {
        if (coinPrefab == null) return;
        float currentZ = zStart;
        while (currentZ < zEnd)
        {
            float laneX = (Random.Range(0, 2) == 0) ? -3f : 3f;

            int lungime = Random.Range(3, 5);
            for (int i = 0; i < lungime; i++)
            {
                if (currentZ > zEnd) break;
                PlaseazaWorld(coinPrefab, parent, laneX, currentZ);
                currentZ += 2.5f;
            }
            currentZ += Random.Range(5f, 10f);
        }
    }

    // 3. DINAMIC 
    void SpawnMonedeDinamic(Transform parent, float zStart, float zEnd)
    {
        if (coinPrefab == null) return;
        float currentZ = zStart;
        while (currentZ < zEnd)
        {
            int laneIndex = Random.Range(-1, 2); // -1, 0, 1
            float laneX = laneIndex * 3f;

            int lungime = Random.Range(3, 5);
            for (int i = 0; i < lungime; i++)
            {
                if (currentZ > zEnd) break;
                PlaseazaWorld(coinPrefab, parent, laneX, currentZ);
                currentZ += 2.5f;
            }
            currentZ += Random.Range(5f, 10f); // Pauză între schimbări
        }
    }

    //            GENERARE DECOR LATERAL
    void GenerareDecorLateral(Transform parent)
    {
        if (decorPrefabs.Length == 0) return;

        for (float z = 0; z < lungimeDrum; z += Random.Range(3f, 6f))
        {
            int densitateRand = Random.Range(1, 3);
            for (int k = 0; k < densitateRand; k++)
            {
                GameObject decorStanga = decorPrefabs[Random.Range(0, decorPrefabs.Length)];
                float xStanga = Random.Range(-45f, -12f);
                float zRandom = z + Random.Range(-1.5f, 1.5f);
                SpawnDecor(decorStanga, parent, xStanga, zRandom);
            }

            densitateRand = Random.Range(1, 3);
            for (int k = 0; k < densitateRand; k++)
            {
                GameObject decorDreapta = decorPrefabs[Random.Range(0, decorPrefabs.Length)];
                float xDreapta = Random.Range(12f, 45f);
                float zRandom = z + Random.Range(-1.5f, 1.5f);
                SpawnDecor(decorDreapta, parent, xDreapta, zRandom);
            }
        }
    }

    void SpawnDecor(GameObject prefab, Transform parent, float x, float z)
    {
        if (prefab == null) return;
        GameObject obj = Instantiate(prefab, parent);

        float rotatieY = Random.Range(0f, 360f);
        obj.transform.localRotation = Quaternion.Euler(0, rotatieY, 0);

        float scaleRandom = Random.Range(0.8f, 1.3f);
        obj.transform.localScale = obj.transform.localScale * scaleRandom;

        obj.transform.localPosition = new Vector3(x, 0f, z);
    }

    // UTILS

    GameObject GetObstacolVariat()
    {
        if (obstaclePrefabs.Length == 0) return null;
        int indexNou;
        do { indexNou = Random.Range(0, obstaclePrefabs.Length); }
        while (indexNou == ultimulObstacolIndex);
        ultimulObstacolIndex = indexNou;
        return obstaclePrefabs[indexNou];
    }

    GameObject GetObstacolBrad()
    {
        foreach (GameObject ob in obstaclePrefabs)
            if (ob.name.Contains("Tree") || ob.name.Contains("Brad") || ob.name.Contains("Pine"))
                return ob;
        if (obstaclePrefabs.Length > 0) return obstaclePrefabs[0];
        return null;
    }

    void CreareZidUnic(Transform parent, float zPos, int bandaLibera)
    {
        for (int banda = -1; banda <= 1; banda++)
            if (banda != bandaLibera)
                PlaseazaWorld(GetObstacolVariat(), parent, banda * 3f, zPos);
    }
}