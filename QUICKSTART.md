# Character Moveset System - Quick Start Guide

## What Was Implemented

This PR implements a complete character moveset system that allows each character to have unique abilities with resource costs, replacing the simple attack/defend/special system.

## Quick Overview

### For Players
- Each character now has unique moves with different effects
- Moves cost resources (Focus, Energy, etc.) that regenerate each turn
- Move selection UI shows all available moves with descriptions and costs
- Moves that are too expensive are grayed out
- Cecelia Sylvan has 10 unique moves as specified in the issue

### For Developers
- Easy to add new characters with custom movesets
- Factory pattern for creating movesets
- ScriptableObject support for Unity assets
- Backward compatible with old battle system
- Extensible for adding new move types and effects

## How to Use

### Testing in Unity

1. **Open the project in Unity**
2. **Open the battle scene**
3. **Enter Play Mode**
4. **Wait for battle to start**
5. **When it's Cecelia's turn:**
   - Move selection panel will appear
   - Shows all 10 of her moves
   - Click a move to select it
   - Click a target if needed
   - Move executes and Focus is spent

### Key Things to Test

1. ✅ Resource display shows "Focus: 6/6" under Cecelia
2. ✅ Move panel appears with all 10 moves
3. ✅ Clicking Twin Slash (free) works immediately
4. ✅ Other moves require Focus
5. ✅ After using a move, Focus decreases
6. ✅ Next turn, Focus regenerates (+1)
7. ✅ Expensive moves gray out when unaffordable
8. ✅ Hero 2 has a different moveset with Energy
9. ✅ Enemies use moves from their movesets

### Adding a New Character

```csharp
// In MoveSetFactory.cs, add a new method:
public static CharacterMoveSet CreateNewHeroMoveSet()
{
    CharacterMoveSet moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
    moveSet.characterName = "New Hero";
    moveSet.role = "Tank / Support";
    moveSet.resource = new CharacterResource("Mana", 12, 2);
    moveSet.moves = new List<Move>();
    
    // Add moves
    Move tackle = new Move("tackle", "Tackle", "Charge at enemy", 0);
    tackle.damage = 4;
    moveSet.moves.Add(tackle);
    
    Move shield = new Move("shield", "Shield Wall", "Raise shield", 3);
    shield.defenseBuff = 5;
    shield.durationTurns = 2;
    shield.targetType = MoveTargetType.Self;
    moveSet.moves.Add(shield);
    
    // Add more moves...
    
    return moveSet;
}

// In BattleManager.cs SetupBattle():
Character newHero = new Character("New Hero", 120, 10, 8, true);
newHero.SetMoveSet(MoveSetFactory.CreateNewHeroMoveSet());
playerSquad.Add(newHero);
```

## File Structure

```
VillainLeagueUnity/
├── Assets/Scripts/
│   ├── Move.cs                    # Move data structure
│   ├── CharacterMoveSet.cs        # Moveset and resource management
│   ├── MoveSetFactory.cs          # Factory for creating movesets
│   ├── Character.cs               # Modified to support movesets
│   ├── BattleManager.cs           # Modified for move system
│   ├── BattleUI.cs                # Modified to display moves
│   └── BattleUISetup.cs           # Modified to add move panel
├── Docs/
│   ├── MovesetSystem.md           # Detailed system documentation
│   ├── UI_Mockup.md               # Visual UI mockup
│   └── Architecture.md            # System architecture diagrams
└── IMPLEMENTATION_SUMMARY.md      # Complete implementation overview
```

## Cecelia Sylvan's Moves

| Move | Cost | Effect |
|------|------|--------|
| Twin Slash | 0 | 3 damage, 2 hits |
| Blink Strike | 2 | 5 damage, ignores guard |
| Flash Step | 1 | Move 5, evasion 2 |
| Hero's Intercept | 2 | Protect ally, armor 3 |
| Rally Heart | 2 | AoE attack buff +2 |
| Severing Cut | 1 | 4 damage, bleed 2 |
| Swordbind | 1 | 2 damage, attack -2 |
| Piercing Lunge | 2 | 5 damage, pierce 2 |
| Blade Whirl | 2 | 3 damage AoE |
| Afterimage | 2 | Evasion 2, counter 3 |

## Documentation

- **MovesetSystem.md** - Complete system documentation
- **UI_Mockup.md** - Visual representation of the UI
- **Architecture.md** - System architecture with diagrams
- **IMPLEMENTATION_SUMMARY.md** - Implementation details

## Technical Details

### Classes Created
- `Move` - Individual move/ability
- `CharacterResource` - Resource pool management
- `CharacterMoveSet` - Character's complete moveset
- `MoveSetFactory` - Factory for creating movesets
- `MoveTargetType` - Enum for target types

### Key Features
- Resource regeneration system
- Cost validation and affordability checking
- Dynamic UI generation
- Multiple targeting options
- Visual feedback (grayed out when unaffordable)
- Backward compatibility

## Future Enhancements (Not Implemented)

The following could be added in the future:
- Status effect tracking and visualization
- Move animations
- Sound effects
- Move cooldowns
- Move range visualization on grid
- Undo move selection
- Keyboard shortcuts
- Detailed move tooltips on hover
- Save/load movesets from files

## Troubleshooting

**Q: Move panel doesn't appear**
- Check that character has a moveset assigned
- Verify `character.SetMoveSet()` was called
- Check console for errors

**Q: All moves are grayed out**
- Check resource current value
- Verify resource regeneration is working
- Check move costs

**Q: Clicking move does nothing**
- Check that move is affordable
- Verify button listener is attached
- Check console for errors

**Q: Resource not regenerating**
- Check `regenPerTurn` value
- Verify `Regenerate()` is called at turn start
- Check that resource isn't already at max

## Support

For questions or issues:
1. Check the documentation in the `Docs/` folder
2. Review `IMPLEMENTATION_SUMMARY.md`
3. Check the issue tracker
4. Review the code comments

## Credits

Implemented as part of Issue: "Creating movesets"
- Cecelia Sylvan moveset as specified in the issue
- Resource system (Focus: 6 max, +1 per turn)
- All 10 moves with proper costs and effects
