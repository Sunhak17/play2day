using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;
    private bool jumpRequest;
    private int facingDirection = 1; // 1 = right, -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Get movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Detect jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequest = true;
        }
        
        // Alternative kick input (not requiring collision)
        if (Input.GetKeyDown(KeyCode.K))
        {
            TryKickBall();
        }
    }

    void FixedUpdate()
    {
        // Apply movement
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update facing direction based on movement
        if (moveInput > 0)
            facingDirection = 1; // Moving right
        else if (moveInput < 0)
            facingDirection = -1; // Moving left

        // Apply jump
        if (jumpRequest)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpRequest = false;
        }
    }
    void OnDrawGizmosSelected()
    {
        // Shows the ground check circle in the Scene View
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }

    void TryKickBall()
    {
        // Find ball nearby
        GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");
        if (ballObj != null)
        {
            float distance = Vector2.Distance(transform.position, ballObj.transform.position);
            if (distance < 2f) // Kick if within 2 units
            {
                Rigidbody2D ballRb = ballObj.GetComponent<Rigidbody2D>();
                if (ballRb != null)
                {
                    // Kick in the direction player is facing
                    Vector2 kickDirection = new Vector2(facingDirection, 0.3f).normalized;
                    ballRb.AddForce(kickDirection * 10f, ForceMode2D.Impulse);
                    Debug.Log("Ball kicked forward!");
                }
            }
        }
    }

    // Ball kicking logic: press K when near the ball
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (ballRb != null)
                {
                    // Kick in the direction player is facing
                    Vector2 kickDirection = new Vector2(facingDirection, 0.3f).normalized;
                    ballRb.AddForce(kickDirection * 500f, ForceMode2D.Impulse);
                }
            }
        }
    }
}
