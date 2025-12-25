using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI References")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;
    public GameObject goldenGoalPanel;
    public TextMeshProUGUI goldenGoalText;

    [Header("Golden Goal Settings")]
    public bool goldenGoalMode = false;
    public float goldenGoalTime = 30f; // 30 seconds for golden goal

    private bool gameEnded = false;
    private bool isGoldenGoal = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
        
        if (goldenGoalPanel != null)
            goldenGoalPanel.SetActive(false);

        // Check if this is golden goal mode
        if (goldenGoalMode)
        {
            StartGoldenGoal();
        }
    }

    void Update()
    {
        // Check if time is up
        time timeScript = FindObjectOfType<time>();
        if (timeScript != null && timeScript.GetTimeRemaining() <= 0 && !gameEnded)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("=== GAME ENDING ===");

        // Determine winner
        string winner = "";
        if (ScoreManager.instance == null)
        {
            Debug.LogError("ScoreManager.instance is NULL! Cannot read scores!");
            return;
        }

        int playerScore = ScoreManager.instance.playerScore;
        int aiScore = ScoreManager.instance.aiScore;

        Debug.Log($"Final Scores - Player: {playerScore}, AI: {aiScore}");

        if (playerScore > aiScore)
        {
            winner = "Player";
            if (winnerText != null)
                winnerText.text = "Player Wins!";
        }
        else if (aiScore > playerScore)
        {
            winner = "AI";
            if (winnerText != null)
                winnerText.text = "AI Wins!";
        }
        else
        {
            winner = "Draw";
            if (winnerText != null)
                winnerText.text = "It's a Draw!";
        }

        Debug.Log($"Winner determined: {winner}");

        // Check if it's a draw - start golden goal
        if (winner == "Draw" && !isGoldenGoal)
        {
            Debug.Log("Match is a draw! Starting Golden Goal mode...");
            StartGoldenGoalMode();
            return; // Don't pause the game or show game over panel
        }

        // Save results - ensure GameResult exists and persists
        if (GameResult.instance == null)
        {
            Debug.LogWarning("GameResult.instance was NULL! Creating new one...");
            GameObject resultObj = new GameObject("GameResult");
            GameResult result = resultObj.AddComponent<GameResult>();
            // Wait a frame to ensure Awake() completes
            Debug.Log("GameResult created, waiting for initialization...");
        }
        
        // Double check it exists
        if (GameResult.instance == null)
        {
            Debug.LogError("GameResult.instance is STILL NULL after creation! Cannot save scores!");
            return;
        }
        
        Debug.Log($"Saving match result: Player {playerScore} - {aiScore} AI, Winner: {winner}, Golden Goal: {isGoldenGoal}");
        GameResult.instance.SetMatchResult(playerScore, aiScore, winner, isGoldenGoal);
        Debug.Log($"Verified GameResult has scores: Player {GameResult.instance.playerScore}, AI {GameResult.instance.aiScore}");

        // Play pre-game audio when game ends (whistle/end sound)
        if (GameAudioManager.instance != null)
        {
            GameAudioManager.instance.PlayPreGameAudio();
        }

        // Go directly to After-Match scene after 2 seconds
        Debug.Log("Game ended! Going to After-Match scene...");
        Invoke("GoToAfterMatch", 2f);
    }

    void StartGoldenGoalMode()
    {
        isGoldenGoal = true;
        
        // Show golden goal notification
        if (goldenGoalPanel != null)
        {
            goldenGoalPanel.SetActive(true);
            if (goldenGoalText != null)
                goldenGoalText.text = "Golden Goal!\nNext goal wins!";
        }

        // Reset timer for golden goal
        time timeScript = FindObjectOfType<time>();
        if (timeScript != null)
        {
            timeScript.gameTime = goldenGoalTime;
            timeScript.ResetTimer();
        }

        // Reset game state
        gameEnded = false;
        
        // Hide the notification after 3 seconds
        Invoke("HideGoldenGoalPanel", 3f);
    }

    void StartGoldenGoal()
    {
        isGoldenGoal = true;
        if (goldenGoalText != null)
            goldenGoalText.text = "Golden Goal Mode\nFirst goal wins!";
    }

    void HideGoldenGoalPanel()
    {
        if (goldenGoalPanel != null)
            goldenGoalPanel.SetActive(false);
    }

    public void OnGoalScored()
    {
        // If in golden goal mode, immediately end the game
        if (isGoldenGoal)
        {
            Debug.Log("Golden goal scored! Ending game...");
            Invoke("EndGameAfterGoldenGoal", 2f); // Wait 2 seconds before ending
        }
    }

    void EndGameAfterGoldenGoal()
    {
        EndGame();
        Invoke("GoToAfterMatch", 2f);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Welcome");
    }

    public void GoToAfterMatch()
    {
        Time.timeScale = 1f;
        
        // Final verification before scene change
        if (GameResult.instance != null)
        {
            Debug.Log($"[GameManager] Before scene load - GameResult exists with Player: {GameResult.instance.playerScore}, AI: {GameResult.instance.aiScore}");
        }
        else
        {
            Debug.LogError("[GameManager] GameResult.instance is NULL before loading After-Match scene!");
        }
        
        SceneManager.LoadScene("After-Match");
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}
