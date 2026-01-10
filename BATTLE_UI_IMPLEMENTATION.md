# Battle Scene UI Implementation

## Overview
This implementation adds a complete battle UI setup that automatically creates all necessary UI elements at runtime for the turn-based battle system.

## What Was Implemented

### 1. Automatic UI Setup
- **BattleUISetup.cs**: A new script that automatically generates all UI elements when the scene starts
- The UI is created programmatically, eliminating the need for manual Unity Editor setup
- All UI elements are properly positioned and styled

### 2. Turn Order Display
Located at the top of the screen:
- **Current Turn**: Shows which character is currently taking their turn
- **Next Turn**: Shows which character will go next (new feature)

### 3. Character Displays

#### Player Squad (Left Side - Green)
- Hero 1: Name, HP text, HP bar (100 HP, 15 ATK, 5 DEF)
- Hero 2: Name, HP text, HP bar (80 HP, 20 ATK, 3 DEF)

#### Enemy Squad (Right Side - Red)
- Villain 1: Name, HP text, HP bar (70 HP, 12 ATK, 4 DEF)
- Villain 2: Name, HP text, HP bar (90 HP, 10 ATK, 6 DEF)

### 4. Action Buttons (Bottom Center)
Three action buttons for player turns:
- **ATTACK**: Perform a normal attack
- **DEFEND**: Take a defensive stance
- **SPECIAL**: Perform a 2x damage attack

### 5. Target Selection Panel
A modal panel that appears when selecting a target:
- Shows available enemy targets
- Only displays alive enemies
- Allows clicking to select target

### 6. Message Display
Center screen message area that shows:
- Battle start notification
- Current action descriptions
- Damage dealt
- Battle outcome (Victory/Defeat)

## Technical Details

### Files Modified
1. **Assets/Scripts/BattleUISetup.cs** (NEW)
   - Creates Canvas with ScreenSpace-Overlay rendering
   - Generates all UI elements programmatically
   - Sets up proper UI hierarchy
   - Wires up BattleUI component

2. **Assets/Scripts/TurnManager.cs** (ENHANCED)
   - Added `GetNextCharacter()` method to track upcoming turn

3. **Assets/Scripts/BattleUI.cs** (ENHANCED)
   - Added `nextTurnText` field
   - Added `UpdateNextTurnText()` method

4. **Assets/Scripts/BattleManager.cs** (ENHANCED)
   - Now updates next turn display during battle flow

5. **Assets/Scenes/BattleScene.unity** (UPDATED)
   - Added BattleUISetup component to BattleSystem GameObject

### UI Layout
```
┌────────────────────────────────────────┐
│     Turn: Hero 1    Next: Villain 1    │  ← Turn Order Display
├──────────────┬──────────┬──────────────┤
│ Hero 1       │          │    Villain 1 │
│ 100/100      │          │        70/70 │
│ [████████]   │          │   [████████] │
│              │          │              │
│ Hero 2       │ Battle   │    Villain 2 │
│ 80/80        │ Message  │        90/90 │
│ [████████]   │          │   [████████] │
├──────────────┴──────────┴──────────────┤
│      [ATTACK] [DEFEND] [SPECIAL]       │  ← Action Buttons
└────────────────────────────────────────┘
```

## How It Works

1. **Scene Loads**: BattleUISetup.Awake() is called first (execution order -100)
2. **UI Creation**: All UI elements are generated programmatically
3. **Component Wiring**: BattleUI component is created and all references are assigned
4. **BattleManager Start**: Battle begins automatically
5. **Turn Flow**: 
   - Turn text shows current character
   - Next turn text shows upcoming character
   - Player sees action buttons on their turn
   - Enemy turn plays automatically
6. **Battle Continues**: Until one side is defeated

## Color Coding
- **Green**: Player characters (Heroes)
- **Red**: Enemy characters (Villains)
- **Gray**: Next turn indicator
- **White**: Current turn and messages
- **Dark Gray**: UI panels and buttons

## Testing
To test this implementation in Unity:
1. Open the project in Unity Editor (2022.3.10f1 or later)
2. Open `Assets/Scenes/BattleScene.unity`
3. Press Play
4. The UI will be automatically created
5. Battle will start automatically
6. Click action buttons during hero turns
7. Select targets when prompted
8. Watch the turn order at the top
9. Battle continues until victory or defeat

## Features
✅ 2 Player Characters with stats
✅ 2 Enemy Characters with stats
✅ Turn order display (current + next)
✅ HP bars with real-time updates
✅ 3 action types (Attack, Defend, Special)
✅ Target selection system
✅ Automatic battle flow
✅ Win/Lose conditions
✅ Clean, organized UI layout
✅ Color-coded teams
✅ EventSystem for UI input handling

## Benefits of This Approach
1. **No Manual Setup**: Everything is created automatically
2. **Version Control Friendly**: Less complex scene files to merge
3. **Maintainable**: UI layout is in readable C# code
4. **Consistent**: UI will be the same every time
5. **Flexible**: Easy to modify positions, colors, and sizes
6. **Self-Contained**: All UI logic in one script
