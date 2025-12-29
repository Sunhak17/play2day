using UnityEngine;

public class AudioDebugger : MonoBehaviour
{
    void Start()
    {
        // Check for Audio Listener
        AudioListener listener = FindObjectOfType<AudioListener>();
        if (listener == null)
            Debug.LogError("NO AUDIO LISTENER FOUND! Add one to Main Camera.");
        else
            Debug.Log("Audio Listener found on: " + listener.gameObject.name);

        // Check MusicManager
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
        {
            Debug.Log("MusicManager found. Music Enabled: " + mm.musicEnabled);
            if (mm.musicSource != null)
            {
                Debug.Log("AudioSource exists");
                Debug.Log("AudioSource.clip: " + (mm.musicSource.clip != null ? mm.musicSource.clip.name : "NULL"));
                Debug.Log("AudioSource.isPlaying: " + mm.musicSource.isPlaying);
                Debug.Log("AudioSource.volume: " + mm.musicSource.volume);
                Debug.Log("AudioSource.mute: " + mm.musicSource.mute);
            }
            else
            {
                Debug.LogError("musicSource is NULL! Assign Audio Source in Inspector.");
            }
        }
        else
        {
            Debug.LogError("MusicManager not found!");
        }

        // Check PlayerPrefs
        Debug.Log("PlayerPrefs MusicOn: " + PlayerPrefs.GetInt("MusicOn", 1));
        Debug.Log("PlayerPrefs MusicVolume: " + PlayerPrefs.GetFloat("MusicVolume", 1f));
    }
}
