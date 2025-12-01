using UnityEngine;


// Controls player movement and rotation.
public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public bool isTouching = false;
    private Rigidbody rb; // Reference to player's Rigidbody.
    public float laneDistance = 3f; 
    public float moveSpeed = 10f;   

    private int targetLane = 0;      // -1, 0, +1

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); // Access player's Rigidbody.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")&&isTouching==true ) // Only jump if grounded
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            
        }



        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            ChangeLane(+1);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            ChangeLane(-1);
        }

        Vector3 targetPosition = new Vector3(targetLane * laneDistance, transform.position.y, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }



void ChangeLane(int direction)
{
    targetLane += direction;

    // Limite: -1, 0, +1
    targetLane = Mathf.Clamp(targetLane, -1, 1);
}



void OnCollisionEnter(Collision collision)
    {
        isTouching = true;
    }

    void OnCollisionStay(Collision collision)
    {
        isTouching = true;
    }

    void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }
}

//COD BUN