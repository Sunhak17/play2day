using UnityEngine;
using UnityEngine.SceneManagement;

public class TeamSelectionManager : MonoBehaviour
{
    [Header("Team Selectors")]
    public TeamSelector playerTeamSelector;
    public TeamSelector aiTeamSelector;

    [Header("Navigation")]
    public string nextSceneName = "Choose_Customization"; // Scene for player/ball/stadium selection

    public void OnNextButtonClicked()
    {
        // Confirm both team selections
        if (playerTeamSelector != null)
            playerTeamSelector.ConfirmSelection();

        if (aiTeamSelector != null)
            aiTeamSelector.ConfirmSelection();

        // Go to next scene (choose player, ball, stadium)
        LoadNextScene();
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Welcome");
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set!");
        }
    }

    // Optional: Validate that different teams are selected
    public bool ValidateSelections()
    {
        if (playerTeamSelector == null || aiTeamSelector == null)
            return false;

        Team playerTeam = playerTeamSelector.GetCurrentTeam();
        Team aiTeam = aiTeamSelector.GetCurrentTeam();

        if (playerTeam == null || aiTeam == null)
            return false;

        // Check if same team is selected for both
        if (playerTeam.teamName == aiTeam.teamName)
        {
            Debug.LogWarning("Player and AI cannot select the same team!");
            return false;
        }

        return true;
    }
}
