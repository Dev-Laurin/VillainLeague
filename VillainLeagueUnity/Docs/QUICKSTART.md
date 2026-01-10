# Quick Start Guide

## What This Project Contains

This is a complete Unity 2D turn-based battle system with:
- 2 player characters in your squad
- 2 enemy villains to fight
- Turn-based combat similar to Mario & Luigi series
- Attack, Defend, and Special move actions
- HP tracking and damage calculation

## Opening in Unity

1. **Install Unity**
   - Download Unity Hub from https://unity.com/download
   - Install Unity 2022.3.10f1 or later (LTS version recommended)

2. **Add Project to Unity Hub**
   - Open Unity Hub
   - Click "Add" or "Open"
   - Navigate to your cloned VillainLeague project folder
   - Select the folder and click "Open"

3. **Open the Project**
   - Click on the project in Unity Hub to open it
   - Unity will import all assets (this may take a few minutes the first time)

## Setting Up the Battle Scene

The project includes a basic battle scene at `Assets/Scenes/BattleScene.unity`. However, to make it fully functional, you'll need to add UI elements:

### Required UI Setup (In Unity Editor):

1. **Open BattleScene.unity**
   - In Unity, go to Project window
   - Navigate to Assets/Scenes
   - Double-click BattleScene.unity

2. **Create a Canvas**
   - Right-click in Hierarchy
   - Select UI > Canvas
   - In Canvas component, set Render Mode to "Screen Space - Overlay"
   - On Canvas Scaler component, set UI Scale Mode to "Scale With Screen Size"
   - Set Reference Resolution to 1920x1080

3. **Import TextMeshPro**
   - When prompted to import TMP Essentials, click "Import TMP Essentials"
   - This is required for text display in the battle system

4. **Add UI Elements to Canvas**
   
   Create these UI elements as children of the Canvas:
   
   **Panel for Player Info (Left side):**
   - Panel (name it "PlayerPanel")
   - Position: X: -400, Y: 200
   - Add these as children:
     - TextMeshPro Text: "Hero 1" (name: Player1Name)
     - TextMeshPro Text: "100/100" (name: Player1HP)
     - Slider (name: Player1HPBar)
     - TextMeshPro Text: "Hero 2" (name: Player2Name)
     - TextMeshPro Text: "80/80" (name: Player2HP)
     - Slider (name: Player2HPBar)

   **Panel for Enemy Info (Right side):**
   - Panel (name it "EnemyPanel")
   - Position: X: 400, Y: 200
   - Add these as children:
     - TextMeshPro Text: "Villain 1" (name: Enemy1Name)
     - TextMeshPro Text: "70/70" (name: Enemy1HP)
     - Slider (name: Enemy1HPBar)
     - TextMeshPro Text: "Villain 2" (name: Enemy2Name)
     - TextMeshPro Text: "90/90" (name: Enemy2HP)
     - Slider (name: Enemy2HPBar)

   **Turn and Message Display (Top center):**
   - TextMeshPro Text (name: TurnText)
     - Position: Y: 300
     - Text: "Turn: Hero 1"
     - Font Size: 36
   - TextMeshPro Text (name: MessageText)
     - Position: Y: 0
     - Text: "Select an action..."
     - Font Size: 24

   **Action Buttons (Bottom center):**
   - Button (name: AttackButton)
     - Position: X: -200, Y: -200
     - Text: "Attack"
   - Button (name: DefendButton)
     - Position: X: 0, Y: -200
     - Text: "Defend"
   - Button (name: SpecialButton)
     - Position: X: 200, Y: -200
     - Text: "Special"

   **Target Selection Panel (Center, initially hidden):**
   - Panel (name: TargetSelectionPanel)
     - Position: center of screen
     - Add these as children:
       - Button (name: Target1Button) - Text: "Target 1"
       - Button (name: Target2Button) - Text: "Target 2"

5. **Wire Up the BattleUI Script**
   - Select the BattleSystem GameObject in Hierarchy
   - Find the BattleUI component in Inspector
   - Drag and drop each UI element from the Hierarchy to its corresponding field in the BattleUI component
   - Drag the BattleUI component to the BattleManager's "Battle UI" field
   - The TurnManager should already be assigned

6. **Save the Scene**
   - Press Ctrl+S (Windows) or Cmd+S (Mac)

## Playing the Game

1. Click the Play button at the top of Unity Editor
2. The battle will start automatically
3. When it's a player character's turn:
   - Click "Attack" to do normal damage
   - Click "Defend" to take a defensive stance
   - Click "Special" to do 2x damage
4. When you choose Attack or Special, select a target enemy
5. Watch as enemies take their turns automatically
6. Battle ends when all heroes or all villains are defeated

## Game Mechanics

### Characters

**Your Squad:**
- Hero 1: 100 HP, 15 Attack, 5 Defense
- Hero 2: 80 HP, 20 Attack, 3 Defense

**Enemy Squad:**
- Villain 1: 70 HP, 12 Attack, 4 Defense
- Villain 2: 90 HP, 10 Attack, 6 Defense

### Actions

- **Attack**: Deals damage equal to your Attack stat minus enemy Defense (minimum 1)
- **Defend**: Protects the character for that turn (currently implemented, can be enhanced)
- **Special**: Deals 2x your Attack stat minus enemy Defense (minimum 1)

### Turn Order

1. All 4 characters take turns in order
2. Dead characters are automatically skipped
3. Enemies use simple AI to attack random alive players
4. Battle continues until one side is completely defeated

## Troubleshooting

**"Missing Script" errors:**
- Make sure all .cs files are in the Assets/Scripts folder
- Wait for Unity to finish compiling scripts (check bottom-right of Unity Editor)

**UI doesn't appear:**
- Make sure you created all UI elements and assigned them in the BattleUI component
- Check that Canvas is set to "Screen Space - Overlay"

**Battle doesn't start:**
- Check the Console window (Window > General > Console) for error messages
- Make sure BattleManager and TurnManager are both on the BattleSystem GameObject

**TextMeshPro errors:**
- Import TMP Essentials when prompted
- Or go to Window > TextMeshPro > Import TMP Essential Resources

## Next Steps

After setting up the basic UI, you can:
- Add character sprites and animations
- Implement sound effects
- Add more complex battle mechanics
- Create multiple battle scenarios
- Add a main menu and victory/defeat screens

For more detailed information, see BATTLE_SYSTEM_README.md
