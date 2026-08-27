using UnityEngine;
using UnityEngine.UI;

public class ResultScreenUI : MonoBehaviour
{
    [Header("Stars")]
    [SerializeField] private Image star1;
    [SerializeField] private Image star2;
    [SerializeField] private Image star3;

    [Header("Star Sprites")]
    [SerializeField] private Sprite emptyStar;
    [SerializeField] private Sprite yellowStar;

    public void ShowResult(int starsEarned)
    {
        starsEarned = Mathf.Clamp(starsEarned, 0, 3);

        star1.sprite = emptyStar;
        star2.sprite = emptyStar;
        star3.sprite = emptyStar;

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
    }

    public void HideResult()
    {
        gameObject.SetActive(false);
    }
}