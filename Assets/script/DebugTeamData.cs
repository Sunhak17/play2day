using UnityEngine;

public class DebugTeamData : MonoBehaviour
{
    void Start()
    {
        Debug.Log("=== TEAM DATA DEBUG ===");
        
        if (TeamData.instance == null)
        {
            Debug.LogError("TeamData.instance is NULL! TeamData GameObject might not exist or DontDestroyOnLoad failed.");
            return;
        }

        Debug.Log("TeamData instance found!");
        Debug.Log("Player Team: " + TeamData.instance.playerTeamName);
        Debug.Log("Player Logo: " + (TeamData.instance.playerTeamLogo != null ? "SET" : "NULL"));
        Debug.Log("Player Character: " + (TeamData.instance.playerCharacterSprite != null ? "SET" : "NULL"));
        Debug.Log("Player Kit: " + (TeamData.instance.playerTeamKit != null ? "SET" : "NULL"));
        
        Debug.Log("AI Team: " + TeamData.instance.aiTeamName);
        Debug.Log("AI Logo: " + (TeamData.instance.aiTeamLogo != null ? "SET" : "NULL"));
        Debug.Log("AI Character: " + (TeamData.instance.aiCharacterSprite != null ? "SET" : "NULL"));
        Debug.Log("AI Kit: " + (TeamData.instance.aiTeamKit != null ? "SET" : "NULL"));
        
        Debug.Log("Selected Ball: " + TeamData.instance.selectedBall);
        Debug.Log("Selected Stadium: " + TeamData.instance.selectedStadium);
        
        Debug.Log("======================");
    }
}
