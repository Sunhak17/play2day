using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigator : MonoBehaviour
{
    // Load scene by name (through loading screen)
    public void LoadScene(string sceneName)
    {
        Time.timeScale = 1f; // Reset time scale in case it was paused
        
        // Check if game has already started (skip loading screen)
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            // First time - use loading screen
            PlayerPrefs.SetString("TargetScene", sceneName);
            SceneManager.LoadScene("Loading");
        }
    }

    // Load specific scenes
    public void LoadWelcome()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("Welcome");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "Welcome");
            SceneManager.LoadScene("Loading");
        }
    }

    public void LoadSetting()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("Setting");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "Setting");
            SceneManager.LoadScene("Loading");
        }
    }

    public void LoadChoose()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("Team_selection");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "Team_selection");
            SceneManager.LoadScene("Loading");
        }
    }

    public void LoadChoose_Menu()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("Choose_Customization");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "Choose_Customization");
            SceneManager.LoadScene("Loading");
        }
    }

    public void LoadGamePlay()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("GamePlay");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "GamePlay");
            SceneManager.LoadScene("Loading");
        }
    }

    public void LoadAfterMatch()
    {
        if (PlayerPrefs.GetInt("GameStarted", 0) == 1)
        {
            SceneManager.LoadScene("After-Match");
        }
        else
        {
            PlayerPrefs.SetString("TargetScene", "After-Match");
            SceneManager.LoadScene("Loading");
        }
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
