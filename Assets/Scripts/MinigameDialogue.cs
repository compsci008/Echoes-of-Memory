using UnityEngine;
using TMPro;

public class MinigameDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    public string[] speakers;

    private int currentLine = 0;

    void Start()
    {
        currentLine = 0;
        ShowLine();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentLine++;

            if (currentLine < dialogueLines.Length)
            {
                ShowLine();
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLine];

        bool mayaSpeaking = speakers[currentLine] == "Maya";

        mayaNameBox.SetActive(mayaSpeaking);
        gameMasterNameBox.SetActive(!mayaSpeaking);
    }
}