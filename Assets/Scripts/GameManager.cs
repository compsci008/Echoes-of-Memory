 using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI cleanedText;
    public GameObject completionText;

    private int cleanedCount = 0;
    private int totalDirtySpots = 5;

    void Awake()
    {
        Instance = this;

        cleanedText.text = "Cleaned: 0 / " + totalDirtySpots;
        completionText.SetActive(false);
    }

    public void SpotCleaned()
    {
        cleanedCount++;

        cleanedText.text = "Cleaned: " + cleanedCount + " / " + totalDirtySpots;

        if (cleanedCount >= totalDirtySpots)
        {
            completionText.SetActive(true);
        }
    }
}