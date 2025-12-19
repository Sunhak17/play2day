using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float jumpForce = 10f;

    [Header("AI Settings")]
    public Transform ball;
    public Transform targetGoal; // Assign the opponent's goal
    public float kickDistance = 1.0f; // Only kick when very close to ball
    public float kickForce = 15f;
    public float jumpChance = 0.3f; // 30% chance to jump when near ball

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.3f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float nextActionTime = 0f;
    private float lastKickTime = 0f;
    private float kickCooldown = 1f; // 1 second between kicks

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Auto-find ball if not assigned
        if (ball == null)
        {
            GameObject ballObj = GameObject.FindGameObjectWithTag("Ball");
            if (ballObj != null)
                ball = ballObj.transform;
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
    }
}