using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static GameManager instanta;

    [Header("Interfata")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText; 
    public GameObject gameOverPanel;  

    [Header("Setari Joc")]
    public int vieti = 3;
    public float scor = 0;
    public float vitezaInitiala = 15f;
    public float vitezaMaxima = 40f;
    public float rataCrestereViteza = 0.5f;

    private bool jocTerminat = false;

    void Awake()
    {
        instanta = this; 
    }

    void Start()
    {
        PlatformMovement.vitezaGlobala = vitezaInitiala;
        UpdateLivesUI();
        if (gameOverPanel != null) gameOverPanel.SetActive(false); 
    }

    void Update()
    {
        if (jocTerminat) return;

        // 1. SCOR SI VITEZA
        scor += PlatformMovement.vitezaGlobala * Time.deltaTime;

        if (scoreText != null)
            scoreText.text = ((int)scor).ToString() + " m";

        if (PlatformMovement.vitezaGlobala < vitezaMaxima)
            PlatformMovement.vitezaGlobala += rataCrestereViteza * Time.deltaTime;
    }

    // --- FUNCTII PENTRU VIETI ---

    public void PierdeViata()
    {
        if (jocTerminat) return;

        vieti--;
        UpdateLivesUI();

        if (vieti <= 0)
        {
            GameOver();
        }
    }

    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            string textInimi = "";

            for (int i = 0; i < vieti; i++)
            {
                textInimi += "♥ "; 
            }

            livesText.text = textInimi;
        }
    }

    public void GameOver()
    {
        jocTerminat = true;
        PlatformMovement.vitezaGlobala = 0; 

        Debug.Log("GAME OVER!");

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartJoc()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}