using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 5f;

    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement.normalized * moveSpeed;
    }
}