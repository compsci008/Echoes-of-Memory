using UnityEngine;

public class ResultScreenUI : MonoBehaviour
{
    [Header("Result Screen")]
    public GameObject resultPanel;

    [Header("Stars")]
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public void HideResult()
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(false);
        }
    }

    public void ShowResult(int starsEarned)
    {
        if (resultPanel != null)
        {
            resultPanel.SetActive(true);
        }

        if (star1 != null)
            star1.SetActive(starsEarned >= 1);

        if (star2 != null)
            star2.SetActive(starsEarned >= 2);

        if (star3 != null)
            star3.SetActive(starsEarned >= 3);
    }
}