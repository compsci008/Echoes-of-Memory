using UnityEngine;

public class BeachAfterController : MonoBehaviour
{
    [SerializeField] private MinigameDialogue dialogue;

    [Header("Audio")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip passMusic;
    [SerializeField] private AudioClip failMusic;

    [Header("Testing")]
    [SerializeField] private bool useTestStars = false;

    [Range(0, 3)]
    [SerializeField] private int testStars = 3;

    private void Awake()
    {
        Time.timeScale = 1f;

        int stars = useTestStars
            ? testStars
            : KiteGameManager.StarsEarned;

        if (stars >= 2)
        {
            musicSource.clip = passMusic;
        }
        else
        {
            musicSource.clip = failMusic;
        }

        musicSource.Play();

        switch (stars)
        {
            case 3:
                dialogue.dialogueLines = new string[]
                {
                    "Grandma remembers it clearly now. She remembers bringing you to the beach and helping you fly your kite.",
                    "She even remembers laughing whenever the wind almost carried it away.",
                    "I remember that too. She always held onto the string whenever I got scared of losing it.",
                    "This memory has returned clearly."
                };
                break;

            case 2:
                dialogue.dialogueLines = new string[]
                {
                    "Grandma remembers taking you to the beach and watching you fly a kite.",
                    "Some of the smaller details are still blurry, but the memory is returning.",
                    "That's okay. At least she remembers that we were here together.",
                    "You've restored a good part of this memory."
                };
                break;

            case 1:
                dialogue.dialogueLines = new string[]
                {
                    "Grandma remembers a kite flying above the beach.",
                    "But she can't clearly remember who was holding it.",
                    "Then there's still something left for her to remember.",
                    "The memory is faint, but at least something has returned."
                };
                break;

            default:
                dialogue.dialogueLines = new string[]
                {
                    "Grandma recognises this beach, but the memory is still very blurry.",
                    "She can't remember what happened here yet.",
                    "Even if she can't remember it yet, I still do.",
                    "Perhaps another memory will help her remember more."
                };
                break;
        }

        dialogue.speakers = new string[]
        {
            "GameMaster",
            "GameMaster",
            "Maya",
            "GameMaster"
        };
    }
}