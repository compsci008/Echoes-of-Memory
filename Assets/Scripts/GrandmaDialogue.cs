using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GrandmaDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TMP_Text dialogueText;
    public TMP_Text mayaNameText;
    public TMP_Text grandmaNameText;

    public GameObject mayaPortrait;
    public GameObject grandmaPortrait;

    private int dialogueIndex = 0;

    private string[] speakers =
    {
        "MAYA",
        "GRANDMA",
        "MAYA",
        "GRANDMA",
        "MAYA"
    };

    private string[] dialogueLines =
    {
        "Grandma? Mum said dinner is ready.",
        "Who... who are you?",
        "Grandma, it's me. Maya.",
        "Maya...? Where am I?",
        "Grandma... you're at home."
    };

    void Start()
    {
        dialoguePanel.SetActive(true);
        ShowDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueIndex++;

            if (dialogueIndex < dialogueLines.Length)
            {
                ShowDialogue();
            }
            else
            {
                SceneManager.LoadScene("CleaningMiniGame");
            }
        }
    }

    void ShowDialogue()
    {
        string speaker = speakers[dialogueIndex];
        dialogueText.text = dialogueLines[dialogueIndex];

        if (speaker == "MAYA")
        {
            mayaNameText.text = "MAYA";

            mayaNameText.transform.parent.gameObject.SetActive(true);
            mayaPortrait.SetActive(true);

            grandmaNameText.transform.parent.gameObject.SetActive(false);
            grandmaPortrait.SetActive(false);
        }
        else
        {
            grandmaNameText.text = "GRANDMA";

            grandmaNameText.transform.parent.gameObject.SetActive(true);
            grandmaPortrait.SetActive(true);

            mayaNameText.transform.parent.gameObject.SetActive(false);
            mayaPortrait.SetActive(false);
        }
    }
}