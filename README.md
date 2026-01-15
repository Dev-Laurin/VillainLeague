# Villain League - Unity Turn-Based Battle System

[![MIT License](https://img.shields.io/github/license/Dev-Laurin/VillainLeague.svg?style=for-the-badge)](https://github.com/Dev-Laurin/VillainLeague/blob/main/VillainLeagueUnity/Docs/LICENSE.txt)

A feature-rich 2D Unity turn-based battle system inspired by the Mario & Luigi series, featuring unique character movesets, super abilities, dynamic banter, and a medieval fantasy UI.

**[📖 Full Documentation](VillainLeagueUnity/Docs/PROJECT_OVERVIEW.md)** | **[⚡ Quick Start](QUICKSTART.md)** | **[🐛 Report Bug](https://github.com/Dev-Laurin/VillainLeague/issues)**

---

## 🎮 About The Project

Villain League is a comprehensive turn-based battle system built in Unity that brings strategic RPG combat to life. Control a squad of heroes with unique abilities as they battle against villains in tactical turn-based combat.

### ✨ Core Features

- **🎯 Character Moveset System** - Each character has 10+ unique moves with different effects, costs, and targeting options
- **⚡ Super Abilities** - Secondary resource bars charge during battle, enabling devastating ultimate attacks
- **🤝 Team Super Attacks** - When both heroes are charged, unleash a combined attack for massive damage
- **💬 Battle Banter** - Dynamic dialogue between characters that reacts to moves and battle situations
- **🎨 Medieval Fantasy UI** - Beautiful parchment-themed move selection interface with hover descriptions
- **🔄 Turn-Based Combat** - Strategic gameplay with resource management and tactical decision-making
- **📊 Resource Management** - Multiple resource types (Mana, Focus, Energy, Style, Resolve) with regeneration
- **🎭 Character Personalities** - Each hero has unique dialogue reflecting their personality

---

## 🎲 Game Mechanics

### Character Moveset System

Each character has a unique moveset with:
- **10+ Unique Moves** per character with distinct effects
- **Resource Costs** - Moves require primary resources (Mana, Focus, Energy, Resolve)
- **Move Effects** including:
  - Damage and multi-hit attacks
  - Healing and buffs
  - Debuffs and status effects
  - Area-of-Effect (AoE) attacks
  - Defensive abilities and counters
- **Affordability Checking** - Unaffordable moves are grayed out
- **Target Types** - Self, single target, AoE, and ally targeting

**Example Character - Cecelia Sylvan:**
- Resource: Focus (6 max, +1 per turn)
- 10 unique sword-based moves
- Mix of offensive, defensive, and support abilities

📚 **Documentation:** [QUICKSTART.md](QUICKSTART.md) | [Character Moveset Guide](VillainLeagueUnity/Docs/MovesetSystem.md)

### Super Abilities System

Characters build up secondary resources to unleash powerful ultimate abilities:

- **Secondary Resources:**
  - **Style** (Naice Ajimi) - Earned through stylish offensive moves
  - **Resolve** (Bellinor Chabbeneoux) - Earned through protective actions
  
- **Individual Ultimates:**
  - Powerful moves requiring both primary and secondary resources
  - Must charge secondary resource to maximum (6/6)
  - Unique effects per character
  
- **Team Super Attack:**
  - Available when BOTH heroes reach full charge
  - Depletes both heroes' secondary resources
  - Deals 15 base damage to ALL enemies
  - Devastating combined attack

📚 **Documentation:** [SUPER_ABILITIES.md](SUPER_ABILITIES.md) | [Super Abilities Summary](SUPER_ABILITIES_SUMMARY.md)

### Battle Banter System

Dynamic dialogue between Bellinor Chabbeneoux and Naice Ajimi:

- **4 Banter Types:**
  - Move comments and praise
  - Playful friendly insults
  - "Holding up?" check-ins
  - Low HP concern messages
  
- **Character Personalities:**
  - **Bellinor**: Formal, protective, supportive paladin
  - **Naice**: Playful, stylish, confident rogue
  
- **Auto-Dismissing** - Appears for 3 seconds, no click required
- **Contextual** - Reacts to battle situation and character actions
- **Non-Intrusive** - Doesn't interrupt gameplay flow

📚 **Documentation:** [BANTER_README.md](BANTER_README.md) | [Battle Banter System](VillainLeagueUnity/Docs/BATTLE_BANTER_SYSTEM.md)

### Move Chooser UI

A beautiful medieval fantasy-themed interface for move selection:

- **Full-Screen Popup** - Appears during player character turns
- **Scrollable Move List** - Shows all available moves
- **Separate Description Panel** - Displays detailed info on hover
- **Visual Affordability** - Grayed out moves when resources insufficient
- **Medieval Theme:**
  - Parchment colors (RGB 242, 230, 191)
  - Gold accents (RGB 217, 179, 64)
  - Dark brown panels (RGB 51, 38, 26)
  - Unicode icons for effects
  
- **Complete Information Display:**
  - Move name and description
  - Resource costs (primary and secondary)
  - Full effect breakdown
  - Duration and target type

📚 **Documentation:** [MOVE_CHOOSER_UI_README.md](MOVE_CHOOSER_UI_README.md) | [UI Guide](MOVE_CHOOSER_UI_GUIDE.md) | [UI Mockup](MOVE_CHOOSER_UI_MOCKUP.md)

### Battle System Core

Classic turn-based combat mechanics:

- **Squad-Based Combat** - 2 heroes vs 2 villains
- **Turn Order Management** - Characters take turns in sequence
- **Damage Calculation** - Based on Attack and Defense stats
- **HP Management** - Real-time health tracking with visual bars
- **Win/Lose Conditions** - Battle ends when one side is defeated
- **Enemy AI** - Villains automatically select targets and attacks

📚 **Documentation:** [Battle System README](VillainLeagueUnity/Docs/BATTLE_SYSTEM_README.md) | [Project Overview](VillainLeagueUnity/Docs/PROJECT_OVERVIEW.md)

---

## 🚀 Getting Started

### Prerequisites

- **Unity Hub** - Download from [unity.com/download](https://unity.com/download)
- **Unity Editor** - Version 2022.3.10f1 or later (LTS recommended)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Dev-Laurin/VillainLeague.git
   cd VillainLeague
   ```

2. **Open in Unity**
   - Launch Unity Hub
   - Click "Add" or "Open"
   - Select the `VillainLeagueUnity` folder
   - Wait for Unity to import assets

3. **Import TextMeshPro**
   - When prompted, click "Import TMP Essentials"

4. **Open Battle Scene**
   - Navigate to `Assets/Scenes`
   - Open `BattleScene.unity`

5. **Play!**
   - Press the Play button
   - Experience the battle system

📚 **Detailed Setup:** [QUICKSTART.md](QUICKSTART.md)

---

## 📁 Project Structure

```
VillainLeague/
├── README.md                              # This file
├── QUICKSTART.md                          # Quick start guide
├── SUPER_ABILITIES.md                     # Super abilities documentation
├── BANTER_README.md                       # Battle banter system
├── MOVE_CHOOSER_UI_README.md              # Move UI documentation
├── IMPLEMENTATION_SUMMARY.md              # Implementation details
│
└── VillainLeagueUnity/                    # Unity project root
    ├── Assets/
    │   ├── Scenes/
    │   │   └── BattleScene.unity          # Main battle scene
    │   ├── Scripts/
    │   │   ├── BattleManager.cs           # Core battle controller
    │   │   ├── Character.cs               # Character data and stats
    │   │   ├── CharacterMoveSet.cs        # Moveset management
    │   │   ├── Move.cs                    # Move data structure
    │   │   ├── MoveSetFactory.cs          # Moveset creation
    │   │   ├── MoveSetLoader.cs           # Load movesets from JSON
    │   │   ├── TurnManager.cs             # Turn order logic
    │   │   ├── BattleUI.cs                # UI controller
    │   │   ├── BattleUISetup.cs           # UI setup helper
    │   │   ├── MoveChooserUI.cs           # Enhanced move UI
    │   │   ├── MoveChooserUISetup.cs      # Move UI setup
    │   │   └── BattleBanter.cs            # Banter system
    │   └── Data/
    │       └── Characters/                # Character JSON files
    │
    └── Docs/                              # Detailed documentation
        ├── PROJECT_OVERVIEW.md            # Complete project overview
        ├── BATTLE_SYSTEM_README.md        # Battle system details
        ├── MovesetSystem.md               # Moveset system guide
        ├── BATTLE_BANTER_SYSTEM.md        # Banter implementation
        ├── QUICKSTART.md                  # Quick start guide
        └── README.md                      # Unity project readme
```

---

## 📚 Documentation Index

### Getting Started
- **[Quick Start Guide](QUICKSTART.md)** - Get up and running quickly
- **[Project Overview](VillainLeagueUnity/Docs/PROJECT_OVERVIEW.md)** - Comprehensive project overview
- **[How to Use](VillainLeagueUnity/Docs/HOW_TO_USE.md)** - Detailed usage instructions

### Game Mechanics
- **[Character Moveset System](VillainLeagueUnity/Docs/MovesetSystem.md)** - Complete moveset documentation
- **[Super Abilities](SUPER_ABILITIES.md)** - Ultimate moves and team attacks
- **[Battle Banter](BANTER_README.md)** - Dynamic dialogue system
- **[Move Chooser UI](MOVE_CHOOSER_UI_README.md)** - Enhanced UI system

### Technical Documentation
- **[Battle System](VillainLeagueUnity/Docs/BATTLE_SYSTEM_README.md)** - Core battle mechanics
- **[Implementation Summary](IMPLEMENTATION_SUMMARY.md)** - Technical implementation details
- **[Architecture](VillainLeagueUnity/Docs/Architecture.md)** - System architecture diagrams
- **[Character Config](VillainLeagueUnity/Docs/CharacterMovesetConfig.md)** - Character configuration guide

### UI & Visual Guides
- **[Move UI Guide](MOVE_CHOOSER_UI_GUIDE.md)** - Move selection UI details
- **[UI Mockup](MOVE_CHOOSER_UI_MOCKUP.md)** - Visual mockups
- **[Banter Mockup](BANTER_VISUAL_MOCKUP.md)** - Banter UI examples
- **[Super Abilities Visual](SUPER_ABILITIES_VISUAL.md)** - Super ability UI

### Development Guides
- **[Testing Guide](VillainLeagueUnity/Docs/TESTING_GUIDE.md)** - How to test the system
- **[Changelog](VillainLeagueUnity/Docs/CHANGELOG.md)** - Version history
- **[Implementation Complete](VillainLeagueUnity/Docs/IMPLEMENTATION_COMPLETE.md)** - Completion reports

---

## 🎯 Characters

### Player Squad

#### Bellinor Chabbeneoux (Paladin)
- **Primary Resource:** Resolve (6 max, +1 per turn)
- **Secondary Resource:** Resolve (6 max, earned through protective actions)
- **Role:** Tank / Support
- **Personality:** Formal, protective, honorable
- **Ultimate:** "Oathbound Chorus" - Heals all allies for 6 HP and grants 3 Armor
- **Moveset:** Guard Stance, Intercept, Rapid Mend, Holy Strike, Barrier, Consecrate, Shield Bash, Lay on Hands, Redemption, Righteous Fury

#### Naice Ajimi (Rogue)
- **Primary Resource:** Mana (6 max, +1 per turn)
- **Secondary Resource:** Style (6 max, earned through stylish moves)
- **Role:** DPS / Trickster
- **Personality:** Playful, stylish, confident
- **Ultimate:** "Final Act" - Creates illusions and deals 5 damage to all enemies
- **Moveset:** Feinting Strike, Smoke & Mirrors, Cutting Remark, False Opening, Take a Bow, Quick Stab, Shadow Strike, Distraction, Precision Strike, Riposte

#### Cecelia Sylvan (Swordmaster)
- **Primary Resource:** Focus (6 max, +1 per turn)
- **Role:** DPS / Mobility
- **Moveset:** Twin Slash, Blink Strike, Flash Step, Hero's Intercept, Rally Heart, Severing Cut, Swordbind, Piercing Lunge, Blade Whirl, Afterimage

### Enemy Squad
- **Villain 1:** 70 HP, 12 ATK, 4 DEF
- **Villain 2:** 90 HP, 10 ATK, 6 DEF

---

## 🛠️ Built With

- **Unity Engine** - Version 2022.3.10f1 or later
- **C#** - .NET Standard 2.1
- **TextMeshPro** - UI text rendering
- **Unity UI** - User interface system
- **2D Renderer** - 2D graphics rendering

---

## 🎨 Example Gameplay

### Typical Battle Flow

1. **Battle Starts** - Two heroes face two villains
2. **Turn Order** - Characters take turns based on turn order
3. **Player Turn:**
   - Move selection UI appears with medieval theme
   - Hover over moves to see detailed descriptions
   - Select a move (if affordable)
   - Choose target if needed
   - Move executes, resources spent
   - Banter may trigger (25% chance)
4. **Resource Management:**
   - Primary resources regenerate each turn
   - Secondary resources earned through specific actions
   - Super abilities available when fully charged
5. **Enemy Turn** - AI selects and executes actions
6. **Victory/Defeat** - Battle ends when one side is eliminated

### Team Super Attack Example

1. Bellinor uses protective moves → Gains Resolve (secondary)
2. Naice uses stylish moves → Gains Style (secondary)
3. Both reach 6/6 on secondary resources
4. On either hero's turn, player sees three options:
   - **NORMAL TURN** - Regular moves
   - **USE SUPER** - Individual ultimate
   - **TEAM SUPER** - Combined attack!
5. Team Super selected → 15 damage to all enemies
6. Both secondary resources depleted

---

## 🗺️ Roadmap

### Completed ✅
- [x] Core battle system with turn management
- [x] Character moveset system with 10+ moves per character
- [x] Resource system with multiple resource types
- [x] Super abilities and secondary resources
- [x] Team super attack system
- [x] Battle banter with dynamic dialogue
- [x] Medieval fantasy move selection UI
- [x] Hover descriptions and visual feedback
- [x] Character JSON configuration system
- [x] Comprehensive documentation

### Future Enhancements 🔮
- [ ] Character sprites and animations
- [ ] Move animations and visual effects
- [ ] Sound effects and background music
- [ ] Voice acting for banter
- [ ] Additional characters with unique movesets
- [ ] More battle scenarios and maps
- [ ] Equipment system
- [ ] Experience and leveling
- [ ] Status effect animations
- [ ] Combo attack indicators
- [ ] Main menu and UI polish
- [ ] Save/Load system

See [open issues](https://github.com/Dev-Laurin/VillainLeague/issues) for planned features.

---

## 🤝 Contributing

Contributions are welcome! Whether it's bug fixes, new features, documentation, or art assets, your help is appreciated.

### How to Contribute

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Areas for Contribution

- **Features** - New battle mechanics, characters, moves
- **UI/UX** - Visual improvements, animations
- **Art** - Character sprites, effects, backgrounds
- **Audio** - Sound effects, music, voice acting
- **Documentation** - Tutorials, guides, translations
- **Testing** - Bug reports, test cases
- **Code Quality** - Refactoring, optimization

---

## 📄 License

Distributed under the MIT License. See [`LICENSE.txt`](VillainLeagueUnity/Docs/LICENSE.txt) for more information.

---

## 🙏 Acknowledgments

- Inspired by the **Mario & Luigi** series battle system
- Built with [Unity Game Engine](https://unity.com/)
- Uses [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html)
- Thanks to the Unity community for resources and support

---

## 📞 Contact

Project Link: [https://github.com/Dev-Laurin/VillainLeague](https://github.com/Dev-Laurin/VillainLeague)

---

## 🎓 Learning Resources

This project demonstrates:
- Turn-based game architecture
- State machine patterns
- Resource management systems
- Dynamic UI generation
- Event-driven programming
- Unity coroutines and game loops
- Object-oriented design principles
- JSON data serialization
- Component-based architecture

Perfect for learning Unity game development, turn-based RPG mechanics, and clean code architecture!

---

**[⬆ Back to top](#villain-league---unity-turn-based-battle-system)**
