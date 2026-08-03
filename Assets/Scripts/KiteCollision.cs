using UnityEngine;

public class KiteCollision : MonoBehaviour
{
    private KiteHealth kiteHealth;

    private void Awake()
    {
        kiteHealth = GetComponent<KiteHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        BirdMovement bird =
            other.GetComponentInParent<BirdMovement>();

        if (bird == null || !bird.enabled)
        {
            return;
        }

        bird.enabled = false;

        Collider2D[] birdColliders =
            bird.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D birdCollider in birdColliders)
        {
            birdCollider.enabled = false;
        }

        kiteHealth.LoseLife();

        Destroy(bird.gameObject);
    }
}