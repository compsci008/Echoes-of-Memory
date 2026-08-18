using UnityEngine;

public class KiteGameManager : MonoBehaviour
{
    [SerializeField] private KiteHealth kiteHealth;
    [SerializeField] private ResultScreenUI resultScreen;

    private bool gameEnded;

    public bool GameEnded
    {
        get { return gameEnded; }
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        int starsEarned = kiteHealth.CurrentLives;

        resultScreen.ShowResult(starsEarned);

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        resultScreen.ShowResult(0);

        Time.timeScale = 0f;
    }
}