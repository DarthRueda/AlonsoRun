using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private float initialScrollSpeed = 5f;

    private int score;
    private int highScore;
    private float timer;
    private float ScrollSpeed;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        LoadHighScore();
    }

    private void Update()
    {
        UpdateScore();
        UpdateScrollSpeed();
    }

    public void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            if (!gameOverScreen.activeInHierarchy)
            {
                gameOverScreen.SetActive(true);
            }

            GameObject mainCamera = Camera.main?.gameObject;
            if (mainCamera != null)
            {
                AudioSource cameraAudioSource = mainCamera.GetComponent<AudioSource>();
                if (cameraAudioSource != null && cameraAudioSource.isPlaying)
                {
                    cameraAudioSource.Stop();
                }
            }
        }
        else
        {
            Debug.LogError("GameOverScreen is not assigned!");
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        timer = 0f;
        score = 0;
    }

    private void UpdateScore()
    {
        int scorePerSecond = 10;
        timer += Time.deltaTime;
        score = (int)(timer * scorePerSecond);

        if (scoreText != null)
            scoreText.text = string.Format("{0:00000}", score);

        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();

            if (recordText != null)
                recordText.text = string.Format("{0:00000}", highScore);
        }
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (recordText != null)
            recordText.text = string.Format("{0:00000}", highScore);
    }

    private void UpdateScrollSpeed()
    {
        float speedDivider = 10f;
        ScrollSpeed = initialScrollSpeed + timer / speedDivider;
    }

    public float GetScrollSpeed()
    {
        return ScrollSpeed;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public int GetCurrentScore()
    {
        return score;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Infinito")
        {

            gameOverScreen = GameObject.Find("GameOverScreen");
            scoreText = GameObject.Find("Score")?.GetComponent<TMP_Text>();
            recordText = GameObject.Find("Record")?.GetComponent<TMP_Text>();

            if (gameOverScreen != null)
            {
                gameOverScreen.SetActive(false);
            }
            LoadHighScore();
        }
    }

    public void StartGame()
    {
        timer = 0f;
        score = 0;
        SceneManager.LoadScene("Infinito");
    }
}
