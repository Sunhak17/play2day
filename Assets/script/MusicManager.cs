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

        // Set initial volume from PlayerPrefs
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        musicSource.volume = volume;

        // Load music enabled state
        musicEnabled = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        musicSource.mute = !musicEnabled;
    }

    // Optional: call this from your settings script
    public void SetVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }

    public void SetMusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        if (musicSource != null)
        {
            musicSource.mute = !enabled;
            if (enabled)
            {
                // Restore volume and play if not playing
                float volume = PlayerPrefs.GetFloat("Volume", 1f);
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
        PlayerPrefs.SetInt("MusicOn", enabled ? 1 : 0);
    }
}