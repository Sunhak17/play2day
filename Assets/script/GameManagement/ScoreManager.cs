using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int playerScore = 0;
    public int aiScore = 0;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("ScoreManager created and set to DontDestroyOnLoad");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPlayerScore()
    {
        playerScore++;
        Debug.Log($"Player scored! New score - Player: {playerScore}, AI: {aiScore}");
        UpdateScoreUI();
    }

    public void AddAIScore()
    {
        aiScore++;
        Debug.Log($"AI scored! New score - Player: {playerScore}, AI: {aiScore}");
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (playerScoreText != null)
            playerScoreText.text = playerScore.ToString();
        if (aiScoreText != null)
            aiScoreText.text = aiScore.ToString();
    }

    public void ResetScores()
    {
        playerScore = 0;
        aiScore = 0;
        UpdateScoreUI();
    }
}
