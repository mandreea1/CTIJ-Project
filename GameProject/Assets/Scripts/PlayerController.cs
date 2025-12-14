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

    //void Start()
    //{
    //    anim = GetComponentInChildren<Animator>();
    //    if (anim != null) anim.applyRootMotion = false;
    //}

    // NOU: Metodă publică pentru a seta Animator-ul din exterior
    public void SetAnimator(Animator newAnim)
    {
        anim = newAnim;
        // Forțează pornirea animației de alergare imediat
        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;
            // Folosim Play() cu Layer Index 0 (Base Layer) și timpul 0f
            // pentru a forța rularea imediată.
            anim.Play("HumanoidRun", 0, 0f);
        }
    }


    void Update()
    {
        //if (anim == null) anim = GetComponentInChildren<Animator>();

        if (transform.position.y <= 0.25f && verticalVelocity <= 0)
        {
            isGrounded = true;
            verticalVelocity = 0;
            currentJumpCount = 0; 

            Vector3 pos = transform.position;
            pos.y = 0;
            transform.position = pos;

            //if (anim != null) anim.SetBool("IsJumping", false);
            if (anim != null) anim.CrossFade("HumanoidRun", 0.05f);

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

    //void Jump()
    //{
    //    //impuls
    //    verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    //    currentJumpCount++;
    //    isGrounded = false;

    //    if (anim != null)
    //    {
    //        anim.Play("HumanoidJumpUp", 0, 0f);
    //        anim.SetBool("IsJumping", true);
    //    }
    //}
    void Jump()
    {
        // impuls vertical
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        currentJumpCount++;
        isGrounded = false;

        // siguranta: ia animatorul ACTIV (cel instantiat dupa swap)
        //if (anim == null || !anim.gameObject.activeInHierarchy)
        //    anim = GetComponentInChildren<Animator>();

        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;

            // sari in animatia de jump
            anim.CrossFade("HumanoidJumpUp", 0.05f);
        }
    }



    void ChangeLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);
    }
}