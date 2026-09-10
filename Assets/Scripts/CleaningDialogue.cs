using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CleaningDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [Header("Gameplay Objects")]
    public GameObject playerMaya;
    public GameObject[] dirtySpots;

    [Header("Music")]
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
        "Welcome to your first memory, Maya.",
        "This place... it feels familiar.",
        "Your grandmother spent many days here with her family. But the memory is beginning to fade.",
        "So how do I restore it?",
        "Start by cleaning the room. Move close to the dirty spots and press E to clean them before time runs out.",
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

        if (playerMaya != null)
        {
            playerMaya.SetActive(false);
        }

        foreach (GameObject spot in dirtySpots)
        {
            if (spot != null)
            {
                spot.SetActive(false);
            }
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
                "She remembers cleaning this living room every weekend while music played in the background.",
                "She remembers that clearly?",
                "She does. She even remembers you following her around and trying to help her clean.",
                "Haha... that sounds like me. I'm glad Grandma can remember that again."
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
                "She remembers cleaning this living room often, and she can remember music playing while she cleaned.",
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
                "She remembers spending time in this living room, but she can't quite remember what she was doing or who was with her.",
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
                "She can recognise this living room, but she still can't remember what happened here.",
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

        if (playerMaya != null)
        {
            playerMaya.SetActive(true);
        }

        foreach (GameObject spot in dirtySpots)
        {
            if (spot != null)
            {
                spot.SetActive(true);
            }
        }

        dialoguePanel.SetActive(false);

        GameManager.Instance.BeginGame();
    }

    void EndAfterDialogue()
    {
        EnableUIButtons();

        if (gameMasterMusic != null)
        {
            gameMasterMusic.StopMusic();
        }

        dialoguePanel.SetActive(false);

        Time.timeScale = 1f;

        SceneManager.LoadScene("Beach_Before");
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