using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterMatchDisplay : MonoBehaviour
{
    [Header("Score UI")]
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    public TextMeshProUGUI resultText;
    public GameObject goldenGoalIndicator;

    [Header("Team Display")]
    public Image playerTeamLogo;
    public TextMeshProUGUI playerTeamNameText;
    public Image aiTeamLogo;
    public TextMeshProUGUI aiTeamNameText;

    void Start()
    {
        Debug.Log("=== AFTER-MATCH SCENE LOADED ===");
        Debug.Log("Checking GameResult.instance: " + (GameResult.instance != null ? "EXISTS" : "NULL"));
        Debug.Log("Checking ScoreManager.instance: " + (ScoreManager.instance != null ? "EXISTS" : "NULL"));
        
        if (GameResult.instance != null)
        {
            Debug.Log($"GameResult data: Player {GameResult.instance.playerScore}, AI {GameResult.instance.aiScore}");
        }
        if (ScoreManager.instance != null)
        {
            Debug.Log($"ScoreManager data: Player {ScoreManager.instance.playerScore}, AI {ScoreManager.instance.aiScore}");
        }
        
        DisplayResults();
        DisplayTeams();
    }

    void DisplayResults()
    {
        // Try GameResult first, fallback to ScoreManager
        int playerScore = 0;
        int aiScore = 0;
        string winner = "";
        bool wasGoldenGoal = false;

        if (GameResult.instance != null)
        {
            Debug.Log("[AfterMatch] Using GameResult for scores");
            playerScore = GameResult.instance.playerScore;
            aiScore = GameResult.instance.aiScore;
            winner = GameResult.instance.winner;
            wasGoldenGoal = GameResult.instance.wasGoldenGoal;
        }
        else if (ScoreManager.instance != null)
        {
            Debug.Log("[AfterMatch] GameResult NULL, using ScoreManager for scores");
            playerScore = ScoreManager.instance.playerScore;
            aiScore = ScoreManager.instance.aiScore;
            
            // Determine winner from scores
            if (playerScore > aiScore)
                winner = "Player";
            else if (aiScore > playerScore)
                winner = "AI";
            else
                winner = "Draw";
        }
        else
        {
            Debug.LogError("[AfterMatch] Both GameResult and ScoreManager are NULL! Cannot display scores.");
            return;
        }

        Debug.Log($"[AfterMatch] Displaying scores - Player: {playerScore}, AI: {aiScore}, Winner: {winner}");

        // Display scores
        if (playerScoreText == null)
        {
            Debug.LogError("playerScoreText is NOT assigned in AfterMatchDisplay!");
        }
        else
        {
            playerScoreText.text = playerScore.ToString();
            Debug.Log($"Set player score text to: {playerScoreText.text}");
        }
        
        if (aiScoreText == null)
        {
            Debug.LogError("aiScoreText is NOT assigned in AfterMatchDisplay!");
        }
        else
        {
            aiScoreText.text = aiScore.ToString();
            Debug.Log($"Set AI score text to: {aiScoreText.text}");
        }

        // Display result message
        if (resultText != null)
        {
            if (wasGoldenGoal)
            {
                resultText.text = $"Golden Goal!\n{winner} Wins!";
            }
            else if (winner == "Player")
            {
                resultText.text = "Victory!\nYou Win!";
            }
            else if (winner == "AI")
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
            goldenGoalIndicator.SetActive(wasGoldenGoal);
        }

        Debug.Log("Results displayed successfully!");
    }

    void DisplayTeams()
    {
        if (TeamData.instance != null)
        {
            // Display player team
            if (playerTeamLogo != null && TeamData.instance.playerTeamLogo != null)
                playerTeamLogo.sprite = TeamData.instance.playerTeamLogo;
            
            if (playerTeamNameText != null)
                playerTeamNameText.text = TeamData.instance.playerTeamName;

            // Display AI team
            if (aiTeamLogo != null && TeamData.instance.aiTeamLogo != null)
                aiTeamLogo.sprite = TeamData.instance.aiTeamLogo;
            
            if (aiTeamNameText != null)
                aiTeamNameText.text = TeamData.instance.aiTeamName;

            Debug.Log($"Teams displayed - {TeamData.instance.playerTeamName} vs {TeamData.instance.aiTeamName}");
        }
        else
        {
            Debug.LogWarning("TeamData instance not found!");
        }
    }

    public void OnRematchButtonClicked()
    {
        // Reset scores but keep teams
        if (GameResult.instance != null)
            GameResult.instance.Reset();
        
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.playerScore = 0;
            ScoreManager.instance.aiScore = 0;
        }

        // Reload gameplay scene
        SceneManager.LoadScene("GamePlay");
        Debug.Log("Rematch started!");
    }

    public void OnChangeTeamsButtonClicked()
    {
        // Reset everything and go back to team selection
        if (GameResult.instance != null)
            GameResult.instance.Reset();
        
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.playerScore = 0;
            ScoreManager.instance.aiScore = 0;
        }

        SceneManager.LoadScene("Choose_Menu");
        Debug.Log("Going to team selection...");
    }

    public void OnMainMenuButtonClicked()
    {
        // Go to welcome screen
        SceneManager.LoadScene("Welcome");
        Debug.Log("Going to main menu...");
    }
}
