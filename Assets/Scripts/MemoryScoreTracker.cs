using UnityEngine;

public static class MemoryScoreTracker
{
    public static int CleaningStars { get; private set; }
    public static int KiteStars { get; private set; }
    public static int ArcheryStars { get; private set; }

    public static int TotalStars
    {
        get
        {
            return CleaningStars + KiteStars + ArcheryStars;
        }
    }

    public static void SetCleaningStars(int stars)
    {
        CleaningStars = Mathf.Clamp(stars, 0, 3);
    }

    public static void SetKiteStars(int stars)
    {
        KiteStars = Mathf.Clamp(stars, 0, 3);
    }

    public static void SetArcheryStars(int stars)
    {
        ArcheryStars = Mathf.Clamp(stars, 0, 3);
    }

    public static void ResetScores()
    {
        CleaningStars = 0;
        KiteStars = 0;
        ArcheryStars = 0;
    }
}