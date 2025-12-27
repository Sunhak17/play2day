using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // Add this

public class GameSettings : MonoBehaviour
{
    public Toggle musicToggle;

    void Start()
    {
        // Load saved music setting
        bool musicOn = PlayerPrefs.GetInt("MusicOn", 1) == 1;
        if (musicToggle != null)
            musicToggle.isOn = musicOn;

        OnMusicToggled (musicOn);
    }

    public void OnMusicToggled(bool isOn)
    {
        MusicManager mm = FindObjectOfType<MusicManager>();
        if (mm != null)
            mm.SetMusicEnabled(isOn);
        PlayerPrefs.SetInt("MusicOn", isOn ? 1 : 0);
    }
}