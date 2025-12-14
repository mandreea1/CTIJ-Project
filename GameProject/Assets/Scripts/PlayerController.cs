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

    // Metoda pentru AvatarSwapper - Esentiala pentru a prelua animatorul corect
    public void SetAnimator(Animator newAnim)
    {
        anim = newAnim;
        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;
            // Ne asiguram ca pleaca pe FALSE (alergare)
            anim.SetBool("IsJumping", false);
        }
    }

    void Update()
    {
        // --- REPARATIA PENTRU CASE SI ANIMATIE ---

        // 1. Folosim Raycast (Laser) ca sa detectam ORICE podea (pamant sau casa)
        // Raza pleaca de la 0.5m in sus si verifica 0.8m in jos.
        bool hitGround = Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 0.8f);

        // Daca laserul atinge ceva SI nu suntem in urcare (velocity <= 0)
        if (hitGround && verticalVelocity <= 0)
        {
            isGrounded = true;
            verticalVelocity = 0;
            currentJumpCount = 0;

            // Aliniere fina pe podea (ca sa nu tremure pe casa)
            Vector3 pos = transform.position;
            pos.y = hit.point.y;
            transform.position = pos;

            // --- AICI ESTE FIX-UL CERUT DE TINE ---
            if (anim != null)
            {
                // Ii spunem Animatorului: "Suntem pe jos, deci IsJumping e FALSE"
                // Asta va activa tranzitia inapoi catre HumanoidRun automat
                anim.SetBool("IsJumping", false);
            }
        }
        else
        {
            // Daca nu atingem nimic, suntem in aer
            isGrounded = false;
        }

        // --- INPUT SARITURA ---
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded || currentJumpCount < maxJumps)
            {
                Jump();
            }
        }

        // --- MISCARE STANGA/DREAPTA ---
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) ChangeLane(1);

        float targetX = targetLane * laneDistance;
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * moveSpeed);

        // --- APLICARE GRAVITATIE ---
        if (!isGrounded)
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        float newY = transform.position.y + (verticalVelocity * Time.deltaTime);

        // Aplicam pozitia finala
        transform.position = new Vector3(newX, newY, transform.position.z);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Jump()
    {
        // Calculam forta de saritura
        verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        currentJumpCount++;
        isGrounded = false;

        // --- ACTIVARE ANIMATIE CU VARIABILA ---
        if (anim != null)
        {
            anim.applyRootMotion = false;
            anim.enabled = true;

            // Setam variabila pe TRUE ca sa activeze tranzitia (sagetile din Animator)
            anim.SetBool("IsJumping", true);

            // OPTIONAL: Fortam si play instantaneu pentru reactie rapida
            // (Poti comenta linia de mai jos daca vrei sa lasi doar Bool-ul sa decida)
            anim.Play("HumanoidJumpUp", 0, 0f);
        }
    }

    void ChangeLane(int direction)
    {
        targetLane += direction;
        targetLane = Mathf.Clamp(targetLane, -1, 1);
    }
}