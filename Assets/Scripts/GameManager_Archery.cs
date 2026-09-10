using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Archery : MonoBehaviour
{
    public static GameManager_Archery Instance;

    [Header("Gameplay UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    [Header("End Screen UI")]
    public TextMeshProUGUI titleText;

    [Header("Results Prefab (Stars)")]
    public ArcheryScore archeryScore;

    [Header("Dialogue")]
    public ArcheryDialogue archeryDialogue;

    [Header("Audio")]
    public AudioSource targetHitAudioSource;
    public AudioSource successAudioSource;
    public AudioSource failureAudioSource;
    public AudioSource backgroundMusicAudioSource;

    [Header("Timer")]
    public float timeRemaining = 30f;

    [Header("Score")]
    public int score = 0;

    private bool gameEnded;
    private bool gameStarted;

    public bool IsGameActive
    {
        get { return gameStarted && !gameEnded; }
    }

    private void Awake()
    {
        // Prevent multiple GameManager_Archery objects.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Time.timeScale = 1f;

        score = 0;
        gameEnded = false;
        gameStarted = false;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        if (archeryScore != null)
        {
            archeryScore.HideResult();
        }

        UpdateScoreText();
        UpdateTimerText();
    }

    private void Update()
    {
        if (!IsGameActive)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;

            UpdateTimerText();
            EndGame(false);
            return;
        }

        UpdateTimerText();
    }

    public void BeginGame()
    {
        if (gameStarted || gameEnded)
        {
            return;
        }

        gameStarted = true;
        Time.timeScale = 1f;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }

        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Play();
        }
    }

    public void TargetHit()
    {
        if (!IsGameActive)
        {
            return;
        }

        if (targetHitAudioSource != null)
        {
            targetHitAudioSource.Play();
        }
    }

    public void TargetKilled()
    {
        if (!IsGameActive)
        {
            return;
        }

        score++;
        UpdateScoreText();

        Debug.Log("Score: " + score);

        if (score >= 15)
        {
            EndGame(true);
        }
    }

    private void EndGame(bool playerWon)
    {
        // Prevent EndGame from running more than once.
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;
        gameStarted = false;

        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Stop();
        }

        int starsEarned = CalculateStars(score);

        if (playerWon)
        {
            if (titleText != null)
            {
                titleText.text = "MINI-GAME COMPLETE!";
                titleText.color = Color.green;
            }

            if (successAudioSource != null)
            {
                successAudioSource.Play();
            }
        }
        else
        {
            if (titleText != null)
            {
                titleText.text = "TIME'S UP!";
                titleText.color = Color.red;
            }

            if (failureAudioSource != null)
            {
                failureAudioSource.Play();
            }
        }

        if (archeryScore != null)
        {
            archeryScore.ShowResult(starsEarned);
        }

        Time.timeScale = 0f;
    }

    private int CalculateStars(int finalScore)
    {
        if (finalScore >= 15)
        {
            return 3;
        }

        if (finalScore >= 10)
        {
            return 2;
        }

        if (finalScore >= 5)
        {
            return 1;
        }

        return 0;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Archery Mini game");
    }

    public void ContinueGame()
    {
        if (archeryScore == null)
        {
            Debug.LogWarning("ArcheryScore is not assigned.");
            return;
        }

        if (archeryDialogue == null)
        {
            Debug.LogWarning("ArcheryDialogue is not assigned.");
            return;
        }

        int starsEarned = archeryScore.StarsEarned;

        archeryScore.HideResult();

        // EndGame paused the game, so restore normal time.
        Time.timeScale = 1f;

        archeryDialogue.StartAfterDialogue(starsEarned);
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Time Left:\n00:" + seconds.ToString("00");
    }
}