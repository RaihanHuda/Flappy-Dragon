using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Mengatur state game (Siap / Main / Game Over), skor, dan UI.
/// Pakai pola Singleton biar gampang diakses dari script lain: GameManager.Instance
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum State { Ready, Playing, GameOver }
    public State CurrentState { get; private set; } = State.Ready;

    [Header("Referensi UI")]
    public TextMeshProUGUI scoreText;      // teks skor saat main
    public GameObject startPanel;          // panel "Tap untuk mulai"
    public GameObject gameOverPanel;       // panel game over
    public TextMeshProUGUI finalScoreText; // skor akhir
    public TextMeshProUGUI highScoreText;  // rekor tertinggi

    private int score;

    void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
    }

    void Start()
    {
        score = 0;
        UpdateScoreUI();
        if (startPanel) startPanel.SetActive(true);
        if (gameOverPanel) gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // Kalau sudah game over, tap untuk restart
        if (CurrentState == State.GameOver && GetTapInput())
            RestartGame();
    }

    // Input universal: keyboard, mouse, atau sentuhan layar HP
    public static bool GetTapInput()
    {
        return Input.GetKeyDown(KeyCode.Space)
            || Input.GetMouseButtonDown(0)
            || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    }

    public void StartGame()
    {
        if (CurrentState != State.Ready) return;
        CurrentState = State.Playing;
        if (startPanel) startPanel.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText) scoreText.text = score.ToString();
    }

    public void GameOver()
    {
        if (CurrentState == State.GameOver) return;
        CurrentState = State.GameOver;

        int high = PlayerPrefs.GetInt("HighScore", 0);
        if (score > high)
        {
            high = score;
            PlayerPrefs.SetInt("HighScore", high);
            PlayerPrefs.Save();
        }

        if (gameOverPanel) gameOverPanel.SetActive(true);
        if (finalScoreText) finalScoreText.text = "Skor: " + score;
        if (highScoreText) highScoreText.text = "Rekor: " + high;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
