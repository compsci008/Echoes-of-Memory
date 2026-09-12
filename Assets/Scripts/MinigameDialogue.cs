using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MinigameDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    public GameObject mayaNameBox;
    public GameObject gameMasterNameBox;

    [TextArea(2, 4)]
    public string[] dialogueLines;

    public string[] speakers;

    [Header("Scene Transition")]
    [SerializeField] private string nextSceneName;

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
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(nextSceneName);
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