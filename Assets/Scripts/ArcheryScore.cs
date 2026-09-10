using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ArcheryScore : MonoBehaviour
{
    [Header("Stars")]
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [Header("Star Sprites")]
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite yellowStar;

    private bool canContinue = false;

    public int StarsEarned { get; private set; }

    public void ShowResult(int starsEarned)
    {
        starsEarned = Mathf.Clamp(starsEarned, 0, 3);

        StarsEarned = starsEarned;

        // Start with all stars empty
        star1.sprite = emptyStar;
        star2.sprite = emptyStar;
        star3.sprite = emptyStar;

        // Fill only the number of stars actually earned
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
        canContinue = false;

        // Hide result screen
        gameObject.SetActive(false);

        // Start Pine's after-game dialogue
        if (GameManager_Archery.Instance != null &&
            GameManager_Archery.Instance.archeryDialogue != null)
        {
            GameManager_Archery.Instance.archeryDialogue.StartAfterDialogue(StarsEarned);
        }
        else
        {
            Debug.LogError("ArcheryDialogue has not been assigned.");
        }
    }

    public void HideResult()
    {
        gameObject.SetActive(false);
        canContinue = false;
    }
}