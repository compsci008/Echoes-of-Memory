using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float startingTime = 30f;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private KiteGameManager gameManager;

    private float currentTime;
    private bool timerRunning;

    private void Start()
    {
        currentTime = startingTime;
        timerRunning = true;

        UpdateTimerText();
    }

    private void Update()
    {
        if (!timerRunning ||
            gameManager == null ||
            gameManager.GameEnded)
        {
            return;
        }

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            timerRunning = false;

            UpdateTimerText();
            gameManager.WinGame();

            return;
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        if (timerText == null)
        {
            return;
        }

        int displayedTime =
            Mathf.CeilToInt(currentTime);

        timerText.text =
            "Time: " + displayedTime;
    }
}