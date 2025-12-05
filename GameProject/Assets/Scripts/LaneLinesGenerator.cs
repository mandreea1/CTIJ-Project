using UnityEngine;

public class LaneLinesGenerator : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 3f;
    public int segments = 10;
    public float dashLength = 2f;
    public float dashWidth = 0.2f;
    public float dashHeight = 0.05f;

    [Header("Road Settings")]
    public float tileLength = 80f;

    void Start()
    {
        //offsetul pentru stanga/dreapta
        float lineOffset = laneDistance / 2f;

        GenerateLane(-lineOffset); // Stanga
        GenerateLane(+lineOffset); // Dreapta
    }

    void GenerateLane(float targetWorldX)
    {
        // Calculam pozitia X locala
        float localX = targetWorldX / transform.localScale.x;

        float totalLengthMeters = tileLength;

        // Distanta dintre startul unei linii si startul urmatoarei
        float stepMeters = totalLengthMeters / segments;

        for (int i = 0; i < segments; i++)
        {
            GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Cube);
            segment.name = "Line_Segment";

            // Scoatem colliderul ca sa nu se loveasca playerul de vopsea
            DestroyImmediate(segment.GetComponent<Collider>());

            segment.transform.SetParent(transform);

            // calcul pozitie z
            // Aflam la cati metri suntem fata de centru
            float currentMeterZ = (stepMeters * i) - (totalLengthMeters / 2) + (stepMeters / 2);
            float localZ = currentMeterZ / tileLength;
            localZ = currentMeterZ / transform.localScale.z;

            // Y
            float localY = 0.51f; 

            segment.transform.localPosition = new Vector3(localX, localY, localZ);
            segment.transform.localScale = new Vector3(
                dashWidth / transform.localScale.x,
                dashHeight / transform.localScale.y,
                dashLength / transform.localScale.z
            );
            Renderer rend = segment.GetComponent<Renderer>();
            rend.material.color = Color.black;
        }
    }
}