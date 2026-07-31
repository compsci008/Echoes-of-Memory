using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [Header("General Arrow Stats")]
    [SerializeField] private LayerMask whatDestroysArrow;
    [SerializeField] private float destroyTimer = 3f;

    [Header("Normal Arrow Stats")]
    [SerializeField] private float normalArrowSpeed = 15f;

    [Header("Pierce Arrow Stats")]
    [SerializeField] private float pierceArrowSpeed = 10f;

    private Rigidbody2D rb;

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
        }

        else if (arrowType == ArrowType.Pierce)
        {
            SetPierceVelocity();
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

            //Destory Arrow
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetStraightVelocity()
    {
        rb.linearVelocity = transform.right * normalArrowSpeed;
    }

    void SetPierceVelocity()
    {
        rb.linearVelocity = transform.right * pierceArrowSpeed;
    }

    void SetDestoryTime()
    {
        Destroy(gameObject, destroyTimer);
    }
}
