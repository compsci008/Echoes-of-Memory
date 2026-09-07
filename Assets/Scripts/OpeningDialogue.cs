using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class OpeningDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TMP_Text dialogueText;
    public TMP_Text mayaNameText;
    public TMP_Text parentNameText;

    public GameObject mayaPortrait;
    public GameObject parentPortrait; // Mum
    public GameObject dadPortrait;

    private int dialogueIndex = 0;

    private string[] speakers =
    {
        "MUM",
        "DAD",
        "MAYA",
        "MUM",
        "MAYA",
        "DAD"
    };

    private string[] dialogueLines =
    {
        "It's nice having dinner together like this.",
        "It is. We haven't had a quiet evening together in a while.",
        "Yeah, it's actually quite nice.",
        "Maya, could you go upstairs and bring Grandma down for dinner?",
        "Sure, Mum.",
        "She's probably wondering where we are."
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
                SceneManager.LoadScene("GrandmaScene");
            }
        }
    }

    void ShowDialogue()
    {
        string speaker = speakers[dialogueIndex];

        dialogueText.text = dialogueLines[dialogueIndex];

        mayaPortrait.SetActive(false);
        parentPortrait.SetActive(false);
        dadPortrait.SetActive(false);

        if (speaker == "MAYA")
        {
            mayaNameText.text = "MAYA";

            mayaNameText.transform.parent.gameObject.SetActive(true);
            parentNameText.transform.parent.gameObject.SetActive(false);

            mayaPortrait.SetActive(true);
        }
        else
        {
            parentNameText.text = speaker;

            parentNameText.transform.parent.gameObject.SetActive(true);
            mayaNameText.transform.parent.gameObject.SetActive(false);

            if (speaker == "MUM")
            {
                parentPortrait.SetActive(true);
            }
            else if (speaker == "DAD")
            {
                dadPortrait.SetActive(true);
            }
        }
    }
}