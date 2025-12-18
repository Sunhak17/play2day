using UnityEngine;

public class TeamData : MonoBehaviour
{
    public static TeamData instance;

    // Selected teams
    public string playerTeamName = "";
    public string playerTeamShortName = "";
    public Sprite playerTeamLogo;
    public Sprite playerCharacterSprite;
    public Sprite playerTeamKit;
    public Sprite playerHeadSprite;
    public string aiTeamName = "";
    public string aiTeamShortName = "";
    public Sprite aiTeamLogo;
    public Sprite aiCharacterSprite;
    public Sprite aiTeamKit;
    public Sprite aiHeadSprite;

    // Selected customization
    public string selectedBall = "";
    public Sprite selectedBallSprite;
    public string selectedStadium = "";
    public Sprite selectedStadiumSprite;

    void Awake()
    {
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

    public void SetPlayerTeam(string teamName, string shortName, Sprite logo, Sprite playerSprite, Sprite kit, Sprite head)
    {
        playerTeamName = teamName;
        playerTeamShortName = shortName;
        playerTeamLogo = logo;
        playerCharacterSprite = playerSprite;
        playerTeamKit = kit;
        playerHeadSprite = head;
        Debug.Log("Player selected team: " + teamName + " (" + shortName + ")");
    }

    public void SetAITeam(string teamName, string shortName, Sprite logo, Sprite aiSprite, Sprite kit, Sprite head)
    {
        aiTeamName = teamName;
        aiTeamShortName = shortName;
        aiTeamLogo = logo;
        aiCharacterSprite = aiSprite;
        aiTeamKit = kit;
        aiHeadSprite = head;
        Debug.Log("AI selected team: " + teamName + " (" + shortName + ")");
    }

    public void SetCustomization(string ball, string stadium)
    {
        selectedBall = ball;
        selectedStadium = stadium;
    }

    public void SetCustomizationWithSprites(string ball, Sprite ballSprite, string stadium, Sprite stadiumSprite)
    {
        selectedBall = ball;
        selectedBallSprite = ballSprite;
        selectedStadium = stadium;
        selectedStadiumSprite = stadiumSprite;
    }
}
