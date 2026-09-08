using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ResultScreenUI : MonoBehaviour
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

        // Start with all stars empty
        star1.sprite = emptyStar;
        star2.sprite = emptyStar;
        star3.sprite = emptyStar;

        // Fill only the number of stars earned
        if (starsEarned >= 1)
        {
            star1.sprite = yellowStar;
        }

        if (starsEarned >= 2)
        {
            star2.sprite = yellowStar;
        }

        if (starsEarned >= 3)
        {
            star3.sprite = yellowStar;
        }

        gameObject.SetActive(true);
        canContinue = true;
    }

    private void Update()
    {
        if (!canContinue)
        {
            return;
        }

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
    }

    public void HideResult()
    {
        gameObject.SetActive(false);
        canContinue = false;
    }
}