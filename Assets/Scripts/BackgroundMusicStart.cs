using UnityEngine;

public class BackgroundMusicStart : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.time = 15f;
        audioSource.Play();
    }
}