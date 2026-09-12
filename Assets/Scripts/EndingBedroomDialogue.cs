using UnityEngine;
using TMPro;

public class EndingBedroomDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;

    public TMP_Text dialogueText;
    public TMP_Text mayaNameText;
    public TMP_Text grandmaNameText;

    public GameObject mayaPortrait;
    public GameObject grandmaPortrait;

    private int dialogueIndex = 0;

    private string[] speakers;
    private string[] dialogueLines;

    void Start()
    {
        Time.timeScale = 1f;

        int totalStars = MemoryScoreTracker.TotalStars;

        Debug.Log("FINAL MEMORY SCORE: " + totalStars + " / 9");

        if (totalStars >= 7)
        {
            speakers = new string[]
            {
                "GRANDMA",
                "MAYA",
                "GRANDMA",
                "MAYA",
                "GRANDMA"
            };

            dialogueLines = new string[]
            {
                "Maya... I remember you.",
                "Grandma? You really remember me?",
                "Of course. I remember the room we cleaned, the kite at the beach... and so many little moments.",
                "I'm so happy, Grandma.",
                "Thank you for helping me find those memories again."
            };
        }
        else if (totalStars >= 4)
        {
            speakers = new string[]
            {
                "GRANDMA",
                "MAYA",
                "GRANDMA",
                "MAYA",
                "GRANDMA"
            };

            dialogueLines = new string[]
            {
                "Maya... some things are starting to come back to me.",
                "That's okay, Grandma. You don't have to remember everything at once.",
                "I remember being with you... but some parts are still blurry.",
                "What matters is that we're together now.",
                "Yes... I'm glad you're here with me."
            };
        }
        else
        {
            speakers = new string[]
            {
                "GRANDMA",
                "MAYA",
                "GRANDMA",
                "MAYA",
                "GRANDMA"
            };

            dialogueLines = new string[]
            {
                "I'm sorry... everything still feels so unclear.",
                "It's okay, Grandma. You don't need to force yourself to remember.",
                "I wish I could remember more.",
                "I'll remember for both of us. And I'll stay right here with you.",
                "Thank you, Maya..."
            };
        }

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
                dialoguePanel.SetActive(false);
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