using UnityEngine;
using TMPro;

public class AfterMatchDisplay : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI resultText;
    public GameObject goldenGoalIndicator;

    void Start()
    {
        DisplayResults();
    }

    void DisplayResults()
    {
        if (GameResult.instance != null)
        {
            // Display scores
            if (playerScoreText != null)
                playerScoreText.text = GameResult.instance.playerScore.ToString();
            
            if (aiScoreText != null)
                aiScoreText.text = GameResult.instance.aiScore.ToString();

            // Display result message
            if (resultText != null)
            {
                if (GameResult.instance.wasGoldenGoal)
                {
                    resultText.text = $"Golden Goal!\n{GameResult.instance.winner} Wins!";
                }
                else if (GameResult.instance.winner == "Player")
                {
                    resultText.text = "Victory!\nYou Win!";
                }
                else if (GameResult.instance.winner == "AI")
                {
                    resultText.text = "Defeat!\nAI Wins!";
                }
                else
                {
                    resultText.text = "Draw!\nNo Winner";
                }
            }

            // Show golden goal indicator if applicable
            if (goldenGoalIndicator != null)
            {
                goldenGoalIndicator.SetActive(GameResult.instance.wasGoldenGoal);
            }
        }
        else
        {
            Debug.LogWarning("GameResult instance not found!");
        }
    }
}
