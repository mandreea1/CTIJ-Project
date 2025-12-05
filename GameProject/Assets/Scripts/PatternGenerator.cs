using UnityEngine;

public class PatternGenerator : MonoBehaviour
{
    [Header("Piese LEGO (Prefabs)")]
    public GameObject obstaclePrefab; 
    public GameObject coinPrefab;   

    public void DecoreazaPlatforma(Transform platforma)
    {
        int modelAles = Random.Range(0, 3);

        switch (modelAles)
        {
            case 0:
                GenerareSlalom(platforma); 
                break;
            case 1:
                GenerareLiniiSimple(platforma); 
                break;
            case 2:
                GenerareZidCuGaura(platforma); 
                break;
        }
    }

    void GenerareSlalom(Transform parent)
    {
        //  5 obstacole in zig-zag
        for (int i = 0; i < 5; i++)
        {
            float xPos = (i % 2 == 0) ? -3f : 3f;

            //  distanta Z: start de la 10m, din 10 in 10 metri
            float zPos = 10f + (i * 12f);

            PlasareObiect(obstaclePrefab, parent, xPos, zPos);
        }
    }

    void GenerareLiniiSimple(Transform parent)
    {
        // 2 obstacole pe mijloc
        PlasareObiect(obstaclePrefab, parent, 0, 20);
        PlasareObiect(obstaclePrefab, parent, 0, 50);
    }

    void GenerareZidCuGaura(Transform parent)
    {
        // Un zid la 40m distanta, o banda libera random
        int bandaLibera = Random.Range(-1, 2); // -1, 0, 1

        for (int banda = -1; banda <= 1; banda++)
        {
            if (banda != bandaLibera)
            {
                PlasareObiect(obstaclePrefab, parent, banda * 3f, 40f);
            }
        }
    }
    void PlasareObiect(GameObject prefab, Transform parent, float x, float z)
    {
        // obstacolul
        GameObject obj = Instantiate(prefab, parent);
        float localX = x / parent.localScale.x;
        float localZ = z / parent.localScale.z;

        localZ = localZ - 0.5f;

        obj.transform.localPosition = new Vector3(localX, 0, localZ);

        obj.transform.localRotation = Quaternion.identity;

        Vector3 originalScale = prefab.transform.localScale;
        obj.transform.localScale = new Vector3(
            originalScale.x / parent.localScale.x,
            originalScale.y / parent.localScale.y,
            originalScale.z / parent.localScale.z
        );
    }
}