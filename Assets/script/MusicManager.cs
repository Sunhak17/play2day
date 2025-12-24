using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource musicSource;

    private static MusicManager instance;

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
    }

    // Optional: call this from your settings script
    public void SetVolume(float value)
    {
        musicSource.volume = value;
        PlayerPrefs.SetFloat("Volume", value);
    }
}