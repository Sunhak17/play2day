# Play2Day: Cambodia Premier League Soccer

## About the Project
Play2Day is a modern digital soccer game inspired by the Cambodia Premier League, developed by a team of Year 3 university students in 2025. This Unity-based project lets you experience the excitement of Cambodia’s top football clubs in a fast-paced, competitive, and fun environment. Select your favorite club from the Cambodia Premier League and lead them to victory!

---

## Features
- **Official Club Selection:** Choose from real Cambodia Premier League clubs with authentic logos and unique styles
- **Dynamic Gameplay:** Fast-paced soccer with smooth animations and physics-based interactions
- **Customizable Audio:** Toggle background music, sound effects, and ambient fan sounds through the settings menu
- **Multiple Game Scenes:** Complete flow from welcome screen through team selection, customization, gameplay, to post-match analysis
- **After-Match Highlights:** Review detailed results and statistics after every match
- **Interactive UI:** Polished menu system with personalization options

---

## Project Structure

```
play2day/
├── Assets/
│   ├── animation/          # Character and object animations
│   ├── audio/              # Background music and sound effects
│   ├── prefab/             # Reusable game objects
│   ├── Scenes/             # Game scenes (see Game Scenes below)
│   ├── script/             # Game logic organized by system:
│   │   ├── Audio/
│   │   ├── GameManagement/
│   │   ├── Gameplay/
│   │   ├── Player/
│   │   ├── Team/
│   │   └── UI/
│   ├── sprite/             # 2D graphics and textures
│   └── TechnoGami/         # UI assets and fonts
├── Packages/               # Unity dependencies
├── ProjectSettings/        # Unity project configuration
└── Documentation/
    ├── README.md           # This file
    └── AUDIO_SETUP_GUIDE.md # Audio configuration guide
```

---

## Game Scenes

| Scene | Purpose |
|-------|---------|
| **Welcome.unity** | Game introduction and splash screen |
| **CPL.unity** | Cambodia Premier League team information |
| **Team_selection.unity** | Select your team to play with |
| **Choose_Customization.unity** | Customize team appearance and settings |
| **Setting.unity** | Audio and gameplay settings menu |
| **GamePlay.unity** | Main gameplay experience |
| **After-Match.unity** | Match results and statistics |
| **Loading.unity** | Loading screen between scenes |

---

## Controls
- **Move Left/Right:** A / D keys
- **Shoot:** K key
- **Jump:** Space bar
- **Settings:** Access from any scene via the settings menu

---

## Getting Started

### Prerequisites
- Unity (version specified in `ProjectSettings/ProjectVersion.txt`)
- TextMesh Pro (included in Assets)

### Setup Instructions

1. **Clone or Open the Project**
   ```bash
   cd play2day
   open play2day.sln  # or open in Unity Hub
   ```

2. **Audio Setup** (Important!)
   - Follow [AUDIO_SETUP_GUIDE.md](AUDIO_SETUP_GUIDE.md) for detailed audio configuration
   - Audio files are already imported in `Assets/audio/`
   - Configure GameAudioManager in the GamePlay scene

3. **Run the Game**
   - Open Unity and load the project
   - Start with the **Welcome** scene
   - Click Play in the Unity Editor

---

## Gameplay Loop

1. Start from the Welcome scene
2. View Cambodia Premier League info (CPL scene)
3. Select your favorite team (Team_selection scene)
4. Customize your team (Choose_Customization scene)
5. Review audio settings (Setting scene)
6. Enter the match (GamePlay scene)
7. Use A/D to move, Space to jump, K to shoot
8. View your results and statistics (After-Match scene)
9. Return to menus to play again

---

## Audio Files

The following audio files are included in `Assets/audio/`:
- `background_fan.mp3` - Stadium ambience (loops during gameplay)
- `game_music.mp3` - Background music
- `game_start.mp3` - Pre-game intro sound
- `goal.mp3` - Goal celebration sound

> **Note:** See [AUDIO_SETUP_GUIDE.md](AUDIO_SETUP_GUIDE.md) for detailed audio configuration instructions.

---

## System Architecture

### Game Management
- Handles scene transitions and game flow
- Manages game state across scenes

### Gameplay
- Core football mechanics
- Player and ball physics
- Animation controllers

### Player
- Player character control and behavior
- Animation synchronization
- Input handling

### Team
- Team data and selection logic
- Club information management

### UI
- Menu systems and navigation
- Settings interface
- Match results display

### Audio
- Audio playback management
- Volume control
- Dynamic audio triggers

---

## Development Notes

- **Physics:** Uses Unity 2D physics for ball and player interactions
- **Animations:** Multiple animator controllers for smooth character movements and ball physics
- **UI Framework:** Built with TextMesh Pro for professional text rendering
- **Font:** Uses Thaleah Pixel Font for retro aesthetic

---

## Credits
- Developed by Year 3 students, 2025
- Inspired by the Cambodia Premier League and the spirit of soccer
- Uses TextMesh Pro for UI rendering
- Pixel art font styling for authentic game aesthetic

---

## License
This project is developed for educational purposes at CADT (Cambodia Academy of Digital Technology).