using UnityEngine;

public class GoalDetection : MonoBehaviour
{
    public bool isPlayerGoal; // True if this is player's goal (AI scores), False if AI's goal (player scores)

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
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

            // Reset ball position
            ResetBall(other.gameObject);
        }
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
}
