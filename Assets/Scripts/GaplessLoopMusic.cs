using UnityEngine;

public class GaplessLoopMusic : MonoBehaviour
{
    public AudioClip musicClip;

    private AudioSource sourceA;
    private AudioSource sourceB;

    private AudioSource currentSource;
    private AudioSource nextSource;

    private double nextStartTime;
    private float overlap = 0.3f;

    private bool isPlaying = false;

    void Awake()
    {
        sourceA = gameObject.AddComponent<AudioSource>();
        sourceB = gameObject.AddComponent<AudioSource>();

        SetupSource(sourceA);
        SetupSource(sourceB);

        currentSource = sourceA;
        nextSource = sourceB;
    }

    void Update()
    {
        if (isPlaying && AudioSettings.dspTime >= nextStartTime)
        {
            nextSource.clip = musicClip;
            nextSource.Play();

            AudioSource temp = currentSource;
            currentSource = nextSource;
            nextSource = temp;

            nextStartTime = AudioSettings.dspTime + musicClip.length - overlap;
        }
    }

    public void PlayMusic()
    {
        if (isPlaying || musicClip == null)
            return;

        sourceA.Stop();
        sourceB.Stop();

        currentSource = sourceA;
        nextSource = sourceB;

        currentSource.clip = musicClip;
        currentSource.Play();

        nextStartTime = AudioSettings.dspTime + musicClip.length - overlap;

        isPlaying = true;
    }

    public void StopMusic()
    {
        sourceA.Stop();
        sourceB.Stop();

        isPlaying = false;
    }

    void SetupSource(AudioSource source)
    {
        source.playOnAwake = false;
        source.loop = false;
        source.spatialBlend = 0f;
        source.volume = 1f;
    }
}