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
    public void SetAnimator(Animator newAnim)
    {
        anim = newAnim;
        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;
            anim.SetBool("IsJumping", false);
        }
    }

    void Update()
    {
        if (GameState.IsPaused || GameState.IsGameOver) return; //////////adaugat pentru pauza si game over

        // CALCULAM BANDA (X)
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);

        float targetX = targetLane * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed);

        // CALCULAM INALTIMEA (Y)
        float newY = transform.position.y;

        bool hitGround = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 0.8f);

        if (hitGround && verticalVelocity <= 0)
        {
            isGrounded = true;
            verticalVelocity = 0;
            currentJumpCount = 0; 

            newY = hit.point.y;

            if (anim != null) anim.SetBool("IsJumping", false);
        }
        else
        {
            isGrounded = false;

            verticalVelocity += gravity * Time.deltaTime;

            newY += verticalVelocity * Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || currentJumpCount < maxJumps)
            {
                Jump();
            }
        }

        transform.position = new Vector3(newX, newY, transform.position.z);

        transform.rotation = Quaternion.identity;
    }

    void Jump()
    {
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        currentJumpCount++;
        isGrounded = false;

        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;
            anim.SetBool("IsJumping", true);
            anim.Play("HumanoidJumpUp", 0, 0f);
        }
    }

    void ChangeLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);
    }
}