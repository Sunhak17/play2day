using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;

    private static MusicManager instance;
    public bool musicEnabled = true;

    void Awake()
    {
        // Singleton pattern: only one MusicManager allowed
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize defaults if first time running
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            PlayerPrefs.SetFloat("MusicVolume", 1f);
            PlayerPrefs.Save();
        }
        if (!PlayerPrefs.HasKey("MusicOn"))
        {
            PlayerPrefs.SetInt("MusicOn", 1); // Default ON
            PlayerPrefs.Save();
        }

        // Set initial volume from PlayerPrefs (default to 1.0 if not set)
        float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        if (musicSource != null)
        {
            musicSource.volume = volume;
        }

        // Load music enabled state (default to true)
        musicEnabled = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        
        // Apply the music state
        if (musicSource != null)
        {
            musicSource.mute = !musicEnabled;
            
            // If enabled and not playing, start playing
            if (musicEnabled && !musicSource.isPlaying && musicSource.clip != null)
            {
                musicSource.Play();
            }
            else if (!musicEnabled && musicSource.isPlaying)
            {
                // If disabled and playing, stop it
                musicSource.Stop();
            }
        }
        
        Debug.Log($"MusicManager initialized - Enabled: {musicEnabled}, Volume: {volume}");
    }

    // Optional: call this from your settings script
    public void SetVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
            PlayerPrefs.SetFloat("MusicVolume", value);
            PlayerPrefs.Save();
        }
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        
        // Save the state FIRST
        PlayerPrefs.SetInt("MusicOn", enabled ? 1 : 0);
        PlayerPrefs.Save();
        
        if (musicSource != null)
        {
            musicSource.mute = !enabled;
            if (enabled)
            {
                // Restore volume and play if not playing
                float volume = PlayerPrefs.GetFloat("MusicVolume", 1f);
                musicSource.volume = volume;
                if (!musicSource.isPlaying && musicSource.clip != null)
                    musicSource.Play();
            }
            else
            {
                // Optionally pause or stop music
                // musicSource.Pause(); // Uncomment if you want to pause instead of mute
            }
        }
        
        Debug.Log($"MusicManager.SetMusicEnabled called with: {enabled}, saved to PlayerPrefs");
    }
}