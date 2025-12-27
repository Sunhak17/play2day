using UnityEngine;
using System.Collections;

/// <summary>
/// Manages all gameplay sound effects for the 2D football game
/// Includes pre-game intro, goal celebration, and background fan ambience
/// </summary>
public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager instance;

    [Header("Audio Clips")]
    [Tooltip("Audio to play before the game starts (e.g., countdown, whistle, crowd chant)")]
    public AudioClip preGameAudio;
    
    [Tooltip("Audio to play when a goal is scored (e.g., celebration, cheering)")]
    public AudioClip goalAudio;
    
    [Tooltip("Looping background fan ambience (e.g., crowd murmur, stadium atmosphere)")]
    public AudioClip backgroundFanAudio;

    [Header("Audio Sources")]
    private AudioSource sfxSource;      // For one-shot sounds (pre-game, goal)
    private AudioSource ambientSource;  // For looping fan audio

    [Header("Click / Toggles")]
    public AudioClip clickAudio;
    public bool sfxEnabled = true;
    public bool clickEnabled = true;

    [Header("Settings")]
    [Range(0f, 1f)]
    public float sfxVolume = 0.8f;
    
    [Range(0f, 1f)]
    public float ambientVolume = 0.4f;

    void Awake()
    {
        // Singleton pattern
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        // Create two AudioSource components
        sfxSource = gameObject.AddComponent<AudioSource>();
        sfxSource.playOnAwake = false;
        sfxSource.loop = false;
        sfxSource.volume = sfxVolume;

        ambientSource = gameObject.AddComponent<AudioSource>();
        ambientSource.playOnAwake = false;
        ambientSource.loop = true;
        ambientSource.volume = ambientVolume;

        // Load SFX and click enabled state
        sfxEnabled = PlayerPrefs.GetInt("SFXOn", 1) == 1;
        clickEnabled = PlayerPrefs.GetInt("ClickOn", 1) == 1;

        sfxSource.mute = !sfxEnabled;
        ambientSource.mute = !sfxEnabled;
        if (!sfxEnabled)
            StopBackgroundFanAudio();
    }

    void Start()
    {
        // Start background fan audio immediately
        PlayBackgroundFanAudio();
        
        // Play pre-game audio (countdown or intro)
        PlayPreGameAudio();
    }

    /// <summary>
    /// Play the pre-game intro audio (called at game start)
    /// </summary>
    public void PlayPreGameAudio()
    {
        if (!sfxEnabled)
            return;

        if (preGameAudio != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(preGameAudio, sfxVolume);
            Debug.Log("Playing pre-game audio");
        }
        else
        {
            Debug.LogWarning("Pre-game audio clip is not assigned!");
        }
    }

    /// <summary>
    /// Play goal celebration audio for 3 seconds only (called when a goal is scored)
    /// </summary>
    public void PlayGoalAudio()
    {
        if (!sfxEnabled)
            return;

        if (goalAudio != null && sfxSource != null)
        {
            // Stop any currently playing goal audio
            sfxSource.Stop();
            
            // Play the goal audio
            sfxSource.clip = goalAudio;
            sfxSource.Play();
            
            // Stop it after 3 seconds
            StartCoroutine(StopGoalAudioAfter3Seconds());
            
            Debug.Log("Playing goal celebration audio (3 seconds)");
        }
        else
        {
            Debug.LogWarning("Goal audio clip is not assigned!");
        }
    }

    /// <summary>
    /// Coroutine to stop goal audio after exactly 3 seconds
    /// </summary>
    private System.Collections.IEnumerator StopGoalAudioAfter3Seconds()
    {
        yield return new WaitForSeconds(3f);
        
        if (sfxSource != null && sfxSource.isPlaying)
        {
            sfxSource.Stop();
            Debug.Log("Goal audio stopped after 3 seconds");
        }
    }

    /// <summary>
    /// Start playing looping background fan ambience
    /// </summary>
    public void PlayBackgroundFanAudio()
    {
        if (!sfxEnabled)
            return;

        if (backgroundFanAudio != null && ambientSource != null)
        {
            ambientSource.clip = backgroundFanAudio;
            ambientSource.Play();
            Debug.Log("Playing background fan audio (looping)");
        }
        else
        {
            Debug.LogWarning("Background fan audio clip is not assigned!");
        }
    }

    /// <summary>
    /// Enable or disable all SFX/ambient audio
    /// </summary>
    public void SetSFXEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        if (sfxSource != null)
            sfxSource.mute = !enabled;
        if (ambientSource != null)
        {
            ambientSource.mute = !enabled;
            if (!enabled)
                StopBackgroundFanAudio();
            else
                PlayBackgroundFanAudio();
        }
        PlayerPrefs.SetInt("SFXOn", enabled ? 1 : 0);
    }

    /// <summary>
    /// Enable or disable click sound playback
    /// </summary>
    public void SetClickEnabled(bool enabled)
    {
        clickEnabled = enabled;
        PlayerPrefs.SetInt("ClickOn", enabled ? 1 : 0);
    }

    /// <summary>
    /// Play a UI click sound (if enabled)
    /// </summary>
    public void PlayClick()
    {
        if (clickAudio != null && sfxSource != null && sfxEnabled && clickEnabled)
        {
            sfxSource.PlayOneShot(clickAudio, sfxVolume);
        }
    }

    /// <summary>
    /// Stop background fan audio
    /// </summary>
    public void StopBackgroundFanAudio()
    {
        if (ambientSource != null && ambientSource.isPlaying)
        {
            ambientSource.Stop();
            Debug.Log("Stopped background fan audio");
        }
    }

    /// <summary>
    /// Update SFX volume (for settings menu)
    /// </summary>
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
            sfxSource.volume = sfxVolume;
    }

    /// <summary>
    /// Update ambient volume (for settings menu)
    /// </summary>
    public void SetAmbientVolume(float volume)
    {
        ambientVolume = Mathf.Clamp01(volume);
        if (ambientSource != null)
            ambientSource.volume = ambientVolume;
    }

    void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}
