using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
 
    [Header("Gameplay UI")]
    public TextMeshProUGUI cleanedText;
    public TextMeshProUGUI timerText;
 
    [Header("Instructions")]
    public GameObject instructionsPanel;
 
    [Header("End Screen UI")]
    public TextMeshProUGUI titleText;
 
    [Header("Results Prefab (Stars)")]
    public ResultScreenUI resultScreenUI;
 
    [Header("Audio")]
    public AudioSource cleaningAudioSource;
    public AudioSource successAudioSource;
    public AudioSource failureAudioSource;
    public AudioSource backgroundMusicAudioSource;
 
    [Header("Timer")]
    public float timeRemaining = 30f;
 
    private bool gameEnded = false;
    private bool gameStarted = false;
 
    private int cleanedCount = 0;
    private int totalDirtySpots = 5;
 
    public bool IsGameActive
    {
        get { return gameStarted && !gameEnded; }
    }
 
    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1f;
        cleanedCount = 0;
        gameEnded = false;
        gameStarted = false;
        cleanedText.text = "Cleaned: 0 / " + totalDirtySpots;
        instructionsPanel.SetActive(true);
        timerText.gameObject.SetActive(false);
        if (resultScreenUI != null)
        {
            resultScreenUI.HideResult();
        }
        UpdateTimerText();
    }
 
    private void Update()
    {
        if (!gameStarted)
        {
            if (Input.anyKeyDown)
            {
                gameStarted = true;
                instructionsPanel.SetActive(false);
                timerText.gameObject.SetActive(true);
                if (backgroundMusicAudioSource != null)
                {
                    backgroundMusicAudioSource.Play();
                }
            }
            return;
        }
        if (gameEnded)
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
 
    public void SpotCleaned()
    {
        if (gameEnded)
        {
            return;
        }
        if (cleaningAudioSource != null)
        {
            cleaningAudioSource.Play();
        }
        cleanedCount++;
        cleanedText.text = "Cleaned: " + cleanedCount + " / " + totalDirtySpots;
        if (cleanedCount >= totalDirtySpots)
        {
            EndGame(true);
        }
    }
 
    private void EndGame(bool playerWon)
    {
        gameEnded = true;
        timerText.gameObject.SetActive(false);
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Stop();
        }
        int starsEarned = CalculateStars(cleanedCount);
        if (playerWon)
        {
            titleText.text = "ROOM CLEANED!";
            titleText.color = Color.green;
            if (successAudioSource != null)
            {
                successAudioSource.Play();
            }
        }
        else
        {
            titleText.text = "TIME'S UP!";
            titleText.color = Color.red;
            if (failureAudioSource != null)
            {
                failureAudioSource.Play();
            }
        }
        if (resultScreenUI != null)
        {
            resultScreenUI.ShowResult(starsEarned);
        }
        Time.timeScale = 0f;
    }
 
    private int CalculateStars(int cleaned)
    {
        if (cleaned >= 5)
        {
            return 3;
        }
        if (cleaned >= 4)
        {
            return 2;
        }
        if (cleaned >= 2)
        {
            return 1;
        }
        return 0;
    }
 
    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CleaningMiniGame");
    }
 
    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = "Time Left:\n00:" + seconds.ToString("00");
    }
}
 