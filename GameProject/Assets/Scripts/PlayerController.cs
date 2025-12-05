using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Setari Miscare")]
    public float laneDistance = 3f;
    public float moveSpeed = 10f;

    [Header("Setari Saritura")]
    public float jumpHeight = 2.0f;
    public float gravity = -20f;
    public int maxJumps = 2;

    private Animator anim;
    private int targetLane = 0;
    private bool isGrounded = true;
    private float verticalVelocity = 0;
    private int currentJumpCount = 0; 

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim != null) anim.applyRootMotion = false;
    }

    void Update()
    {
        if (transform.position.y <= 0.25f && verticalVelocity <= 0)
        {
            isGrounded = true;
            verticalVelocity = 0;
            currentJumpCount = 0; 

            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;

            if (anim != null) anim.SetBool("IsJumping", false);
        }
        else
        {
            isGrounded = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || currentJumpCount < maxJumps)
            {
                Jump();
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);

        float targetX = targetLane * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed);

        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        float newY = transform.position.y + (verticalVelocity * Time.deltaTime);
        float newZ = 0f;

        transform.position = new Vector3(newX, newY, newZ);

        //  rotatia 0 ca sa nu derapeze
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Jump()
    {
        //impuls
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        currentJumpCount++;
        isGrounded = false;

        if (anim != null)
        {
            anim.Play("HumanoidJumpUp", 0, 0f);
            anim.SetBool("IsJumping", true);
        }
    }

    void ChangeLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);
    }
}