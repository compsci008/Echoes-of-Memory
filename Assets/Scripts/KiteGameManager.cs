using UnityEngine;

public class KiteGameManager : MonoBehaviour
{
    [SerializeField] private KiteHealth kiteHealth;
    [SerializeField] private ResultScreenUI resultScreen;

    [Header("Sound")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;

    public static int StarsEarned;

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
        if (gameEnded) return;

        gameEnded = true;

        int starsEarned = kiteHealth.CurrentLives;
        StarsEarned = starsEarned;

        if (starsEarned >= 2)
        {
            sfxSource.PlayOneShot(winSound);
        }
        else
        {
            sfxSource.PlayOneShot(loseSound);
        }

        resultScreen.ShowResult(starsEarned);
        Time.timeScale = 0f;
    }

    public void LoseGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        StarsEarned = 0;

        sfxSource.PlayOneShot(loseSound);

        resultScreen.ShowResult(0);
        Time.timeScale = 0f;
    }
}