using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Archery_Dialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [Header("Gameplay Objects")]
    public GameObject playerMaya;

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
        "Welcome to your 2nd memory memory, Maya.",
        "This place... this was my school too.",
        "Your grandmother spent many years at this school as well. But the memory is beginning to fade.",
        "So how do I restore it?",
        "See the targets? Move your mouse and left click. Fire. I left powers for you to hit, as a bonus.",
        "Thank you, I will begin now."
    };

    void Start()
    {
        currentLine = 0;

        Time.timeScale = 0f;

        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.sendNavigationEvents = false;
        }

        if (playerMaya != null)
        {
            playerMaya.SetActive(false);
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
        if (EventSystem.current != null)
        {
            EventSystem.current.sendNavigationEvents = true;
            EventSystem.current.SetSelectedGameObject(null);
        }

        if (playerMaya != null)
        {
            playerMaya.SetActive(true);
        }

        dialoguePanel.SetActive(false);

        GameManager_Archery.Instance.BeginGame();
    }
}