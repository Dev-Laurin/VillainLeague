# Character Moveset System

This document describes the moveset system implementation for the Villain League battle system.

## Overview

The moveset system allows each character to have unique moves with different effects, costs, and targeting options. Each character has a resource pool (e.g., Focus, Energy) that regenerates each turn and is spent when using moves.

## Core Classes

### Move.cs
Defines a single move/ability with properties:
- `moveName`: Display name of the move
- `description`: Description shown in UI
- `resourceCost`: How much resource is required to use this move
- `damage`: Base damage dealt
- `hits`: Number of hits (for multi-hit moves)
- `healing`: Amount of healing provided
- Various buff/debuff values and special effects
- `targetType`: Who this move can target (SingleEnemy, AllEnemies, Self, etc.)

### CharacterResource.cs
Manages a character's resource pool:
- `resourceName`: Display name (e.g., "Focus", "Energy")
- `maxResource`: Maximum amount
- `currentResource`: Current amount
- `regenPerTurn`: How much regenerates each turn
- Methods: `Regenerate()`, `CanAfford()`, `Spend()`

### CharacterMoveSet.cs
Contains a character's complete moveset:
- `characterName`: Name of the character
- `role`: Character role description
- `resource`: The resource pool for this character
- `moves`: List of available moves

### MoveSetFactory.cs
Factory class that creates pre-defined movesets:
- `CreateCeceliaSylvanMoveSet()`: Creates Cecelia's 10 moves with Focus resource
- `CreateDefaultMoveSet()`: Creates a basic moveset for other characters

## Usage

### In BattleManager

Characters are initialized with movesets:
```csharp
Character cecelia = new Character("Cecelia Sylvan", 100, 15, 5, true);
cecelia.SetMoveSet(MoveSetFactory.CreateCeceliaSylvanMoveSet());
```

### Move Selection Flow

1. On player turn, if character has a moveset, display move selection UI
2. Player selects a move from the list
3. System checks if character has enough resource
4. Player selects target if needed (based on move's targetType)
5. Move is executed and resource is spent
6. Resource regenerates at start of next turn

## Cecelia Sylvan's Moves

1. **Twin Slash** - Free basic attack (2 hits)
2. **Blink Strike** - Teleport strike that ignores guard (Cost: 2)
3. **Flash Step** - Mobility + evasion buff (Cost: 1)
4. **Hero's Intercept** - Protect ally (Cost: 2)
5. **Rally Heart** - AoE attack buff (Cost: 2)
6. **Severing Cut** - Damage + bleed (Cost: 1)
7. **Swordbind** - Damage + attack debuff (Cost: 1)
8. **Piercing Lunge** - Armor-piercing damage (Cost: 2)
9. **Blade Whirl** - AoE spin attack (Cost: 2)
10. **Afterimage** - Evasion + counter buff (Cost: 2)

## UI Changes

- Added resource display for each player character (e.g., "Focus: 6/6")
- Move selection panel shows all available moves with:
  - Move name
  - Description
  - Resource cost
  - Grayed out if not affordable
- Replaces old Attack/Defend/Special buttons when character has moveset
- Falls back to old system for characters without movesets

## Adding New Characters

To add a new character with a custom moveset:

1. Create a new static method in `MoveSetFactory`
2. Define the character's resource pool
3. Create Move objects for each ability
4. Add moves to the moveset
5. In `BattleManager.SetupBattle()`, assign the moveset to the character

Example:
```csharp
Character newHero = new Character("Hero Name", 100, 15, 5, true);
newHero.SetMoveSet(MoveSetFactory.CreateHeroMoveSet());
playerSquad.Add(newHero);
```
