# Super Abilities System

## Overview

This PR implements a super ability system for characters in Villain League. Each character has a secondary resource (Style, Resolve, etc.) that charges up during battle. Once charged to maximum, the character can use their powerful ultimate ability.

## Features Implemented

### 1. Secondary Resource System
- **Naice Ajimi**: Has a "Style" bar (max 6 points)
  - Gains Style by using certain moves (Feinting Strike, Smoke & Mirrors, etc.)
  - No automatic regeneration - must be earned through combat
  
- **Bellinor Chabbeneoux**: Has a "Resolve" bar (max 6 points)
  - Gains Resolve by using protective moves (Guard Stance, Intercept, Rapid Mend)
  - No automatic regeneration - must be earned through combat

### 2. Ultimate Moves
- Each character has one ultimate move marked with `isSuper: true`
- Ultimate moves require:
  - Primary resource cost (Mana/Resolve from the primary resource bar)
  - Secondary resource cost (Style/Resolve from the secondary resource bar - must be at max)
  
- **Naice's Ultimate**: "Final Act"
  - Costs: 6 Mana + 6 Style
  - Effect: Creates illusions and deals 5 damage to all enemies
  
- **Bellinor's Ultimate**: "Oathbound Chorus"
  - Costs: 5 Resolve (primary) + 6 Resolve (secondary)
  - Effect: Heals all allies for 6 HP and grants 3 Armor

### 3. Team Super Attack
- When BOTH heroes have their secondary resources fully charged (6/6)
- On either hero's turn, the player is presented with three choices:
  1. **Normal Turn**: Use regular moves
  2. **Use Super**: Use this character's individual ultimate ability
  3. **Team Super**: Both heroes combine their powers for a devastating team attack
  
- **Team Super Attack**:
  - Depletes BOTH heroes' secondary resources completely
  - Deals 15 base damage to ALL enemies
  - Can only be used when both heroes are alive and charged

## UI Changes

### Secondary Resource Display
- Each hero now shows two resource bars:
  - Primary resource (Mana/Resolve) - shown in light blue
  - Secondary resource (Style/Resolve) - shown in gold/yellow
  
### Action Buttons
When both heroes have supers charged:
- **"NORMAL TURN"** button: Proceed with regular move selection
- **"USE SUPER"** button: Select from this character's super moves only
- **"TEAM SUPER"** button: Execute the devastating team attack

### Move Selection
- When selecting a super move, only ultimate moves are displayed
- Moves show both resource costs (primary and secondary)
- Unaffordable moves are grayed out

## How to Test in Unity

### Prerequisites
1. Open the project in Unity Editor (2022.3.10f1 or later)
2. Open the Battle Scene
3. Enter Play Mode

### Test Case 1: Charging Style (Naice)
1. Wait for Naice's turn
2. Use moves that grant Style:
   - **Feinting Strike** (0 cost, +1 Style)
   - **Smoke & Mirrors** (3 Mana, +1 Style)
   - **Cutting Remark** (2 Mana, +1 Style)
   - **False Opening** (3 Mana, +2 Style)
   - **Take a Bow** (0 cost, +2 Style)
3. Watch the "Style: X/6" bar fill up
4. Once at 6/6, the super is ready!

### Test Case 2: Charging Resolve (Bellinor)
1. Wait for Bellinor's turn
2. Use protective moves that grant Resolve:
   - **Guard Stance** (1 Resolve cost, +1 Resolve gain)
   - **Intercept** (2 Resolve cost, +2 Resolve gain)
   - **Rapid Mend** (2 Resolve cost, +1 Resolve gain)
3. Watch the "Resolve: X/6" bar fill up (secondary resource)
4. Once at 6/6, the super is ready!

### Test Case 3: Individual Super
1. Charge one hero's secondary resource to 6/6
2. When it's their turn and super is ready:
   - If both heroes are charged: Click "USE SUPER" button
   - If only this hero is charged: Move selection will include ultimate moves
3. Select the ultimate move from the list
4. Choose target if needed
5. Watch the ultimate execute and resources deplete

### Test Case 4: Team Super Attack
1. Charge BOTH heroes' secondary resources to 6/6
2. When either hero's turn comes up, three buttons appear:
   - NORMAL TURN
   - USE SUPER
   - TEAM SUPER
3. Click "TEAM SUPER"
4. Watch both heroes execute their combined attack
5. All enemies take 15 base damage
6. Both heroes' secondary resources drop to 0/6

### Expected Behaviors
- ✅ Secondary resource bars are visible and update correctly
- ✅ Style/Resolve gain messages appear when using appropriate moves
- ✅ Team Super button only appears when BOTH heroes are at 6/6
- ✅ Individual Super button appears when current hero is at 6/6
- ✅ Ultimate moves are marked and cost secondary resources
- ✅ Team Super depletes both heroes' secondary resources
- ✅ Cannot use super moves without full secondary resource

## Implementation Details

### Files Modified
1. **Move.cs**: Added `isSuper` and `secondaryResourceCost` fields
2. **Character.cs**: Added `IsSuperReady()` method
3. **BattleManager.cs**: 
   - Added super option selection logic
   - Added team super execution
   - Updated move execution to spend secondary resources
4. **BattleUI.cs**: 
   - Added super button UI elements
   - Updated move display to filter by super status
   - Updated affordability check for secondary resources
5. **BattleUISetup.cs**: Added super button creation
6. **MoveSetLoader.cs**: Added parsing for super move fields
7. **Character JSON files**: Marked ultimate moves with `isSuper: true` and `secondaryResourceCost: 6`

### Code Flow
```
PlayerTurn()
  ├─ AreBothHeroSupersReady() ?
  │   └─ Yes: SelectSuperOption()
  │       ├─ NORMAL TURN → regular SelectMove()
  │       ├─ USE SUPER → SelectMove(showOnlySupers=true)
  │       └─ TEAM SUPER → ExecuteTeamSuper()
  └─ No: SelectMove()
       └─ ExecuteMove()
           ├─ Check primary resource
           ├─ Check secondary resource (if isSuper)
           └─ Spend both resources
```

## Future Enhancements (Not Implemented)
- Gain Resolve when taking damage (requires damage event system)
- Gain Resolve when protecting allies (requires intercept logic)
- Super move animations
- Visual effects for team super
- Sound effects
- Combo multiplier for consecutive Style gains
- Different team super attacks based on character combinations

## Troubleshooting

**Q: Team Super button doesn't appear**
- Verify both heroes are alive
- Check both secondary resources are at 6/6
- Make sure it's a player's turn (not enemy)

**Q: Can't use ultimate move**
- Check both primary and secondary resources
- Ultimate moves need BOTH resources to be sufficient
- Secondary resource must be at maximum (6/6)

**Q: Style/Resolve not gaining**
- Only specific moves grant secondary resources
- Check move descriptions for "Gain Style" or "Gain Resolve"
- Verify moves were executed successfully

**Q: Secondary resource bar not showing**
- Check character has secondaryResource initialized
- Verify BattleUI has secondaryResourceText assigned
- Look in console for initialization errors
