using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float jumpForce = 5f; // Reduced from 10 to match realistic jump

    [Header("AI Settings")]
    public Transform ball;
    public Transform targetGoal; // Assign the opponent's goal
    public float kickDistance = 1.0f; // Only kick when very close to ball
    public float kickForce = 6f;
    public float jumpChance = 0.1f; // Reduced to 10% chance - less frequent jumping

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.3f;
    public LayerMask groundLayer;

    [Header("Header Ability")]
    public Transform headCheck; // Assign a transform above the AI's head in the Inspector
    public float headRadius = 0.3f;
    public float headerForce = 15f;
    public float headerChance = 0.15f; // 15% chance to attempt header

    [Header("Animation")]
    public Animator animator;
    public Animator leftShoeAnimator;
    public string kickTriggerName = "Kick";

    private Rigidbody2D rb;
    private bool isGrounded;
    private float nextActionTime = 0f;
    private float lastKickTime = 0f;
    private float kickCooldown = 1f; // 1 second between kicks
    private Vector3 startPosition; // Store starting position

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = transform.position; // Save starting position
        
        // Auto-find ball if not assigned
        if (ball == null)
        {
            GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");
            if (ballObj != null)
                ball = ballObj.transform;
        }

        // Auto-find Animator on children (shoes)
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator != null)
                Debug.Log("AI Animator found on: " + animator.gameObject.name);
        }
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
        
        // AI decision making
        if (Time.time >= nextActionTime)
        {
            AIBehavior();
            nextActionTime = Time.time + 0.2f; // Update every 0.2 seconds
        }
    }

    void FixedUpdate()
    {
        // Movement is handled in AIBehavior
    }

    void AIBehavior()
    {
        if (ball == null) return;

        float distanceToBall = Vector2.Distance(transform.position, ball.position);
        
        // Move towards ball
        if (distanceToBall > kickDistance)
        {
            MoveTowardsBall();
        }
        // Stop and kick if close enough
        else if (distanceToBall <= kickDistance)
        {
            // Stop moving
            rb.velocity = new Vector2(0, rb.velocity.y);
            
            Debug.Log("AI near ball! Distance: " + distanceToBall + " | Cooldown ready: " + (Time.time >= lastKickTime + kickCooldown));
            
            // Try to kick with cooldown
            if (Time.time >= lastKickTime + kickCooldown)
            {
                TryKickBall();
                lastKickTime = Time.time;
            }
        }
        else
        {
            // Ball is somewhat close but not in kick range - keep moving
            MoveTowardsBall();
        }
    }

    void MoveTowardsBall()
    {
        float direction = Mathf.Sign(ball.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        // Jump if ball is significantly above AI, AI is grounded, and not already jumping
        float heightDifference = ball.position.y - transform.position.y;
        if (heightDifference > 2f && isGrounded && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            if (Random.value < jumpChance) // 10% chance to make it less predictable
            {
                Jump();
                TryHeader(); // Check for header when AI jumps
            }
        }
    }

    void TryKickBall()
    {
        if (ball == null)
        {
            Debug.Log("AI: Ball is null!");
            return;
        }
        
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            // Kick towards goal if assigned, otherwise kick away from AI
            Vector2 kickDirection;
            if (targetGoal != null)
            {
                kickDirection = (targetGoal.position - transform.position).normalized;
            }
            else
            {
                kickDirection = (ball.position - transform.position).normalized;
            }
            
            ballRb.AddForce(kickDirection * kickForce, ForceMode2D.Impulse);
            Debug.Log("AI kicked the ball with force: " + kickForce);

            // Play kick animation
            if (animator != null)
            {
                animator.SetTrigger(kickTriggerName);
                if (leftShoeAnimator != null)
                    leftShoeAnimator.SetTrigger(kickTriggerName);
            }
        }
        else
        {
            Debug.Log("AI: Ball has no Rigidbody2D!");
        }
    }

    void Jump()
    {
        if (isGrounded && Mathf.Abs(rb.velocity.y) < 0.1f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("AI jumped!");
        }
    }

    void TryHeader()
    {
        // Only try header if headCheck is assigned
        if (headCheck == null) return;

        if (ball != null)
        {
            float distance = Vector2.Distance(headCheck.position, ball.position);
            Debug.Log($"AI head to ball distance: {distance:F2}");
            if (distance < headRadius + 0.3f) // Header if ball is close to head
            {
                if (Random.value < headerChance) // Add some randomness
                {
                    Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
                    if (ballRb != null)
                    {
                        // Header direction: towards goal if assigned, otherwise forward
                        Vector2 headerDirection;
                        if (targetGoal != null)
                        {
                            headerDirection = (targetGoal.position - transform.position).normalized;
                        }
                        else
                        {
                            float dir = Mathf.Sign(ball.position.x - transform.position.x);
                            headerDirection = new Vector2(dir, 1f).normalized;
                        }
                        
                        ballRb.AddForce(headerDirection * headerForce, ForceMode2D.Impulse);
                        Debug.Log("AI header!");
                    }
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Visualize kick distance
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, kickDistance);
        
        // Visualize ground check
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }

        // Visualize head check
        if (headCheck != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(headCheck.position, headRadius);
        }
    }

    public void ResetPosition()
    {
        // Reset AI to starting position
        transform.position = startPosition;
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
        Debug.Log("AI reset to starting position");
    }
}