using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private float initialScrollSpeed;
    private int score;
    private int highScore;
    private float timer;
    private float ScrollSpeed;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (recordText != null)
        {
            recordText.text = string.Format("{0:00000}", highScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
        UpdateScrollSpeed();
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    } 

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    private void UpdateScore()
    {
        int scorePerSeconds = 10;

        timer += Time.deltaTime;
        score = (int)(timer * scorePerSeconds);

        if (scoreText != null)
        {
            scoreText.text = string.Format("{0:00000}", score);
        }

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
            {
                recordText.text = string.Format("{0:00000}", highScore);
            }
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public float GetScrollSpeed()
    {
        return ScrollSpeed;
    }

    private void UpdateScrollSpeed()
    {
        float speedDivider = 10f;
        ScrollSpeed = initialScrollSpeed + timer / speedDivider;
    }

    public void ApplyBlancaYNegraEffect(float duration)
    {
        timer = 0;
        StartCoroutine(SlowDownScrollSpeed(duration));
    }

    private IEnumerator SlowDownScrollSpeed(float duration)
    {
        float originalSpeed = ScrollSpeed;
        ScrollSpeed *= 0.3f;
        yield return new WaitForSeconds(duration);
        ScrollSpeed = originalSpeed;
    }
}
