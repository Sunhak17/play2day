# 🎵 Audio Setup Guide - Football Game

## Overview
This guide explains how to add 3 audio effects to your gameplay:
1. **Pre-game intro audio** - Plays when the match starts
2. **Goal celebration audio** - Plays when a goal is scored
3. **Background fan ambience** - Loops continuously during gameplay

---

## 📋 Setup Steps

### Step 1: Create the Audio Manager GameObject

1. Open the **GamePlay** scene in Unity
2. In the **Hierarchy**, right-click and select **Create Empty**
3. Rename it to `GameAudioManager`
4. With `GameAudioManager` selected, click **Add Component**
5. Search for `GameAudioManager` and add the script

---

### Step 2: Import Your Audio Files

1. Create a folder: `Assets/Audio` (if it doesn't exist)
2. Drag your 3 audio files into this folder:
   - `pregame_intro.mp3` or `.wav` (e.g., countdown, whistle, "let's go!")
   - `goal_celebration.mp3` or `.wav` (e.g., crowd cheering, air horn)
   - `background_fans.mp3` or `.wav` (e.g., stadium ambience, crowd murmur)

**Recommended Audio Settings:**
- **Pre-game:** 3-5 seconds, energetic
- **Goal:** 2-4 seconds, celebratory
- **Background fans:** 30-60 seconds loop, subtle ambience

---

### Step 3: Assign Audio Clips in Inspector

1. Select the `GameAudioManager` GameObject in the Hierarchy
2. In the **Inspector**, you'll see the `GameAudioManager` script with these fields:

   **Audio Clips:**
   - **Pre Game Audio** - Drag your pre-game intro audio here
   - **Goal Audio** - Drag your goal celebration audio here
   - **Background Fan Audio** - Drag your looping fan ambience here

   **Settings:**
   - **Sfx Volume** - Volume for pre-game and goal sounds (0.0 to 1.0, default: 0.8)
   - **Ambient Volume** - Volume for background fan loop (0.0 to 1.0, default: 0.4)

3. **Save the scene** (Ctrl+S / Cmd+S)

---

## 🎮 How It Works

### Automatic Playback:
- **Pre-game audio** plays automatically when the match starts
- **Background fan audio** starts looping immediately and continues throughout the match
- **Goal audio** triggers automatically whenever a goal is scored

### Script Integration:
- `GameAudioManager.cs` handles all audio playback
- `GoalDetection.cs` calls `PlayGoalAudio()` when a goal is detected
- Audio is managed through a singleton, accessible from any script via `GameAudioManager.instance`

---

## 🔧 Advanced Customization

### Adjust Volumes Dynamically:
```csharp
// From any script:
GameAudioManager.instance.SetSFXVolume(0.5f);      // 50% volume
GameAudioManager.instance.SetAmbientVolume(0.3f);  // 30% volume
```

### Stop Background Audio:
```csharp
GameAudioManager.instance.StopBackgroundFanAudio();
```

### Play Goal Audio Manually:
```csharp
GameAudioManager.instance.PlayGoalAudio();
```

---

## 🎨 Optional: Add More Audio Events

If you want to add more sounds (e.g., whistle, kick, header), you can extend `GameAudioManager.cs`:

1. Add new `AudioClip` variables in the script
2. Create new `PlayXXXAudio()` methods
3. Call them from the relevant scripts (e.g., `player.cs` for kick sounds)

**Example:**
```csharp
[Header("Additional Audio")]
public AudioClip kickAudio;
public AudioClip whistleAudio;

public void PlayKickAudio()
{
    if (kickAudio != null)
        sfxSource.PlayOneShot(kickAudio, sfxVolume);
}
```

---

## ⚠️ Troubleshooting

**No audio plays:**
- Check that audio clips are assigned in the Inspector
- Ensure the `GameAudioManager` GameObject is active in the scene
- Verify that Unity's **Audio Listener** component exists (usually on the Main Camera)
- Check your system volume and Unity's audio mixer settings

**Audio is too loud/quiet:**
- Adjust `Sfx Volume` and `Ambient Volume` in the Inspector
- Or adjust individual audio clip import settings (select audio file → Inspector → Volume slider)

**Background audio doesn't loop:**
- Verify `backgroundFanAudio` is assigned
- Check that the audio file is set to loop in the import settings (optional, script handles looping)

**Multiple audio managers:**
- Only one `GameAudioManager` should exist. The singleton pattern destroys duplicates automatically.

---

## 📁 File Structure

```
Assets/
├── script/
│   ├── GameAudioManager.cs       ← Main audio manager
│   ├── GoalDetection.cs           ← Triggers goal audio
│   └── ...
├── Audio/                         ← Your audio files
│   ├── pregame_intro.mp3
│   ├── goal_celebration.mp3
│   └── background_fans.mp3
└── Scenes/
    └── GamePlay.unity             ← Scene with GameAudioManager
```

---

## ✅ Quick Checklist

- [ ] Created `GameAudioManager` GameObject in GamePlay scene
- [ ] Added `GameAudioManager` script component
- [ ] Imported 3 audio files to `Assets/Audio/`
- [ ] Assigned all 3 audio clips in the Inspector
- [ ] Tested in Play mode - pre-game audio plays at start
- [ ] Scored a goal - celebration audio plays
- [ ] Background fan audio loops continuously

---

Enjoy your immersive football game with dynamic audio! 🎉⚽
