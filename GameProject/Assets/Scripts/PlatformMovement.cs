using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    

    

    void Update()
    {
        //Translatie platforma
        transform.position += new Vector3(0, 0, 3) * Time.deltaTime;
    }

    //Distrugere instanta platforma la atingerea Box Collider din spatele Playerului

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Destroy"))
        {
            Destroy(gameObject);

        }
    }
}
