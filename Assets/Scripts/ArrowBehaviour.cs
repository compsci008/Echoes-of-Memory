using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [Header("General Arrow Stats")]
    [SerializeField] private LayerMask whatDamagesEnemy;       // e.g. enemies
    [SerializeField] private float destroyTimer = 3f;

    [Header("Normal Arrow Stats")]
    [SerializeField] private float normalArrowSpeed = 15f;
    [SerializeField] private float normalArrowDamage = 2f;

    [Header("Pierce Arrow Stats")]
    [SerializeField] private float pierceArrowSpeed = 10f;
    [SerializeField] private float pierceArrowDamage = 2f;

    [Header("Split Arrow Stats")]
    [SerializeField] private float splitArrowSpeed = 10f;
    [SerializeField] private float splitArrowDamage = 1f;

    public int currentArrowType;
    private Rigidbody2D rb;
    private float damage;

    public enum ArrowType
    {
        Normal,
        Pierce,
        Split
    }

    public ArrowType arrowType;

    // Global selected arrow type (used by LaunchArrow)
    public static int SelectedArrowType { get; private set; } = 1; // 1 = Normal, 2 = Pierce, 3 = Split

    public static void SetSelectedArrowType(int type)
    {
        SelectedArrowType = type;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDestroyTime();
        InitializeArrowStats();
    }

    private void InitializeArrowStats()
    {
        if (arrowType == ArrowType.Normal)
        {
            SetStraightVelocity();
            damage = normalArrowDamage;
            currentArrowType = 1;
        }
        else if (arrowType == ArrowType.Pierce)
        {
            SetPierceVelocity();
            damage = pierceArrowDamage;
            currentArrowType = 2;
        }
        else if (arrowType == ArrowType.Split)
        {
            SetSplitVelocity();
            damage = splitArrowDamage;
            currentArrowType = 3;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layer = collision.gameObject.layer;

        if ((whatDamagesEnemy.value & (1 << layer)) == 0)
            return;

        IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Damage(damage);
        }

        // Only Normal and Split arrows die on first hit.
        // Pierce arrows keep going until their timer expires.
        if (arrowType != ArrowType.Pierce)
        {
            Destroy(gameObject);
        }
    }

    private void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * normalArrowSpeed;
    }

    private void SetPierceVelocity()
    {
        rb.linearVelocity = transform.right * pierceArrowSpeed;
    }

    private void SetSplitVelocity()
    {
        rb.linearVelocity = transform.right * splitArrowSpeed;
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, destroyTimer);
    }
}