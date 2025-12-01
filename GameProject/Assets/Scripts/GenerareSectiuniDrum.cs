using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class GenerareSectiuniDrum : MonoBehaviour
{

    public GameObject roadSection;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trigger"))
        {
            Instantiate(roadSection, new Vector3(0, 0, -36), Quaternion.identity);

        }


    }
}
