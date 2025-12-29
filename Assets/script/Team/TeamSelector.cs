using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Team
{
    public string teamName;
    public string teamShortName; // e.g., "ATG" for "Angkor Tiger"
    public Sprite teamLogo;
    public Sprite playerCharacter; // Player sprite for this team
    public Sprite teamKit; // Team uniform/jersey
    public Sprite playerHead; // Head sprite for this team
}

public class TeamSelector : MonoBehaviour
{
    [Header("Team Settings")]
    public Team[] teams; // Array of 11 teams
    public bool isPlayerSelector = true; // True for player, false for AI

    [Header("UI References")]
    public Image teamLogoImage;
    public TextMeshProUGUI teamNameText;

    private int currentTeamIndex = 0;

    void Start()
    {
        // Initialize with first team
        if (teams.Length > 0)
        {
            UpdateDisplay();
        }
        else
        {
            Debug.LogWarning("No teams assigned to TeamSelector!");
        }
    }

    public void NextTeam()
    {
        if (teams.Length == 0) return;

        currentTeamIndex++;
        if (currentTeamIndex >= teams.Length)
            currentTeamIndex = 0;

        UpdateDisplay();
    }

    public void PreviousTeam()
    {
        if (teams.Length == 0) return;

        currentTeamIndex--;
        if (currentTeamIndex < 0)
            currentTeamIndex = teams.Length - 1;

        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        Team currentTeam = teams[currentTeamIndex];

        // Update UI
        if (teamLogoImage != null)
            teamLogoImage.sprite = currentTeam.teamLogo;

        if (teamNameText != null)
            teamNameText.text = currentTeam.teamName;

    }

    public void ConfirmSelection()
    {
        if (teams.Length == 0) return;

        Team selectedTeam = teams[currentTeamIndex];

        // Save to TeamData
        if (TeamData.instance == null)
        {
            GameObject teamDataObj = new GameObject("TeamData");
            teamDataObj.AddComponent<TeamData>();
        }

        if (isPlayerSelector)
        {
            TeamData.instance.SetPlayerTeam(selectedTeam.teamName, selectedTeam.teamShortName, selectedTeam.teamLogo, selectedTeam.playerCharacter, selectedTeam.teamKit, selectedTeam.playerHead);
        }
        else
        {
            TeamData.instance.SetAITeam(selectedTeam.teamName, selectedTeam.teamShortName, selectedTeam.teamLogo, selectedTeam.playerCharacter, selectedTeam.teamKit, selectedTeam.playerHead);
        }
    }

    public Team GetCurrentTeam()
    {
        if (teams.Length > 0)
            return teams[currentTeamIndex];
        return null;
    }
}
