using UnityEngine;

public class AIPlayer : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 4f;
    public float jumpForce = 10f;

    [Header("AI Settings")]
    public Transform ball;
    public Transform targetGoal; // Assign the opponent's goal
    public float kickDistance = 1.5f;
    public float kickForce = 15f;
    public float jumpChance = 0.3f; // 30% chance to jump when near ball

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.3f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;
    private float nextActionTime = 0f;

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
        // Kick if close enough
        else
        {
            TryKickBall();
            
            // Random jump when near ball
            if (isGrounded && Random.value < jumpChance)
            {
                Jump();
            }
        }
    }

    void MoveTowardsBall()
    {
        float direction = Mathf.Sign(ball.position.x - transform.position.x);
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    void TryKickBall()
    {
        if (ball == null) return;
        
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
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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