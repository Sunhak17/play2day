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

    [Header("Animation")]
    public Animator animator;
    public Animator leftShoeAnimator; // Optional: for left shoe
    public string kickTriggerName = "Kick"; // Name of the trigger in your Animation Controller

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private float moveInput;
    private bool jumpRequest;
    private int facingDirection = 1; // 1 = right, -1 = left

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Auto-find Animator component if not assigned
        if (animator == null)
        {
            // Try to find on this GameObject first
            animator = GetComponent<Animator>();
            
            // If not found, search in children (shoes)
            if (animator == null)
            {
                animator = GetComponentInChildren<Animator>();
            }
            
            if (animator == null)
            {
                Debug.LogError("No Animator component found on Player or its children! Please add Animator to shoes.");
            }
            else
            {
                Debug.Log("Animator found on: " + animator.gameObject.name + " | Trigger name set to: " + kickTriggerName);
            }
        }
    }

    void Update()
    {
        // Ground check
        wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Get movement input
        moveInput = Input.GetAxisRaw("Horizontal");

        // Detect jump - only when grounded and not already jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb.velocity.y <= 0.1f)
        {
            jumpRequest = true;
        }
        
        // Alternative kick input (not requiring collision)
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K key pressed - attempting kick");
            
            // Play animation every time K is pressed
            if (animator != null)
            {
                Debug.Log("Triggering animation: " + kickTriggerName);
                animator.SetTrigger(kickTriggerName);
                
                // Trigger left shoe too if assigned
                if (leftShoeAnimator != null)
                    leftShoeAnimator.SetTrigger(kickTriggerName);
            }
            
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
            Debug.Log($"Player to ball distance: {distance:F2}");
            if (distance < 1.2f) // Kick only if very close to feet
            {
                Rigidbody2D ballRb = ballObj.GetComponent<Rigidbody2D>();
                if (ballRb != null)
                {
                    // Kick in the direction player is facing
                    Vector2 kickDirection = new Vector2(facingDirection, 0.5f).normalized;
                    ballRb.AddForce(kickDirection * 20f, ForceMode2D.Impulse);
                    Debug.Log("Player kicked!");
                }
            }
            else
            {
                Debug.Log($"Ball too far! Distance: {distance:F2} (need < 1.2)");
            }
        }
    }
}
