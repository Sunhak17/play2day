using UnityEngine;
using System.Collections;

public class GoalDetection : MonoBehaviour
{
    public bool isPlayerGoal; // True if this is player's goal (AI scores), False if AI's goal (player scores)
    public GameObject goalAnimation; // Drag your goal animation GameObject here
    public float animationDuration = 2f; // Duration to show animation

    private bool isProcessingGoal = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball") && !isProcessingGoal)
        {
            StartCoroutine(HandleGoal(other.gameObject));
        }
    }

    IEnumerator HandleGoal(GameObject ball)
    {
        isProcessingGoal = true;

        // IMMEDIATELY play goal audio (no delay)
        if (GameAudioManager.instance != null)
        {
            GameAudioManager.instance.PlayGoalAudio();
        }

        // Pause the game timer for 3 seconds
        time timeScript = FindObjectOfType<time>();
        if (timeScript != null)
        {
            timeScript.PauseTimer();
        }

        // Stop the ball
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector2.zero;
            ballRb.angularVelocity = 0f;
        }

        // Update score
        if (isPlayerGoal)
        {
            // AI scored
            ScoreManager.instance.AddAIScore();
            Debug.Log("AI scored!");
        }
        else
        {
            // Player scored
            ScoreManager.instance.AddPlayerScore();
            Debug.Log("Player scored!");
        }

        // Notify GameManager about goal (for golden goal mode)
        if (GameManager.instance != null)
        {
            GameManager.instance.OnGoalScored();
        }

        // Show goal animation
        if (goalAnimation != null)
        {
            goalAnimation.SetActive(true);
        }

        // Wait for 3 seconds (goal celebration time)
        yield return new WaitForSeconds(3f);

        // Resume the game timer
        if (timeScript != null)
        {
            timeScript.ResumeTimer();
        }

        // Hide goal animation
        if (goalAnimation != null)
        {
            goalAnimation.SetActive(false);
        }

        // Reset ball position
        ResetBall(ball);

        // Reset player positions
        ResetPlayers();

        isProcessingGoal = false;
    }

    void ResetBall(GameObject ball)
    {
        ball.transform.position = Vector3.zero;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void ResetPlayers()
    {
        // Find and reset player
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            player.ResetPosition();
        }

        // Find and reset AI
        AIPlayer ai = FindObjectOfType<AIPlayer>();
        if (ai != null)
        {
            ai.ResetPosition();
        }
    }
}
