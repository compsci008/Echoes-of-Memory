using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public static PowerUpManager Instance { get; private set; }

    public enum PowerUpType
    {
        Normal,
        Pierce,
        Split
    }

    public PowerUpType CurrentPowerUp { get; private set; } = PowerUpType.Normal;

    private float powerUpEndTime;
    private bool hasActivePowerUp;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Update()
    {
        if (hasActivePowerUp && Time.time >= powerUpEndTime)
        {
            ResetPowerUp();
        }
    }

    public void ActivatePowerUp(PowerUpType type, float duration)
    {
        CurrentPowerUp = type;
        powerUpEndTime = Time.time + duration;
        hasActivePowerUp = true;

        if (type == PowerUpType.Pierce)
            ArrowBehaviour.SetSelectedArrowType(2);
        else if (type == PowerUpType.Split)
            ArrowBehaviour.SetSelectedArrowType(3);
        else
            ArrowBehaviour.SetSelectedArrowType(1);
    }

    public void ResetPowerUp()
    {
        CurrentPowerUp = PowerUpType.Normal;
        hasActivePowerUp = false;

        ArrowBehaviour.SetSelectedArrowType(1);
    }
}