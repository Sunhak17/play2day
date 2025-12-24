using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private Image loadingBar; // Assign a UI Image for the progress bar
    [SerializeField] private TextMeshProUGUI loadingText; // Optional: show loading percentage
    [SerializeField] private float loadingDuration = 3f; // How long the loading takes (in seconds)
    [SerializeField] private string targetScene = "Welcome"; // Scene to load after loading screen

    private AsyncOperation asyncLoad;

    void Start()
    {
        // Check if coming from first startup or scene transition
        string targetSceneName = PlayerPrefs.GetString("TargetScene", "");
        
        if (targetSceneName == "")
        {
            // First startup - show loading to Welcome
            targetScene = "Welcome";
            StartCoroutine(LoadSceneAsync());
        }
        else
        {
            // Scene transition during gameplay - skip loading and go directly
            SceneManager.LoadScene(targetSceneName);
            PlayerPrefs.DeleteKey("TargetScene");
        }
    }

    IEnumerator LoadSceneAsync()
    {
        // Start loading the target scene in the background
        asyncLoad = SceneManager.LoadSceneAsync(targetScene);
        asyncLoad.allowSceneActivation = false; // Don't switch scene yet

        float elapsedTime = 0f;
        float progress = 0f;

        // Simulate loading progress for the loading bar
        while (elapsedTime < loadingDuration)
        {
            elapsedTime += Time.deltaTime;
            progress = Mathf.Clamp01(elapsedTime / loadingDuration);

            // Update progress bar
            if (loadingBar != null)
            {
                loadingBar.fillAmount = progress;
            }

            // Update loading text (optional)
            if (loadingText != null)
            {
                loadingText.text = Mathf.RoundToInt(progress * 100f) + "%";
            }

            yield return null;
        }

        // Ensure bar is fully filled
        if (loadingBar != null)
            loadingBar.fillAmount = 1f;

        if (loadingText != null)
            loadingText.text = "100%";

        // Wait a moment before switching scene
        yield return new WaitForSeconds(0.5f);

        // Mark game as started
        PlayerPrefs.SetInt("GameStarted", 1);

        // Now allow the scene to activate
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
    }
}
