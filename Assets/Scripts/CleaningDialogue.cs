using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class CleaningDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [Header("Gameplay Objects")]
    public GameObject playerMaya;
    public GameObject[] dirtySpots;

    private int currentLine = 0;

    private string[] speakers =
    {
        "GameMaster",
        "Maya",
        "GameMaster",
        "Maya",
        "GameMaster",
        "Maya"
    };

    private string[] lines =
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
        currentLine = 0;

        Time.timeScale = 0f;

        // Stop Space from activating UI buttons during dialogue
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.sendNavigationEvents = false;
        }

        // Hide gameplay Maya during dialogue
        if (playerMaya != null)
        {
            playerMaya.SetActive(false);
        }

        // Hide stains during dialogue
        foreach (GameObject spot in dirtySpots)
        {
            if (spot != null)
            {
                spot.SetActive(false);
            }
        }

        ShowLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < lines.Length)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void ShowLine()
    {
        dialogueText.text = lines[currentLine];

        bool mayaSpeaking = speakers[currentLine] == "Maya";

        mayaNameBox.SetActive(mayaSpeaking);
        gameMasterNameBox.SetActive(!mayaSpeaking);
    }

    void EndDialogue()
    {
        // Turn UI navigation back on
        if (EventSystem.current != null)
        {
            EventSystem.current.sendNavigationEvents = true;
            EventSystem.current.SetSelectedGameObject(null);
        }

        // Reveal gameplay Maya
        if (playerMaya != null)
        {
            playerMaya.SetActive(true);
        }

        // Reveal stains
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
}