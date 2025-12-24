using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    // Load scene by name (through loading screen)
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        PlayerPrefs.SetString("TargetScene", sceneName);
        SceneManager.LoadScene("Loading");
    }

    // Load specific scenes
    public void LoadWelcome()
    {
        PlayerPrefs.SetString("TargetScene", "Welcome");
        SceneManager.LoadScene("Loading");
    }

    public void LoadSetting()
    {
        PlayerPrefs.SetString("TargetScene", "Setting");
        SceneManager.LoadScene("Loading");
    }

    public void LoadChoose()
    {
        PlayerPrefs.SetString("TargetScene", "Team_selection");
        SceneManager.LoadScene("Loading");
    }

    public void LoadChoose_Menu()
    {
        PlayerPrefs.SetString("TargetScene", "Choose_Customization");
        SceneManager.LoadScene("Loading");
    }

    public void LoadGamePlay()
    {
        PlayerPrefs.SetString("TargetScene", "GamePlay");
        SceneManager.LoadScene("Loading");
    }

    public void LoadAfterMatch()
    {
        PlayerPrefs.SetString("TargetScene", "After-Match");
        SceneManager.LoadScene("Loading");
    }

    // Reload current scene
    public void RestartCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Quit game
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    // Go back to main menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Welcome");
    }
}
