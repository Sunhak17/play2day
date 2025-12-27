using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // Add this

public class GameSettings : MonoBehaviour
{
    public Toggle volumeToggle;
    public Toggle fullscreenToggle;
    public Toggle musicToggle;
    public Toggle sfxToggle;
    public Toggle clickToggle;
    public AudioMixer audioMixer; // Add this

    void Start()
    {
        // Load saved settings
        float volume = PlayerPrefs.GetFloat("Volume", 1f);
        if (volumeToggle != null)
            volumeToggle.isOn = volume > 0.5f;
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        if (audioMixer != null)
            audioMixer.SetFloat("MasterVolume", volume > 0f ? Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20 : -80f);

        // Load toggles
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        if (musicToggle != null)
            musicToggle.isOn = musicOn;

        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        if (sfxToggle != null)
            sfxToggle.isOn = sfxOn;

        bool clickOn = PlayerPrefs.GetInt("ClickOn", 1) == 1;
        if (clickToggle != null)
            clickToggle.isOn = clickOn;
    }

    public void OnVolumeToggled(bool isOn)
    {
        float value = isOn ? 1f : 0f;

        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
        {
            mm.SetVolume(value);
            mm.SetMusicEnabled(isOn); // Ensure mute/unmute
        }

        GameAudioManager gam = FindObjectOfType<GameAudioManager>();
        if (gam != null)
            gam.SetAmbientVolume(value);

        if (audioMixer != null)
            audioMixer.SetFloat("MasterVolume", value > 0f ? Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20 : -80f);

        PlayerPrefs.SetFloat("Volume", value);
    }
    
    public void OnMusicChanged(bool isOn)
    {
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
            mm.SetMusicEnabled(isOn);
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    }

    public void OnSFXChanged(bool isOn)
    {
        GameAudioManager gam = FindObjectOfType<GameAudioManager>();
        if (gam != null)
            gam.SetSFXEnabled(isOn);
        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
    }

    public void OnClickChanged(bool isOn)
    {
        GameAudioManager gam = FindObjectOfType<GameAudioManager>();
        if (gam != null)
            gam.SetClickEnabled(isOn);
        PlayerPrefs.SetInt("ClickOn", isOn ? 1 : 0);
    }
    public void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}