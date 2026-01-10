# Testing Guide

This guide explains how to test and verify the battle system implementation.

## Testing Without Full UI Setup

While the complete UI setup is optional for initial testing, you can verify the core battle system logic using Unity's Console window.

### 1. Open the Project in Unity

```bash
1. Open Unity Hub
2. Add/Open the VillainLeague project
3. Open Assets/Scenes/BattleScene.unity
```

### 2. Check Script Compilation

1. Look at the bottom-right corner of Unity Editor
2. It should say "All Scripts compiled successfully"
3. If there are errors, check the Console window (Window > General > Console)

### 3. View Console Logs

The battle system outputs detailed logs to help track the battle flow:

1. Open Console window: `Window > General > Console`
2. Press Play
3. Watch the logs:
   - "Turn order initialized"
   - "Turn: [Character Name]"
   - "[Character] takes X damage! HP: Y/Z"
   - "New round started!"
   - "Player wins!" or "Player loses!"

### Example Console Output

```
Turn order initialized
Turn: Hero 1
Hero 1's turn!
Villain 1 takes 10 damage! HP: 60/70
Turn: Hero 2
Hero 2's turn!
Villain 2 takes 14 damage! HP: 76/90
Turn: Villain 1
Villain 1's turn!
Hero 1 takes 7 damage! HP: 93/100
Turn: Villain 2
Villain 2's turn!
Hero 2 takes 7 damage! HP: 73/80
New round started!
...
Player wins!
```

## Testing Individual Components

### Test Character System

1. Open `Assets/Scripts/BattleSystemExample.cs`
2. Uncomment the test methods in `Start()`:
   ```csharp
   void Start()
   {
       ExampleCreateCharacters();
       ExampleDamageCalculation();
       ExampleHealing();
       ExampleCheckAlive();
   }
   ```
3. Attach this script to any GameObject in the scene
4. Press Play and check Console for output

### Test Turn Manager

The TurnManager is tested automatically when BattleManager runs. Check logs for:
- Turn order initialization
- Proper turn rotation
- Dead character skipping
- Team status checking

### Test Battle States

Battle states automatically transition. Watch Console for:
- `START` → `PLAYER_TURN` or `ENEMY_TURN`
- `PLAYER_TURN` ↔ `ENEMY_TURN` rotation
- Final state: `WON` or `LOST`

## Manual Testing Checklist

Even without full UI, you can test the system:

### Core Functionality
- [ ] Battle initializes successfully
- [ ] All 4 characters are created with correct stats
- [ ] Turn order is established
- [ ] Turns rotate correctly
- [ ] Damage calculation works (Attack - Defense, min 1)
- [ ] HP updates correctly
- [ ] Dead characters are skipped
- [ ] Battle ends when one team is defeated
- [ ] Victory/defeat is detected correctly

### Character System
- [ ] Characters have correct initial HP
- [ ] TakeDamage reduces HP correctly
- [ ] HP never goes below 0
- [ ] IsAlive returns correct status
- [ ] Heal increases HP correctly
- [ ] Heal doesn't exceed max HP

### Turn System
- [ ] Turn order includes all characters
- [ ] Current turn advances properly
- [ ] Round counter works
- [ ] Team status checks work

## Testing With Full UI

After setting up the UI (see QUICKSTART.md):

### Visual Testing
- [ ] Character names display correctly
- [ ] HP values display correctly
- [ ] HP bars update visually
- [ ] Turn indicator shows current character
- [ ] Messages display battle events
- [ ] Action buttons appear on player turns
- [ ] Action buttons hide during enemy turns
- [ ] Target selection shows available enemies
- [ ] Target selection works correctly

### Interaction Testing
- [ ] Attack button selects attack action
- [ ] Defend button selects defend action
- [ ] Special button selects special action
- [ ] Can select first target
- [ ] Can select second target
- [ ] Buttons respond to clicks
- [ ] Battle flows smoothly

### Battle Flow Testing
1. **Start a Battle**
   - Battle should start automatically
   - Initial character stats should be correct

2. **Player Turn**
   - Action buttons should appear
   - Select Attack
   - Target selection should appear
   - Select a target
   - Damage should be applied
   - HP should update

3. **Enemy Turn**
   - Action buttons should hide
   - Enemy should attack automatically
   - Damage should be applied to player
   - HP should update

4. **Continue Battle**
   - Turns should rotate properly
   - All characters should get their turns
   - Dead characters should be skipped

5. **Battle End**
   - Battle should end when one team is defeated
   - Victory/defeat message should appear

## Performance Testing

### Frame Rate
- [ ] Battle runs smoothly (60 FPS)
- [ ] No lag during turn transitions
- [ ] UI updates don't cause stuttering

### Memory
- [ ] No memory leaks during battle
- [ ] Memory usage is stable
- [ ] Can restart battle without issues

## Edge Cases to Test

### Battle Scenarios
- [ ] What if all players have 1 HP?
- [ ] What if all enemies have 1 HP?
- [ ] What if special attack kills an enemy?
- [ ] What if enemy kills a player?
- [ ] What if only one target remains?
- [ ] What if damage is 0 (high defense)?

### UI Edge Cases
- [ ] What if an HP bar is at 0?
- [ ] What if an HP bar is at max?
- [ ] What if a character name is very long?
- [ ] What if messages are very long?

## Debugging Tips

### Common Issues

**"Missing Script" Error**
- Check that all .cs files compiled successfully
- Look for syntax errors in Console
- Restart Unity Editor

**Battle Doesn't Start**
- Check BattleManager is on BattleSystem GameObject
- Check TurnManager is assigned
- Look for errors in Console

**No Console Output**
- Check Console is open (Window > General > Console)
- Check Console isn't filtered (Clear, Info, Warning, Error buttons)
- Check game is in Play mode

**UI Not Updating**
- Verify BattleUI script is assigned
- Verify all UI elements are assigned in Inspector
- Check for null reference errors in Console

### Debug Helpers

Add these to BattleManager for extra debugging:

```csharp
void OnGUI()
{
    GUI.Label(new Rect(10, 10, 200, 20), $"State: {currentState}");
    GUI.Label(new Rect(10, 30, 200, 20), $"Turn: {turnManager.GetCurrentCharacter()?.characterName}");
}
```

## Automated Testing (Future)

For future development, consider:
- Unit tests for Character class
- Unit tests for TurnManager
- Integration tests for battle flow
- UI tests for interaction
- Performance benchmarks

## Test Results Documentation

After testing, document:
- ✅ Features that work correctly
- ⚠️ Features with minor issues
- ❌ Features that don't work
- 💡 Suggestions for improvement

## Conclusion

The battle system is designed to work correctly even without full UI setup. The core logic is sound and can be tested through Console logs. Full visual testing requires UI setup as described in QUICKSTART.md.

**Minimum Testing**: Console logs verify core functionality
**Complete Testing**: Full UI setup allows interactive testing
**Production Ready**: Requires visual polish, sound, and additional features

---

For questions or issues, check the [GitHub Issues](https://github.com/Dev-Laurin/VillainLeague/issues) page.
