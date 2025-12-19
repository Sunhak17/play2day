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
            Debug.Log("[GameResult] Created and set to DontDestroyOnLoad. Current scores: Player " + playerScore + ", AI " + aiScore);
        }
        else
        {
            Debug.Log("[GameResult] Duplicate instance found - Current instance has Player: " + instance.playerScore + ", AI: " + instance.aiScore + " - destroying this duplicate");
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            Debug.LogError("[GameResult] MAIN INSTANCE IS BEING DESTROYED! Scores: Player " + playerScore + ", AI " + aiScore);
        }
    }

    public void SetMatchResult(int playerScoreValue, int aiScoreValue, string winnerValue, bool goldenGoal = false)
    {
        playerScore = playerScoreValue;
        aiScore = aiScoreValue;
        winner = winnerValue;
        wasGoldenGoal = goldenGoal;
        Debug.Log($"[GameResult] Match Result SET - Player: {playerScore}, AI: {aiScore}, Winner: {winner}, Golden Goal: {wasGoldenGoal}");
    }

    public void Reset()
    {
        playerScore = 0;
        aiScore = 0;
        winner = "";
        wasGoldenGoal = false;
    }
}
