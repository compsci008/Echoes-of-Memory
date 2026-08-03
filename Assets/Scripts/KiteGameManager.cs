using UnityEngine;

public class KiteGameManager : MonoBehaviour
{
    private bool gameEnded;

    public bool GameEnded
    {
        get { return gameEnded; }
    }

    private void Awake()
    {
        // Ensures the game runs normally whenever this scene starts.
        Time.timeScale = 1f;
    }

    public void WinGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        Debug.Log("You Win!");

        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (gameEnded)
        {
            return;
        }

        gameEnded = true;

        Debug.Log("You Lose!");

        Time.timeScale = 0f;
    }
}