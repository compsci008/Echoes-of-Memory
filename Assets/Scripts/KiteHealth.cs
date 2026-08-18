using TMPro;
using UnityEngine;

public class KiteHealth : MonoBehaviour
{
    [SerializeField] private int maximumLives = 3;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private KiteGameManager gameManager;

    private int currentLives;

    public int CurrentLives
    {
        get { return currentLives; }
    }

    private void Start()
    {
        currentLives = maximumLives;
        UpdateLivesText();
    }

    public void LoseLife()
    {
        if (gameManager == null || gameManager.GameEnded)
        {
            return;
        }

        currentLives--;

        if (currentLives <= 0)
        {
            currentLives = 0;
            UpdateLivesText();

            gameManager.LoseGame();
            return;
        }

        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + currentLives;
        }
    }
}