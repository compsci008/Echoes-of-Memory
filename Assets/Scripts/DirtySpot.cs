using UnityEngine;

public class DirtySpot : MonoBehaviour
{
    private bool playerNearby = false;

    private void Update()
    {
        if (!playerNearby)
        {
            return;
        }

        if (GameManager.Instance == null ||
            !GameManager.Instance.IsGameActive)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.SpotCleaned();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }
}