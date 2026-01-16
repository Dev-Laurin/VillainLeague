# Battle Objectives Guide

This guide explains how to use the different battle objective types in the Villain League battle system.

## Overview

The battle system now supports multiple objective types beyond simply defeating all enemies. Each objective type changes the win/lose conditions and can modify gameplay mechanics.

## Objective Types

### 1. Defeat All Enemies (Default)
**Description:** Classic battle mode - eliminate all enemies to win.

**Setup:**
```csharp
battleManager.SetObjectiveDefeatAllEnemies();
```

**Win Condition:** All enemies defeated
**Lose Condition:** All players defeated

---

### 2. Defend NPC
**Description:** Protect an NPC character from being defeated. The NPC is positioned behind heroes, and moves that could harm allies (like counterattacks) are automatically filtered out.

**Setup:**
```csharp
// Create the NPC to protect
Character npc = new Character("Villager", 50, 0, 2, true);
playerSquad.Add(npc); // Add to player squad

// Set objective
battleManager.SetObjectiveDefendNPC(npc);
```

**Win Condition:** All enemies defeated AND NPC still alive
**Lose Condition:** NPC defeated OR all players defeated

**Special Mechanics:**
- Moves with `canHarmAllies = true` are filtered out during move selection
- NPC should be added to player squad but positioned behind heroes

---

### 3. Reduce to HP Threshold
**Description:** Weaken all enemies to a specific HP or lower without knocking them out.

**Setup:**
```csharp
// Reduce all enemies to 10 HP or lower
battleManager.SetObjectiveReduceToThreshold(10);
```

**Win Condition:** All enemies at or below the HP threshold
**Lose Condition:** All players defeated

**Special Mechanics:**
- Enemies cannot be reduced below 1 HP
- Damage is automatically capped to keep enemies alive
- Works with both single-target and AoE attacks

---

### 4. Survive X Turns
**Description:** Survive for a specific number of turns without being defeated.

**Setup:**
```csharp
// Survive for 15 turns
battleManager.SetObjectiveSurviveTurns(15);
```

**Win Condition:** Turn count reaches the target number
**Lose Condition:** All players defeated before reaching the turn count

**Special Mechanics:**
- Turn counter increments after each character's turn
- Display shows current turn progress

---

### 5. Finish With Mana
**Description:** Complete the battle (defeat all enemies) while maintaining a specific total mana threshold.

**Setup:**
```csharp
// Must have at least 8 total mana remaining when battle ends
battleManager.SetObjectiveFinishWithMana(8);
```

**Win Condition:** All enemies defeated AND total party mana >= threshold
**Lose Condition:** All enemies defeated but mana below threshold OR all players defeated

**Special Mechanics:**
- Calculates total mana across all living player characters
- Only checks threshold when enemies are defeated
- Encourages conservative resource management

---

### 6. Charm Opponents
**Description:** Win by charming all opponents instead of defeating them. Charm points are earned through special moves.

**Setup:**
```csharp
// Each enemy needs 10 charm points to be charmed
battleManager.SetObjectiveCharmOpponents(10);
```

**Win Condition:** All enemies have charm points >= required amount
**Lose Condition:** All players defeated

**Special Mechanics:**
- Moves with `charmPoints > 0` add to enemy charm counters
- Charm progress tracked per enemy
- Moves should be marked with charm values in their configuration

**Example Charm Move:**
```csharp
Move charmingSmile = new Move("charm_smile", "Charming Smile", "Win them over with your charm", 2);
charmingSmile.charmPoints = 3; // Adds 3 charm points
charmingSmile.targetType = MoveTargetType.SingleEnemy;
```

---

### 7. Limited Visibility
**Description:** Battle takes place in darkness. Characters must use "Vein Vision" magic to see, which costs mana.

**Setup:**
```csharp
// Vein Vision costs 1 mana per use
battleManager.SetObjectiveLimitedVisibility(1);
```

**Win Condition:** All enemies defeated
**Lose Condition:** All players defeated

**Special Mechanics:**
- `visibilityEnabled = false` in objective
- UI can check this flag to dim/hide enemy information
- Requires special vein vision move to reveal enemies
- Vein vision costs mana each time it's used

**Implementation Notes:**
- Visual implementation (hiding enemy sprites/UI) should be added to BattleUI
- Vein vision move should be added to appropriate character movesets
- Effect could last for several turns or require constant upkeep

---

## Usage Examples

### Example 1: NPC Escort Mission
```csharp
void SetupEscortBattle()
{
    // Create player heroes
    Character hero1 = new Character("Bellinor", 120, 18, 6, true);
    Character hero2 = new Character("Naice", 80, 20, 3, true);
    playerSquad.Add(hero1);
    playerSquad.Add(hero2);
    
    // Create NPC to protect
    Character npc = new Character("Princess Elara", 50, 0, 2, true);
    playerSquad.Add(npc);
    
    // Create enemies
    Character enemy1 = new Character("Bandit", 60, 15, 3, false);
    Character enemy2 = new Character("Thief", 70, 12, 4, false);
    enemySquad.Add(enemy1);
    enemySquad.Add(enemy2);
    
    // Set defend objective
    battleManager.SetObjectiveDefendNPC(npc);
}
```

### Example 2: Training Battle
```csharp
void SetupTrainingBattle()
{
    // Setup characters...
    
    // Goal: Reduce sparring partners to 15 HP without knocking them out
    battleManager.SetObjectiveReduceToThreshold(15);
}
```

### Example 3: Endurance Challenge
```csharp
void SetupEnduranceChallenge()
{
    // Setup characters...
    
    // Goal: Survive 20 turns against waves of enemies
    battleManager.SetObjectiveSurviveTurns(20);
}
```

### Example 4: Diplomatic Mission
```csharp
void SetupDiplomaticBattle()
{
    // Setup characters...
    
    // Goal: Charm all opponents (5 points each) instead of fighting
    battleManager.SetObjectiveCharmOpponents(5);
    
    // Make sure characters have charm moves!
}
```

## Adding Objective Support to Moves

When creating character movesets, you can add support for objectives:

```csharp
// Move that adds charm points
Move diplomaticApproach = new Move("diplomatic", "Diplomatic Approach", "Use words, not swords", 2);
diplomaticApproach.charmPoints = 2;
diplomaticApproach.targetType = MoveTargetType.SingleEnemy;

// Move that could harm allies (filtered in NPC defense)
Move dangerousCounter = new Move("risky_counter", "Risky Counter", "Dangerous counterattack", 1);
dangerousCounter.counterDamage = 5;
dangerousCounter.canHarmAllies = true; // Will be filtered in NPC defense objective
```

## UI Integration

The objective description is automatically displayed at battle start:
- `battleObjective.objectiveDescription` contains the description
- Success/failure messages are customized per objective type
- Consider adding progress indicators for:
  - Turn counter for Survive objectives
  - Charm progress bars for Charm objectives
  - Mana tracking for Finish With Mana objectives
  - Enemy HP display for Reduce to Threshold objectives

## Best Practices

1. **Always initialize objective before battle starts** - Set it in `SetupBattle()` or before calling `StartCoroutine(BattleFlow())`
2. **Provide appropriate moves for objective type** - Charm objectives need charm moves, etc.
3. **Test objective balance** - Ensure objectives are achievable but challenging
4. **Combine with appropriate enemy AI** - Some objectives work better with specific enemy behaviors
5. **Use clear descriptions** - Update `objectiveDescription` to help players understand the goal
