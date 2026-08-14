using UnityEngine;
using UnityEngine.InputSystem;

public class LaunchArrow : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowSpawnPoint;

    private Vector2 worldPosition;
    private Vector2 direction;

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
        if (!Mouse.current.leftButton.wasPressedThisFrame)
            return;

        int type = ArrowBehaviour.SelectedArrowType;

        if (type == 3) // Split
        {
            ShootSplitArrows();
        }
        else
        {
            ShootSingleArrow((ArrowBehaviour.ArrowType)(type - 1));
        }
    }

    private void ShootSingleArrow(ArrowBehaviour.ArrowType forcedType)
    {
        GameObject arrowInst = Instantiate(arrow, arrowSpawnPoint.position, bow.transform.rotation);
        var arrowBehaviour = arrowInst.GetComponent<ArrowBehaviour>();
        if (arrowBehaviour != null)
        {
            arrowBehaviour.arrowType = forcedType;
        }
    }

    private void ShootSplitArrows()
    {
        float angleOffset = 8f; // degrees between arrows

        // Middle
        ShootArrowAtAngle(0f, ArrowBehaviour.ArrowType.Split);

        // Left
        Quaternion leftRot = bow.transform.rotation * Quaternion.Euler(0, 0, angleOffset);
        GameObject leftArrow = Instantiate(arrow, arrowSpawnPoint.position, leftRot);
        var abLeft = leftArrow.GetComponent<ArrowBehaviour>();
        if (abLeft != null) abLeft.arrowType = ArrowBehaviour.ArrowType.Split;

        // Right
        Quaternion rightRot = bow.transform.rotation * Quaternion.Euler(0, 0, -angleOffset);
        GameObject rightArrow = Instantiate(arrow, arrowSpawnPoint.position, rightRot);
        var abRight = rightArrow.GetComponent<ArrowBehaviour>();
        if (abRight != null) abRight.arrowType = ArrowBehaviour.ArrowType.Split;
    }

    private void ShootArrowAtAngle(float angleOffset, ArrowBehaviour.ArrowType forcedType)
    {
        Quaternion rot = bow.transform.rotation * Quaternion.Euler(0, 0, angleOffset);
        GameObject arrowInst = Instantiate(arrow, arrowSpawnPoint.position, rot);
        var arrowBehaviour = arrowInst.GetComponent<ArrowBehaviour>();
        if (arrowBehaviour != null)
        {
            arrowBehaviour.arrowType = forcedType;
        }
    }
}