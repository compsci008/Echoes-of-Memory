using UnityEngine;
using UnityEngine.UI;
<<<<<<< Updated upstream
using UnityEngine.InputSystem;
=======
>>>>>>> Stashed changes
using UnityEngine.SceneManagement;

public class ArcheryScore : MonoBehaviour
{
    [Header("Stars")]
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [Header("Star Sprites")]
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite yellowStar;

    [Header("Scene Transition")]
    [SerializeField] private string nextSceneName;

    private bool canContinue = false;
    public int StarsEarned { get; private set; }

    public void ShowResult(int starsEarned)
    {
        starsEarned = Mathf.Clamp(starsEarned, 0, 3);

        // Remember the result for the after-game dialogue
        StarsEarned = starsEarned;

        star1.sprite = emptyStar;
        star2.sprite = emptyStar;
        star3.sprite = emptyStar;

        if (starsEarned >= 1)
        {
            star1.sprite = yellowStar;
            star2.sprite = yellowStar;
            star3.sprite = yellowStar;
        }
        else if (starsEarned == 2)
        {
            star1.sprite = yellowStar;
            star2.sprite = yellowStar;
            star3.sprite = emptyStar;
        }
        else if (starsEarned == 1)
        {
            star1.sprite = yellowStar;
            star2.sprite = emptyStar;
            star3.sprite = emptyStar;
        }
        else
        {
            star1.sprite = emptyStar;
            star2.sprite = emptyStar;
            star3.sprite = emptyStar;
        }

        gameObject.SetActive(true);
        canContinue = true;
    }

    public void ContinueGame()
    {
        Debug.Log("CONTINUE BUTTON PRESSED");

        if (!canContinue)
        {
            Debug.Log("Continue blocked because canContinue is false.");
            return;
        }

<<<<<<< Updated upstream
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            ContinueGame();
        }
    }

    private void ContinueGame()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogError("Next Scene Name has not been set.");
            return;
        }

        canContinue = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene(nextSceneName);
=======
        canContinue = false;
        gameObject.SetActive(false);

        if (GameManager_Archery.Instance != null &&
            GameManager_Archery.Instance.archeryDialogue != null)
        {
            GameManager_Archery.Instance.archeryDialogue.StartAfterDialogue(StarsEarned);
        }
        else
        {
            Debug.LogError("ArcheryDialogue has not been assigned.");
        }
>>>>>>> Stashed changes
    }

    public void RetryGame()
    {
        Debug.Log("RETRY BUTTON PRESSED");

        canContinue = false;

        Time.timeScale = 1f;

        Debug.Log("Reloading scene: " + SceneManager.GetActiveScene().name);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    

    public void HideResult()
    {
        gameObject.SetActive(false);
        canContinue = false;
    }
}