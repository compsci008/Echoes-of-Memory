using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [Header("General Arrow Stats")]
    [SerializeField] private LayerMask whatDestroysArrow;
    [SerializeField] private float destroyTimer = 3f;

    [Header("Normal Arrow Stats")]
    [SerializeField] private float normalArrowSpeed = 15f;
    [SerializeField] private float normalArrowDamage = 2f;

    [Header("Pierce Arrow Stats")]
    [SerializeField] private float pierceArrowSpeed = 10f;
    [SerializeField] private float pierceArrowDamage = 1f;

    private Rigidbody2D rb;
    private float damage;

    public enum ArrowType
    {
        Normal,
        Pierce
    }
    public ArrowType arrowType;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        SetDestoryTime();

        InitializeArrowStats();
    }

    private void InitializeArrowStats()
    {
        if (arrowType == ArrowType.Normal)
        {
            SetStraightVelocity();
            damage = normalArrowDamage;
        }

        else if (arrowType == ArrowType.Pierce)
        {
            SetPierceVelocity();
            damage = pierceArrowDamage;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysArrow.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Screen Shake

            //Spawn Particles

            //Play sound

            //Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                //Damage Enemy
                iDamageable.Damage(damage);
            }

            //Destory Arrow
            Destroy(gameObject);

            //Debug
            Debug.Log("Arrow hit: " + collision.name);
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

    private void SetDestoryTime()
    {
        Destroy(gameObject, destroyTimer);
    }
}
