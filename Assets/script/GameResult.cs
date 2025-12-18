using UnityEngine;

public class GameResult : MonoBehaviour
{
    public static GameResult instance;

    // Match results
    public int playerScore = 0;
    public int aiScore = 0;
    public string winner = ""; // "Player", "AI", or "Draw"
    public bool wasGoldenGoal = false;

    void Awake()
    {
        // Keep this object alive between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMatchResult(int playerScoreValue, int aiScoreValue, string winnerValue, bool goldenGoal = false)
    {
        playerScore = playerScoreValue;
        aiScore = aiScoreValue;
        winner = winnerValue;
        wasGoldenGoal = goldenGoal;
        Debug.Log($"Match Result Set - Player: {playerScore}, AI: {aiScore}, Winner: {winner}");
    }

    public void Reset()
    {
        playerScore = 0;
        aiScore = 0;
        winner = "";
        wasGoldenGoal = false;
    }
}
