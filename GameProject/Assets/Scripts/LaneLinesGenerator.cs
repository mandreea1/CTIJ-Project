using UnityEngine;

public class LaneLinesGenerator : MonoBehaviour
{
    [Header("Lane Settings")]
    public float laneDistance = 3f;
    public int segments = 10;

    public float dashLength = 2f;
    public float dashWidth = 0.2f;
    public float dashHeight = 0.02f; 

    [Header("Road Settings")]
    public float tileLength = 80f;
    public float heightOffset = 0.16f;

    void Start()
    {
        float lineOffset = laneDistance / 2f;

        GenerateLane(-lineOffset); 
        GenerateLane(+lineOffset); 
    }

    void GenerateLane(float targetLocalX)
    {

        float totalLengthMeters = tileLength;
        float stepMeters = totalLengthMeters / segments;

        for (int i = 0; i < segments; i++)
        {
            GameObject segment = GameObject.CreatePrimitive(PrimitiveType.Cube);
            segment.name = "Line_Segment";

            DestroyImmediate(segment.GetComponent<Collider>());

            segment.transform.SetParent(transform);

            float currentMeterZ = (stepMeters * i) - (totalLengthMeters / 2) + (stepMeters / 2);

            segment.transform.localPosition = new Vector3(targetLocalX, heightOffset, currentMeterZ);

            segment.transform.localScale = new Vector3(dashWidth, dashHeight, dashLength);

            Renderer rend = segment.GetComponent<Renderer>();
            if (rend != null)
            {
                rend.material.color = Color.black; // Sau Color.white dacă e drum negru
            }
        }
    }
}