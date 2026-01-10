# How to Use the Battle Scene

## Quick Start

1. **Open Unity Hub**
   - Launch Unity Hub on your computer

2. **Add/Open Project**
   - Click "Open" in Unity Hub
   - Navigate to the VillainLeague folder
   - Click "Select Folder"

3. **Wait for Import**
   - Unity will import all assets (may take a few minutes on first load)
   - Wait for compilation to complete (check bottom right of Unity Editor)

4. **Open Battle Scene**
   - In the Project window (usually bottom), navigate to `Assets > Scenes`
   - Double-click `BattleScene.unity`

5. **Press Play**
   - Click the Play button (▶) at the top center of Unity Editor
   - The battle UI will automatically appear!

## What You'll See

When you press Play, the UI will automatically create itself and you'll see:

### Top of Screen
```
╔════════════════════════════════════╗
║  Turn: Hero 1      Next: Hero 2    ║
╚════════════════════════════════════╝
```

### Left Side (Your Team - Green)
```
┌─────────────────┐
│ Hero 1          │
│ 100/100         │
│ █████████████   │
│                 │
│ Hero 2          │
│ 80/80           │
│ █████████████   │
└─────────────────┘
```

### Right Side (Enemies - Red)
```
┌─────────────────┐
│      Villain 1  │
│          70/70  │
│   █████████████ │
│                 │
│      Villain 2  │
│          90/90  │
│   █████████████ │
└─────────────────┘
```

### Center
```
Battle Start!
```

### Bottom (Action Buttons)
```
┌─────────┐ ┌─────────┐ ┌─────────┐
│ ATTACK  │ │ DEFEND  │ │ SPECIAL │
└─────────┘ └─────────┘ └─────────┘
```

## Playing the Game

### During Your Turn (Hero 1 or Hero 2)

1. **Choose an Action**
   - Click **ATTACK** for normal damage
   - Click **DEFEND** to protect yourself
   - Click **SPECIAL** for 2x damage

2. **Select Target** (for Attack/Special)
   - A panel will appear in the center
   - Click on which villain you want to attack
   - Only living enemies are shown

3. **Watch the Result**
   - Message shows damage dealt
   - HP bars update in real-time
   - Turn automatically moves to next character

### During Enemy Turn (Villain 1 or Villain 2)

- Enemy automatically chooses a target
- Enemy attacks a random hero
- Damage is calculated and displayed
- Turn moves to next character

### Turn Order

The game rotates through all 4 characters:
1. Hero 1 (your turn - choose action)
2. Hero 2 (your turn - choose action)
3. Villain 1 (automatic)
4. Villain 2 (automatic)
5. Back to Hero 1...

**Note**: Dead characters are automatically skipped!

### Battle End

**Victory** - When both villains are defeated
- Message displays "Victory!"
- Battle stops

**Defeat** - When both heroes are defeated
- Message displays "Defeat..."
- Battle stops

## Example Battle Flow

1. **Battle starts**
   - UI appears automatically
   - Turn: Hero 1, Next: Hero 2

2. **Hero 1's turn**
   - You click ATTACK
   - Select Villain 1
   - "Hero 1 attacks Villain 1 for 11 damage!"
   - Villain 1 HP: 70 → 59

3. **Hero 2's turn**
   - Turn: Hero 2, Next: Villain 1
   - You click SPECIAL
   - Select Villain 1
   - "Hero 2 uses special attack on Villain 1 for 36 damage!"
   - Villain 1 HP: 59 → 23

4. **Villain 1's turn**
   - Turn: Villain 1, Next: Villain 2
   - "Villain 1 attacks Hero 1 for 7 damage!"
   - Hero 1 HP: 100 → 93

5. **Battle continues...**
   - Until one side is completely defeated

## Troubleshooting

### "Nothing happens when I press Play"
- Check the Console window (Window > General > Console)
- Look for error messages (red lines)
- Make sure BattleScene.unity is the active scene

### "UI looks wrong"
- The UI is created programmatically
- If it doesn't appear, check the Console for errors
- Make sure BattleUISetup script is on the BattleSystem GameObject

### "Can't click buttons"
- EventSystem is automatically created
- If buttons don't respond, check Console for errors

### "TextMeshPro errors"
- When prompted, import "TMP Essential Resources"
- Or go to Window > TextMeshPro > Import TMP Essential Resources

## Color Guide

- **Green** - Your heroes (the good guys)
- **Red** - Enemy villains (the bad guys)
- **Light Gray** - "Next turn" indicator
- **White** - Current turn and messages
- **Dark Gray** - UI backgrounds

## Tips

- **Attack** is reliable and always available
- **Special** does 2x damage but plan carefully
- **Defend** protects you (currently a placeholder)
- Focus on one enemy at a time for faster victory
- Watch the HP bars to see who needs protection
- Keep an eye on the turn order at the top

## What Makes This Cool

✨ **No Manual Setup Required** - UI creates itself automatically
✨ **Real-time Updates** - HP bars animate as damage is dealt
✨ **Smart Turn Order** - Dead characters are skipped
✨ **Clear Feedback** - Always know what's happening
✨ **Color Coded** - Easy to distinguish teams
✨ **Responsive** - Works at different screen sizes

Enjoy the battle! 🎮
