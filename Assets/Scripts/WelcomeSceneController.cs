using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("OpeningScene");
        }
    }
}