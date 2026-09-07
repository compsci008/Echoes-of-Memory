using UnityEngine;

public class BirdAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] frames;
    [SerializeField] private float frameTime = 0.08f;

    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (frames.Length == 0)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer >= frameTime)
        {
            timer = 0f;

            currentFrame++;

            if (currentFrame >= frames.Length)
            {
                currentFrame = 0;
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}