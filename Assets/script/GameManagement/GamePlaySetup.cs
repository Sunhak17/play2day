using UnityEngine;
using UnityEngine.UI;

public class GamePlaySetup : MonoBehaviour
{
    [Header("Player References")]
    public SpriteRenderer playerSpriteRenderer; // Body/Kit
    public SpriteRenderer playerHeadRenderer; // Head
    public GameObject playerObject;
    public Image playerTeamLogoUI; // UI Image for player logo
    public TMPro.TextMeshProUGUI playerTeamNameText; // Team name text

    [Header("AI References")]
    public SpriteRenderer aiSpriteRenderer; // Body/Kit
    public SpriteRenderer aiHeadRenderer; // Head
    public GameObject aiObject;
    public Image aiTeamLogoUI; // UI Image for AI logo
    public TMPro.TextMeshProUGUI aiTeamNameText; // Team name text

    [Header("Ball Reference")]
    public SpriteRenderer ballSpriteRenderer;

    [Header("Stadium Reference")]
    public SpriteRenderer stadiumBackground;

    void Start()
    {
        LoadGameData();
    }

    void LoadGameData()
    {
        if (TeamData.instance == null)
        {
            Debug.LogWarning("TeamData not found! Using default settings.");
            return;
        }

        // Apply Player Team Kit
        if (playerSpriteRenderer != null && TeamData.instance.playerTeamKit != null)
        {
            playerSpriteRenderer.sprite = TeamData.instance.playerTeamKit;
            Debug.Log("Applied player kit: " + TeamData.instance.playerTeamName);
        }

        // Apply Player Head
        if (playerHeadRenderer != null && TeamData.instance.playerHeadSprite != null)
        {
            playerHeadRenderer.sprite = TeamData.instance.playerHeadSprite;
            Debug.Log("Applied player head");
        }

        // Apply Player Team Logo to UI
        if (playerTeamLogoUI != null && TeamData.instance.playerTeamLogo != null)
        {
            playerTeamLogoUI.sprite = TeamData.instance.playerTeamLogo;
            Debug.Log("Applied player logo");
        }

        // Apply Player Team Name (short version)
        if (playerTeamNameText != null)
        {
            playerTeamNameText.text = TeamData.instance.playerTeamShortName;
            Debug.Log("Applied player team name: " + TeamData.instance.playerTeamShortName);
        }

        // Apply AI Team Kit
        if (aiSpriteRenderer != null && TeamData.instance.aiTeamKit != null)
        {
            aiSpriteRenderer.sprite = TeamData.instance.aiTeamKit;
            Debug.Log("Applied AI kit: " + TeamData.instance.aiTeamName);
        }

        // Apply AI Head
        if (aiHeadRenderer != null && TeamData.instance.aiHeadSprite != null)
        {
            aiHeadRenderer.sprite = TeamData.instance.aiHeadSprite;
            Debug.Log("Applied AI head");
        }

        // Apply AI Team Logo to UI
        if (aiTeamLogoUI != null && TeamData.instance.aiTeamLogo != null)
        {
            aiTeamLogoUI.sprite = TeamData.instance.aiTeamLogo;
            Debug.Log("Applied AI logo");
        }

        // Apply AI Team Name (short version)
        if (aiTeamNameText != null)
        {
            aiTeamNameText.text = TeamData.instance.aiTeamShortName;
            Debug.Log("Applied AI team name: " + TeamData.instance.aiTeamShortName);
        }

        // Apply Ball
        if (ballSpriteRenderer == null)
        {
            Debug.LogError("Ball SpriteRenderer is NOT assigned in GamePlaySetup!");
        }
        else if (TeamData.instance.selectedBallSprite == null)
        {
            Debug.LogWarning("No ball sprite selected! Using default ball. Ball name: " + TeamData.instance.selectedBall);
        }
        else
        {
            ballSpriteRenderer.sprite = TeamData.instance.selectedBallSprite;
            Debug.Log("Applied ball: " + TeamData.instance.selectedBall + " (Sprite: " + TeamData.instance.selectedBallSprite.name + ")");
        }

        // Apply Stadium
        if (stadiumBackground != null && TeamData.instance.selectedStadiumSprite != null)
        {
            stadiumBackground.sprite = TeamData.instance.selectedStadiumSprite;
            Debug.Log("Applied stadium: " + TeamData.instance.selectedStadium);
        }

        Debug.Log($"Game Setup Complete - Player: {TeamData.instance.playerTeamName} vs AI: {TeamData.instance.aiTeamName}");
        Debug.Log($"Ball: {TeamData.instance.selectedBall}, Stadium: {TeamData.instance.selectedStadium}");
    }
}
