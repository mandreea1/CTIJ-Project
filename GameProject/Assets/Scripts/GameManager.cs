using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instanta;

    [Header("Interfata")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI coinsText;     
    public GameOverUI gameOverUI;
    //public GameObject GameOverPanel;

    [Header("Setari Joc")]
    public int vieti = 3;
    public float scor = 0;                 // distanta (m)
    public float vitezaInitiala = 15f;
    public float vitezaMaxima = 40f;
    public float rataCrestereViteza = 0.5f;
    public float factorScor = 0.5f;  

    [Header("Monede")]
    public int monedeRunda = 0;            
    public int monedeTotale = 0;          

    [Header("FX UI")]
    public UICountBounce coinsBounce;


    private bool jocTerminat = false;

    const string TOTAL_COINS_KEY = "TOTAL_COINS";
    const string HIGH_SCORE_KEY = "HIGH_SCORE";

    void Awake()
    {
        instanta = this;

        monedeTotale = PlayerPrefs.GetInt(TOTAL_COINS_KEY, 0);
    }

    void Start()
    {
        GameState.IsPaused = false;
        GameState.IsGameOver = false;
        Time.timeScale = 1f;
        PlatformMovement.vitezaGlobala = vitezaInitiala;
        UpdateLivesUI();
        UpdateScoreUI();
        UpdateCoinsUI();

        //if (GameOverPanel != null)
        //    GameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (jocTerminat) return;

        // 1. SCOR SI VITEZA (distanta)
        scor += PlatformMovement.vitezaGlobala * factorScor * Time.deltaTime;

        if (PlatformMovement.vitezaGlobala < vitezaMaxima)
            PlatformMovement.vitezaGlobala += rataCrestereViteza * Time.deltaTime;

        UpdateScoreUI();
    }

    // ---------- SCORE UI ----------
    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = ((int)scor).ToString();
    }

    // ---------- MONEDES ----------
    void UpdateCoinsUI()
    {
        if (coinsText != null)
            coinsText.text = monedeRunda.ToString();
    }

    public void AdaugaMoneda(int amount)
    {
        if (jocTerminat) return;

        monedeRunda += amount;
        monedeTotale += amount;

        UpdateCoinsUI();

        PlayerPrefs.SetInt(TOTAL_COINS_KEY, monedeTotale);
        PlayerPrefs.Save();
        if (coinsBounce != null)
        {
            coinsBounce.Bump();
        }
    }

    // ---------- VIETI ----------
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
                textInimi += "♥ ";

            livesText.text = textInimi;
        }
    }

    //public void GameOver()
    //{
    //    jocTerminat = true;
    //    PlatformMovement.vitezaGlobala = 0;

    //    // highscore simplu
    //    int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    //    if (scor > highScore)
    //    {
    //        PlayerPrefs.SetInt(HIGH_SCORE_KEY, (int)scor);
    //        PlayerPrefs.Save();
    //    }

    //    if (gameOverPanel != null)
    //        gameOverPanel.SetActive(true);
    //}

    public void GameOver()
    {
        jocTerminat = true;
        GameState.IsGameOver = true;
        GameState.IsPaused = true;

        PlatformMovement.vitezaGlobala = 0f;

        Time.timeScale = 0f;

        int highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        if (scor > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, (int)scor);
            PlayerPrefs.Save();
        }

        // arata panel-ul nou
        if (gameOverUI != null)
            gameOverUI.Show((int)scor, monedeRunda);
    }


    //public void RestartJoc()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

}