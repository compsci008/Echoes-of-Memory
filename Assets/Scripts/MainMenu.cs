using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string firstSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(firstSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}