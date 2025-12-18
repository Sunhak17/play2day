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

        // Determine winner
        string winner = "";
        if (ScoreManager.instance != null)
        {
            int playerScore = ScoreManager.instance.playerScore;
            int aiScore = ScoreManager.instance.aiScore;

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

            // Check if it's a draw - start golden goal
            if (winner == "Draw" && !isGoldenGoal)
            {
                Debug.Log("Match is a draw! Starting Golden Goal mode...");
                StartGoldenGoalMode();
                return; // Don't pause the game or show game over panel
            }

            // Save results
            if (GameResult.instance == null)
            {
                GameObject resultObj = new GameObject("GameResult");
                resultObj.AddComponent<GameResult>();
            }
            
            GameResult.instance.SetMatchResult(playerScore, aiScore, winner, isGoldenGoal);
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
