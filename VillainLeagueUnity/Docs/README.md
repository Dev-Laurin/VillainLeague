# Villain League - Turn-Based Battle System

[![MIT License](https://img.shields.io/github/license/Dev-Laurin/VillainLeague.svg?style=for-the-badge)](https://github.com/Dev-Laurin/VillainLeague/blob/main/LICENSE.txt)

A 2D Unity turn-based battle system inspired by the Mario & Luigi series, featuring squad-based combat with 2 heroes vs 2 villains.

**[Explore the docs »](PROJECT_OVERVIEW.md)** | **[Quick Start](QUICKSTART.md)** | **[Report Bug](https://github.com/Dev-Laurin/VillainLeague/issues)**

## About The Project

Villain League is a 2D Unity turn-based battle system that brings classic RPG combat mechanics to life. Control a squad of 2 heroes as they battle against 2 villains in strategic turn-based combat inspired by the beloved Mario & Luigi series.

### ✨ Key Features

- **Turn-Based Combat**: Classic RPG-style turn-based battle system
- **Squad-Based**: Control 2 heroes in your party
- **Multiple Actions**: Attack, Defend, and Special moves
- **Strategic Gameplay**: Choose your actions wisely to defeat enemies
- **HP Management**: Real-time health tracking with damage calculation
- **Enemy AI**: Face intelligent villains with tactical behavior
- **Clean Architecture**: Well-structured, modular code for easy extension

### 🎮 Gameplay

- **2 Player Characters**: Hero 1 (100 HP, 15 ATK, 5 DEF) and Hero 2 (80 HP, 20 ATK, 3 DEF)
- **2 Enemy Characters**: Villain 1 (70 HP, 12 ATK, 4 DEF) and Villain 2 (90 HP, 10 ATK, 6 DEF)
- **Battle Actions**: 
  - Attack: Normal damage attack
  - Defend: Defensive stance
  - Special: Powerful 2x damage attack
- **Win Condition**: Defeat all enemies
- **Lose Condition**: All heroes defeated

([back to top](#villain-league---turn-based-battle-system))

### Built With

* [Unity](https://unity.com/) - Game Engine (2022.3.10f1)
* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Programming Language
* [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html) - UI Text System

([back to top](#villain-league---turn-based-battle-system))

## Getting Started

Follow these steps to get the battle system up and running on your machine.

### Prerequisites

Before you begin, ensure you have:

* **Unity Hub** - Download from [unity.com/download](https://unity.com/download)
* **Unity Editor** - Version 2022.3.10f1 or later (LTS version recommended)
  ```
  Install via Unity Hub > Installs > Add
  Select Unity 2022.3.10f1 or later
  ```

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/Dev-Laurin/VillainLeague.git
   ```

2. **Open Unity Hub**
   - Click "Add" or "Open"
   - Navigate to the cloned repository folder
   - Select the folder and click "Open"

3. **Open the Project in Unity**
   - Click on "VillainLeague" in Unity Hub
   - Wait for Unity to import all assets (first time may take a few minutes)

4. **Import TextMeshPro**
   - When prompted, click "Import TMP Essentials"
   - This is required for UI text display

5. **Open the Battle Scene**
   - In Unity Project window, navigate to `Assets/Scenes`
   - Double-click `BattleScene.unity`

6. **Complete UI Setup** (See [QUICKSTART.md](QUICKSTART.md) for detailed UI setup)
   - Create Canvas and UI elements
   - Wire up UI components to BattleUI script
   - Save the scene

7. **Play!**
   - Press the Play button in Unity Editor
   - Experience the turn-based battle system

For detailed setup instructions, see **[QUICKSTART.md](QUICKSTART.md)**

([back to top](#villain-league---turn-based-battle-system))

## Usage

### Playing the Game

1. Open `BattleScene.unity` in Unity Editor
2. Press the **Play** button
3. The battle begins automatically with your squad vs the villain squad

### During Battle

**Your Turn:**
- Click **Attack** to perform a normal attack on an enemy
- Click **Defend** to take a defensive stance
- Click **Special** to perform a powerful 2x damage attack
- Select your target when prompted

**Enemy Turn:**
- Enemies automatically attack your squad
- Watch as damage is calculated and HP bars update

**Victory/Defeat:**
- Win by defeating all enemies
- Lose if all your heroes fall

### Example Screenshots

*(Screenshots would be added here after UI implementation)*

### Code Examples

See `Assets/Scripts/BattleSystemExample.cs` for code examples:

```csharp
// Create a custom character
Character hero = new Character("Warrior", 120, 18, 7, true);

// Deal damage
hero.TakeDamage(25);

// Heal character
hero.Heal(15);

// Check if alive
if (hero.IsAlive()) {
    Debug.Log("Hero is still fighting!");
}
```

For more detailed documentation, see [BATTLE_SYSTEM_README.md](BATTLE_SYSTEM_README.md)

([back to top](#villain-league---turn-based-battle-system))

## Roadmap

### Completed ✅
- [x] Unity 2D project setup
- [x] Core battle system architecture
- [x] Character system with stats (HP, Attack, Defense)
- [x] Turn-based mechanics
- [x] Action system (Attack, Defend, Special)
- [x] Target selection system
- [x] Damage calculation
- [x] Turn order management
- [x] Win/Lose conditions
- [x] Battle scene setup
- [x] Comprehensive documentation

### Future Enhancements 🔮
- [ ] Complete UI implementation with visual designs
- [ ] Character sprites and animations
- [ ] Sound effects and music
- [ ] Particle effects for attacks
- [ ] Status effects (poison, stun, burn)
- [ ] Item system
- [ ] Experience and leveling
- [ ] Multiple battle scenarios
- [ ] Save/Load system
- [ ] Enhanced enemy AI
- [ ] Combo attacks
- [ ] Equipment system
- [ ] Main menu
- [ ] Victory/defeat screens

See the [open issues](https://github.com/Dev-Laurin/VillainLeague/issues) for a full list of proposed features and known issues.

([back to top](#villain-league---turn-based-battle-system))

## Contributing

Contributions make the open source community an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**!

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".

### How to Contribute

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Areas for Contribution

- **UI Design**: Create beautiful battle UI layouts
- **Art**: Character sprites, animations, effects
- **Audio**: Sound effects, background music
- **Features**: New battle mechanics, items, skills
- **Documentation**: Tutorials, guides, examples
- **Bug Fixes**: Report and fix bugs

([back to top](#villain-league---turn-based-battle-system))

## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

([back to top](#villain-league---turn-based-battle-system))

## Contact

Project Link: [https://github.com/Dev-Laurin/VillainLeague](https://github.com/Dev-Laurin/VillainLeague)

([back to top](#villain-league---turn-based-battle-system))

## Acknowledgments

* Inspired by the **Mario & Luigi** series turn-based battle system
* Built with [Unity Game Engine](https://unity.com/)
* Uses [TextMeshPro](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html) for text rendering
* Thanks to the Unity community for excellent resources and support

([back to top](#villain-league---turn-based-battle-system))

## Documentation

- **[PROJECT_OVERVIEW.md](PROJECT_OVERVIEW.md)** - Comprehensive project overview
- **[QUICKSTART.md](QUICKSTART.md)** - Quick start guide for new users
- **[BATTLE_SYSTEM_README.md](BATTLE_SYSTEM_README.md)** - Detailed technical documentation
- **[Assets/Scripts/](Assets/Scripts/)** - Well-commented source code
