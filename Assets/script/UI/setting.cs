using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GameSettings : MonoBehaviour
{
    [Header("Audio Settings")]
    public Toggle musicToggle;
    public Toggle sfxToggle;

    private bool isInitialized = false;

    void Awake()
    {
        // FORCE reset to defaults on first run or if you want to reset
        // Uncomment these lines to reset everything, then comment them back:
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        
        // Ensure default volumes are set (full volume)
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 0.8f);
            PlayerPrefs.Save();
        }
        
        // Ensure default states are ON if never set
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1); // 1 = ON by default
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("SFXOn"))
        {
            PlayerPrefs.SetInt("SFXOn", 1); // 1 = ON by default
            PlayerPrefs.Save();
        }
    }

    void Start()
    {
        // Load saved music settings from PlayerPrefs (default to ON)
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        Debug.Log($"Loading Settings - MusicOn PlayerPrefs value: {PlayerPrefs.GetInt("MusicOn", 1)}");
        
        // Set the toggle to match saved state WITHOUT triggering OnValueChanged
        if (musicToggle != null)
        {
            musicToggle.SetIsOnWithoutNotify(musicOn);
            
            // Remove any existing listeners to prevent duplicates
            musicToggle.onValueChanged.RemoveAllListeners();
            // Add our listener
            musicToggle.onValueChanged.AddListener(OnMusicToggled);
        }

        // Load saved SFX settings from PlayerPrefs (default to ON)
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        
        // Set the toggle to match saved state WITHOUT triggering OnValueChanged
        if (sfxToggle != null)
        {
            sfxToggle.SetIsOnWithoutNotify(sfxOn);
            
            // Remove any existing listeners to prevent duplicates
            sfxToggle.onValueChanged.RemoveAllListeners();
            // Add our listener
            sfxToggle.onValueChanged.AddListener(OnSFXToggled);
        }

        // Mark as initialized BEFORE applying settings
        isInitialized = true;

        // DON'T change the audio state - just sync the UI with current state
        // The MusicManager should already be playing if it was on
        Debug.Log($"Settings loaded - Music: {musicOn}, SFX: {sfxOn}");
    }

    public void OnMusicToggled(bool isOn)
    {
        // Only process if we're initialized (prevents scene load issues)
        if (!isInitialized)
            return;

        Debug.Log($"Music toggled to: {isOn}");
        
        // Save IMMEDIATELY
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"Saved MusicOn to PlayerPrefs: {PlayerPrefs.GetInt("MusicOn")}");
        
        // Then apply to audio manager
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
            mm.SetMusicEnabled(isOn);
    }

    public void OnSFXToggled(bool isOn)
    {
        // Only process if we're initialized (prevents scene load issues)
        if (!isInitialized)
            return;

        Debug.Log($"SFX toggled to: {isOn}");
        
        // Save IMMEDIATELY
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
        Debug.Log($"Saved SFXOn to PlayerPrefs: {PlayerPrefs.GetInt("SFXOn")}");
        
        // Then apply to audio manager
        GameAudioManager gam = GameAudioManager.instance;
        if (gam != null)
            gam.SetSFXEnabled(isOn);
    }
    
    void OnDestroy()
    {
        // Force save when leaving the scene
        PlayerPrefs.Save();
    }
}