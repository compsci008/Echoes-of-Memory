using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ArcheryDialogue : MonoBehaviour

{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [Header("Gameplay Objects")]
    public GameObject playerMaya;

    public GaplessLoopMusic gameMasterMusic;

    private int currentLine = 0;
    private bool showingAfterDialogue = false;

    private string[] currentSpeakers;
    private string[] currentLines;

    // BEFORE-GAME DIALOGUE
    private string[] introSpeakers =
    {
        "GameMaster",
        "Maya",
        "GameMaster",
        "Maya",
        "GameMaster",
        "Maya"
    };

    private string[] introLines =
    {
        "Welcome to your second memory, Maya.",
        "This place... it feels familiar.",
        "Your grandmother spent many days here with her friends. But the memory is beginning to fade.",
        "So how do I restore it?",
        "Shoot the targets before time runs out. Mouse over them and click. I left abilities orbs, shoot them to get different powers to help you. The colors mean different abilities.",
        "Alright. I'll do my best."
    };

    void Start()
    {
        StartIntroDialogue();
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < currentLines.Length)
            {
                ShowLine();
            }
            else
            {
                if (showingAfterDialogue)
                {
                    EndAfterDialogue();
                }
                else
                {
                    EndIntroDialogue();
                }
            }
        }
    }

    void StartIntroDialogue()
    {
        showingAfterDialogue = false;
        currentLine = 0;

        currentSpeakers = introSpeakers;
        currentLines = introLines;

        Time.timeScale = 0f;

        DisableUIButtons();

        if (gameMasterMusic != null)
        {
            gameMasterMusic.PlayMusic();
        }

        // Hide gameplay Maya during intro dialogue
        if (playerMaya != null)
        {
            playerMaya.SetActive(false);
        }

        dialoguePanel.SetActive(true);

        ShowLine();
    }

    public void StartAfterDialogue(int starsEarned)
    {
        showingAfterDialogue = true;
        currentLine = 0;

        SetAfterDialogue(starsEarned);

        Time.timeScale = 0f;

        DisableUIButtons();

        if (gameMasterMusic != null)
        {
            gameMasterMusic.PlayMusic();
        }

        // Hide gameplay Maya so only the dialogue portraits
        if (playerMaya != null)
        {
            playerMaya.SetActive(false);
        }

        dialoguePanel.SetActive(true);

        ShowLine();
    }

    void SetAfterDialogue(int starsEarned)
    {
        if (starsEarned >= 3)
        {
            currentSpeakers = new string[]
            {
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya"
            };

            currentLines = new string[]
            {
                "Amazing work, Maya! You got 3 stars. Your grandma was able to remember this memory very clearly.",
                "Really? What does she remember?",
                "She remembers winning an archery contest, firing arrows until the time ran out and cheers!",
                "She remembers that clearly?",
                "She does. She even remembers her husband cheering for her and hugging her in excitement.",
                "I'm glad Grandma can remember that again."
            };
        }
        else if (starsEarned == 2)
        {
            currentSpeakers = new string[]
            {
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya"
            };

            currentLines = new string[]
            {
                "Good job, Maya! You got 2 stars. Grandma is starting to remember more of this memory.",
                "What can she remember?",
                "She remembers shooting the arrows and the cheers!",
                "That's good! Does she remember anything else?",
                "Some parts are still blurry, but more pieces of the memory are beginning to return.",
                "Then we're getting there. I want to help her remember the rest too."
            };
        }
        else if (starsEarned == 1)
        {
            currentSpeakers = new string[]
            {
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya"
            };

            currentLines = new string[]
            {
                "You got 1 star, Maya. Some parts are still blurry, but Grandma managed to remember a little.",
                "What does she remember?",
                "She remembers shooting arrows, but she can't quite remember what happened after.",
                "So the memory still isn't very clear...",
                "Not yet. But even remembering this place is a small piece of the memory returning.",
                "Then I'll keep trying. I want to help Grandma remember more."
            };
        }
        else
        {
            currentSpeakers = new string[]
            {
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya",
                "GameMaster",
                "Maya"
            };

            currentLines = new string[]
            {
                "This memory is still quite blurry, Maya.",
                "Grandma couldn't remember it?",
                "She can recognise the school, but she still can't remember what happened here.",
                "Oh... I was hoping I could bring the memory back for her.",
                "Don't give up. There are still more memories for us to restore.",
                "You're right. I'll keep going for Grandma."
            };
        }
    }

    void ShowLine()
    {
        dialogueText.text = currentLines[currentLine];

        bool mayaSpeaking = currentSpeakers[currentLine] == "Maya";

        mayaNameBox.SetActive(mayaSpeaking);
        gameMasterNameBox.SetActive(!mayaSpeaking);
    }

    void EndIntroDialogue()
    {
        EnableUIButtons();

        if (gameMasterMusic != null)
        {
            gameMasterMusic.StopMusic();
        }

        // Reveal gameplay Maya
        if (playerMaya != null)
        {
            playerMaya.SetActive(true);
        }

        dialoguePanel.SetActive(false);

        // Resume time
        Time.timeScale = 1f;

        GameManager_Archery.Instance.BeginGame();
    }

    void EndAfterDialogue()
    {
        EnableUIButtons();

        if (gameMasterMusic != null)
        {
            gameMasterMusic.StopMusic();
        }

        dialoguePanel.SetActive(false);

        // Resume the game
        Time.timeScale = 1f;
    }

    void DisableUIButtons()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.sendNavigationEvents = false;
        }
    }

    void EnableUIButtons()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.sendNavigationEvents = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}