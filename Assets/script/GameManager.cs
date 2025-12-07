using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject gameOverPanel;
    public TextMeshProUGUI winnerText;

    private bool gameEnded = false;

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
        Time.timeScale = 0f; // Pause the game

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        // Determine winner
        if (ScoreManager.instance != null)
        {
            if (ScoreManager.instance.playerScore > ScoreManager.instance.aiScore)
            {
                if (winnerText != null)
                    winnerText.text = "Player Wins!";
            }
            else if (ScoreManager.instance.aiScore > ScoreManager.instance.playerScore)
            {
                if (winnerText != null)
                    winnerText.text = "AI Wins!";
            }
            else
            {
                if (winnerText != null)
                    winnerText.text = "It's a Draw!";
            }
        }
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
