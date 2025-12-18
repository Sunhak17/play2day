using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    // Load scene by name
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        SceneManager.LoadScene(sceneName);
    }

    // Load specific scenes
    public void LoadWelcome()
    {
        SceneManager.LoadScene("Welcome");
    }

    public void LoadSetting()
    {
        SceneManager.LoadScene("Setting");
    }

    public void LoadChoose()
    {
        SceneManager.LoadScene("Team_selection");
    }

    public void LoadChoose_Menu()
    {
        SceneManager.LoadScene("Choose_Customization");
    }

    public void LoadGamePlay()
    {
        SceneManager.LoadScene("GamePlay");
    }

    public void LoadAfterMatch()
    {
        SceneManager.LoadScene("After-Match");
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
