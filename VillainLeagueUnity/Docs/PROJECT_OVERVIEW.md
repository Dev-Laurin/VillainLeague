# Villain League - Unity 2D Turn-Based Battle System

## Project Overview

This Unity project implements a complete turn-based battle system for a 2D game, inspired by the Mario & Luigi series. The system features squad-based combat with 2 player characters fighting against 2 enemy villains.

## ✨ Key Features

### Battle System
- ✅ **Turn-Based Combat** - Classic turn-based mechanics where each character takes their turn in sequence
- ✅ **Squad System** - Control 2 heroes in your party
- ✅ **Enemy AI** - Villains automatically select targets and attack
- ✅ **Multiple Actions** - Attack, Defend, and Special moves
- ✅ **HP Management** - Real-time health tracking with visual HP bars
- ✅ **Damage Calculation** - Attack and Defense stats affect damage dealt
- ✅ **Win/Lose Conditions** - Battle ends when one side is defeated

### Technical Features
- ✅ **Modular Architecture** - Separate managers for Battle, Turn, and UI
- ✅ **Character Class** - Flexible character system supporting multiple stats
- ✅ **Coroutine-Based Flow** - Smooth battle flow using Unity coroutines
- ✅ **UI Integration** - Full UI system with TextMeshPro support
- ✅ **Target Selection** - Interactive target selection for attacks

## 📁 Project Structure

```
VillainLeague/
├── Assets/
│   ├── Scenes/
│   │   └── BattleScene.unity          # Main battle scene
│   ├── Scripts/
│   │   ├── BattleManager.cs           # Core battle controller and state machine
│   │   ├── Character.cs               # Character data, stats, and actions
│   │   ├── TurnManager.cs             # Turn order and rotation logic
│   │   ├── BattleUI.cs                # UI controller and updates
│   │   └── BattleSystemExample.cs     # Example usage and testing
│   └── Prefabs/                       # (Ready for character prefabs)
├── ProjectSettings/                    # Unity project configuration
├── .gitignore                         # Unity-specific gitignore
├── README.md                          # Original project readme
├── BATTLE_SYSTEM_README.md            # Detailed system documentation
├── QUICKSTART.md                      # Quick start guide
└── PROJECT_OVERVIEW.md                # This file
```

## 🎮 Game Design

### Character Stats

#### Player Squad
| Character | HP  | Attack | Defense |
|-----------|-----|--------|---------|
| Hero 1    | 100 | 15     | 5       |
| Hero 2    | 80  | 20     | 3       |

#### Enemy Squad
| Character   | HP  | Attack | Defense |
|-------------|-----|--------|---------|
| Villain 1   | 70  | 12     | 4       |
| Villain 2   | 90  | 10     | 6       |

### Battle Actions

1. **Attack** 
   - Deals normal damage based on Attack stat
   - Formula: `max(1, Attack - Enemy Defense)`
   - Always available

2. **Defend**
   - Character takes a defensive stance
   - Currently blocks damage (can be enhanced with damage reduction)
   - Strategic choice for survival

3. **Special**
   - Powerful attack dealing 2x damage
   - Formula: `max(1, (Attack × 2) - Enemy Defense)`
   - No cooldown (can be balanced in future updates)

### Combat Flow

1. Battle starts with all characters in turn order
2. On player turn:
   - Select an action (Attack/Defend/Special)
   - Select a target (for Attack/Special)
   - Action is executed
3. On enemy turn:
   - AI selects random alive player
   - Executes attack automatically
4. Turn passes to next character
5. Cycle continues until victory or defeat

## 🔧 Technical Implementation

### Core Classes

#### BattleManager
- **Responsibility**: Main battle controller
- **Key Features**:
  - State machine (START, PLAYER_TURN, ENEMY_TURN, WON, LOST)
  - Coroutine-based battle flow
  - Action execution
  - Win/lose condition checking
  - Target selection system

#### Character
- **Responsibility**: Character data and stats
- **Key Features**:
  - Health management (current/max HP)
  - Attack and defense stats
  - Damage calculation
  - Healing functionality
  - Alive status checking
  - Player/enemy identification

#### TurnManager
- **Responsibility**: Turn order management
- **Key Features**:
  - Turn order initialization
  - Current turn tracking
  - Dead character skipping
  - Round management
  - Team status checking

#### BattleUI
- **Responsibility**: UI updates and display
- **Key Features**:
  - Character stat display (HP, names)
  - HP bar visualization
  - Turn indicator
  - Message display
  - Button management
  - Target selection UI

### Design Patterns Used

1. **Manager Pattern** - Separate managers for battle, turns, and UI
2. **State Machine** - BattleState enum for battle flow control
3. **Coroutines** - For sequential battle actions and timing
4. **Serialization** - Character data can be serialized for saving
5. **Component-Based** - MonoBehaviours for Unity integration

## 🚀 Getting Started

### For Players
See [QUICKSTART.md](QUICKSTART.md) for instructions on opening and playing the game.

### For Developers
See [BATTLE_SYSTEM_README.md](BATTLE_SYSTEM_README.md) for detailed technical documentation and extension guides.

## 📝 Development Status

### Completed ✅
- [x] Unity project structure
- [x] Core battle system scripts
- [x] Character system with stats
- [x] Turn-based mechanics
- [x] Action system (Attack/Defend/Special)
- [x] Target selection
- [x] Damage calculation
- [x] Win/lose conditions
- [x] Battle scene setup
- [x] UI framework
- [x] Documentation

### Future Enhancements 🔮
- [ ] UI implementation with visual elements
- [ ] Character sprites and animations
- [ ] Sound effects and music
- [ ] Particle effects for actions
- [ ] Status effects (poison, stun, burn, etc.)
- [ ] Item system
- [ ] Multiple battle scenarios
- [ ] Experience and leveling
- [ ] Save/Load system
- [ ] Combo attacks
- [ ] Enhanced enemy AI
- [ ] Skill trees
- [ ] Equipment system

## 🎯 Use Cases

### Educational
- Learning Unity 2D game development
- Understanding turn-based combat systems
- Studying state machines and game loops
- Example of clean code architecture

### Game Development
- Foundation for RPG battle systems
- Template for turn-based games
- Starting point for similar combat mechanics
- Reference implementation for game designers

### Portfolio
- Demonstrates programming skills
- Shows understanding of game design
- Example of documentation practices
- Clean, maintainable code structure

## 🛠️ Technologies

- **Unity Engine**: 2022.3.10f1 or later
- **Language**: C# (.NET Standard 2.1)
- **UI System**: Unity UI with TextMeshPro
- **Platform**: Cross-platform (Standalone, WebGL, Mobile)
- **Rendering**: 2D Renderer

## 📚 Additional Resources

- [Unity Documentation](https://docs.unity3d.com/)
- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [TextMeshPro Guide](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)
- [Game Programming Patterns](https://gameprogrammingpatterns.com/)

## 🤝 Contributing

This project is open for contributions! Some ways to contribute:
- Add new battle mechanics
- Implement UI designs
- Create character sprites
- Add sound effects
- Write additional documentation
- Report bugs
- Suggest features

## 📄 License

Distributed under the MIT License. See `LICENSE.txt` for more information.

## 👏 Acknowledgments

- Inspired by the Mario & Luigi series battle system
- Built with Unity Game Engine
- Uses TextMeshPro for text rendering

---

**Note**: This project provides the complete battle system logic but requires UI setup in Unity Editor to be fully playable. Follow the QUICKSTART.md guide to complete the UI implementation.
