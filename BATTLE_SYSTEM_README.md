# Villain League - Turn-Based Battle System

A 2D Unity turn-based battle system inspired by the Mario and Luigi series, featuring a squad of 2 characters battling against enemy villains.

## Features

- **Turn-Based Combat**: Classic turn-based battle mechanics
- **Squad System**: Control 2 heroes in your party
- **Enemy Squad**: Face off against 2 villains
- **Action System**: 
  - Attack: Basic attack dealing normal damage
  - Defend: Defensive stance for the turn
  - Special: Powerful attack dealing 2x damage
- **HP Management**: Real-time health tracking for all characters
- **Turn Order**: Fair turn-based system rotating between all characters
- **Victory/Defeat Conditions**: Battle ends when one squad is defeated

## Project Structure

```
VillainLeague/
├── Assets/
│   ├── Scenes/
│   │   └── BattleScene.unity      # Main battle scene
│   ├── Scripts/
│   │   ├── BattleManager.cs       # Main battle controller
│   │   ├── Character.cs           # Character data and stats
│   │   ├── TurnManager.cs         # Turn order management
│   │   └── BattleUI.cs            # UI controller
│   └── Prefabs/                   # Character prefabs (to be added)
└── ProjectSettings/                # Unity project configuration
```

## Character Stats

### Player Squad
- **Hero 1**: 100 HP, 15 ATK, 5 DEF
- **Hero 2**: 80 HP, 20 ATK, 3 DEF

### Enemy Squad
- **Villain 1**: 70 HP, 12 ATK, 4 DEF
- **Villain 2**: 90 HP, 10 ATK, 6 DEF

## How to Use

### Opening the Project

1. Install Unity Hub and Unity Editor (version 2022.3.10f1 or later)
2. Open Unity Hub and click "Add" to add this project
3. Select the `/home/runner/work/VillainLeague/VillainLeague` folder
4. Open the project in Unity

### Playing the Battle

1. Open the `BattleScene.unity` from `Assets/Scenes/`
2. Press Play in the Unity Editor
3. During your turn:
   - Click "Attack" to perform a normal attack
   - Click "Defend" to take a defensive stance
   - Click "Special" to perform a powerful 2x damage attack
4. When attacking or using special, select your target from the available enemies
5. Watch as enemies take their turns automatically
6. Battle continues until all heroes or all villains are defeated

### Setting Up the UI (For Developers)

The battle system requires a UI setup with TextMeshPro components. To complete the UI:

1. Create a Canvas in the scene (GameObject > UI > Canvas)
2. Set Canvas Scaler to "Scale With Screen Size"
3. Add the following UI elements as children of the Canvas:

**Player Squad UI:**
- TextMeshPro Text for Hero 1 Name
- TextMeshPro Text for Hero 1 HP
- Slider for Hero 1 HP Bar
- TextMeshPro Text for Hero 2 Name
- TextMeshPro Text for Hero 2 HP
- Slider for Hero 2 HP Bar

**Enemy Squad UI:**
- TextMeshPro Text for Villain 1 Name
- TextMeshPro Text for Villain 1 HP
- Slider for Villain 1 HP Bar
- TextMeshPro Text for Villain 2 Name
- TextMeshPro Text for Villain 2 HP
- Slider for Villain 2 HP Bar

**Turn Info:**
- TextMeshPro Text for Turn Display
- TextMeshPro Text for Message Display

**Action Buttons:**
- Button with TextMeshPro Text "Attack"
- Button with TextMeshPro Text "Defend"
- Button with TextMeshPro Text "Special"

**Target Selection Panel:**
- Panel (initially hidden)
- Button for Target 1 with TextMeshPro Text
- Button for Target 2 with TextMeshPro Text

4. Assign all UI elements to the BattleUI script component
5. Assign the BattleUI component to the BattleManager

## Game Mechanics

### Damage Calculation
```
Actual Damage = max(1, Attack - Defense)
```
This ensures that every attack deals at least 1 damage.

### Turn Order
- All 4 characters (2 heroes + 2 villains) take turns in sequence
- Dead characters are automatically skipped
- A new round begins after all characters have taken their turn

### Special Attack
- Deals 2x the character's attack stat
- Uses the same damage calculation formula
- Can be used every turn (no cooldown)

### Enemy AI
- Enemies automatically select a random alive player character to attack
- Simple but effective AI for demonstration purposes

## Extending the System

### Adding More Characters
In `BattleManager.cs`, modify the `SetupBattle()` method:
```csharp
playerSquad.Add(new Character("Hero 3", 90, 18, 4, true));
enemySquad.Add(new Character("Villain 3", 85, 14, 5, false));
```

### Adding New Actions
1. Add a new enum value to `BattleAction` in `BattleManager.cs`
2. Add a button in the UI and assign it in `BattleUI.cs`
3. Implement the action logic in the `PlayerTurn` method

### Customizing Stats
Modify the character initialization in `SetupBattle()`:
```csharp
new Character("Name", HP, Attack, Defense, isPlayer)
```

## Technical Details

- **Unity Version**: 2022.3.10f1
- **Scripting Backend**: Mono
- **API Compatibility Level**: .NET Standard 2.1
- **Target Platform**: Standalone (Windows/Mac/Linux)
- **Project Type**: 2D

## Dependencies

- Unity Engine
- TextMeshPro (included with Unity)

## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

## Future Enhancements

Potential features to add:
- Character sprites and animations
- Sound effects and music
- Item system
- Status effects (poison, stun, etc.)
- Experience and leveling system
- Multiple battle scenes
- Save/Load system
- More complex enemy AI
- Combo attacks between squad members
