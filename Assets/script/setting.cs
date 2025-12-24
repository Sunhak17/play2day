using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // Add this

public class GameSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public AudioMixer audioMixer; // Add this

    void Start()
    {
        // Load saved settings
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = volume;
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20); // Set initial volume
    }

    public void OnVolumeChanged(float value)
    {
        // Set music volume on MusicManager's AudioSource
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null && mm.musicSource != null)
            mm.musicSource.volume = value;

        PlayerPrefs.SetFloat("Volume", value);
    }
    public void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}