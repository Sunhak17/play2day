using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomizationManager : MonoBehaviour
{
    [Header("Selectors")]
    public CustomizationSelector ballSelector;
    public CustomizationSelector stadiumSelector;

    [Header("Team Display")]
    public Image playerTeamLogo;
    public TextMeshProUGUI playerTeamName;
    public Image playerCharacterImage;
    public Image aiTeamLogo;
    public TextMeshProUGUI aiTeamName;
    public Image aiCharacterImage;

    void Start()
    {
        LoadTeamData();
    }

    void LoadTeamData()
    {
        // Display selected teams from previous scene
        if (TeamData.instance != null)
        {
            // Player team
            if (playerTeamLogo != null)
                playerTeamLogo.sprite = TeamData.instance.playerTeamLogo;
            
            if (playerTeamName != null)
                playerTeamName.text = TeamData.instance.playerTeamName;

            if (playerCharacterImage != null)
                playerCharacterImage.sprite = TeamData.instance.playerCharacterSprite;

            // AI team
            if (aiTeamLogo != null)
                aiTeamLogo.sprite = TeamData.instance.aiTeamLogo;
            
            if (aiTeamName != null)
                aiTeamName.text = TeamData.instance.aiTeamName;

            if (aiCharacterImage != null)
                aiCharacterImage.sprite = TeamData.instance.aiCharacterSprite;

            Debug.Log("Loaded teams - Player: " + TeamData.instance.playerTeamName + ", AI: " + TeamData.instance.aiTeamName);
        }
        else
        {
            Debug.LogWarning("TeamData not found! Make sure teams were selected in previous scene.");
        }
    }

    public void OnStartButtonClicked()
    {
        // Save all customization selections
        if (TeamData.instance != null)
        {
            string ball = ballSelector != null ? ballSelector.GetSelectedItem() : "";
            Sprite ballSprite = ballSelector != null ? ballSelector.GetSelectedSprite() : null;
            string stadium = stadiumSelector != null ? stadiumSelector.GetSelectedItem() : "";
            Sprite stadiumSprite = stadiumSelector != null ? stadiumSelector.GetSelectedSprite() : null;

            TeamData.instance.SetCustomizationWithSprites(ball, ballSprite, stadium, stadiumSprite);

            Debug.Log($"Customization saved - Ball: {ball}, Stadium: {stadium}");
            Debug.Log($"Player characters - Player: {TeamData.instance.playerTeamName}, AI: {TeamData.instance.aiTeamName}");
        }

        // Load gameplay scene
        SceneManager.LoadScene("GamePlay");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("Choose_Menu");
    }
}
