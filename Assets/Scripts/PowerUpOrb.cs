using UnityEngine;

public class PowerUpOrb : MonoBehaviour
{
    [Header("PowerUp Settings")]
    public PowerUpManager.PowerUpType powerUpType = PowerUpManager.PowerUpType.Pierce;
    public float powerUpDuration = 5f;

    [Header("Colors")]
    public Color normalColor = Color.green;
    public Color pierceColor = Color.blue;
    public Color splitColor = Color.red;

    private Renderer orbRenderer;

    void Awake()
    {
        orbRenderer = GetComponent<Renderer>();
        SetColorForType(powerUpType);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Optionally: if (!other.CompareTag("Player")) return;

        PowerUpManager.Instance.ActivatePowerUp(powerUpType, powerUpDuration);

        Destroy(gameObject);
    }

    public void SetColorForType(PowerUpManager.PowerUpType type)
    {
        Color color;
        switch (type)
        {
            case PowerUpManager.PowerUpType.Normal:
                color = normalColor;
                break;
            case PowerUpManager.PowerUpType.Pierce:
                color = pierceColor;
                break;
            case PowerUpManager.PowerUpType.Split:
                color = splitColor;
                break;
            default:
                color = normalColor;
                break;
        }

        if (orbRenderer != null)
            orbRenderer.material.color = color;
    }
}