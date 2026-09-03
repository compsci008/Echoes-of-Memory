using UnityEngine;
using UnityEngine.SceneManagement;

public class BeachBeforeController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;

    private bool sceneLoaded = false;

    private void Update()
    {
        if (!dialoguePanel.activeSelf && !sceneLoaded)
        {
            sceneLoaded = true;
            SceneManager.LoadScene("Kite Minigame");
        }
    }
}