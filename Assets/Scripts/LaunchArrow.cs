using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.InputSystem;

public class LaunchArrow : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;
    private GameObject arrowInst;

    private Vector2 worldPosition;
    private Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BowRotation();
        BowShooting();
    }

    void BowRotation()
    {
        worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)bow.transform.position).normalized;
        bow.transform.right = direction;
    }

    void BowShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
            arrowInst = Instantiate(arrow, arrowSpawnPoint.position, bow.transform.rotation);
    }
}
