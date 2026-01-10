# Character Moveset Configuration System

## Overview

Character movesets are now stored in JSON configuration files located in `Assets/StreamingAssets/CharacterMovesets/`. Each character has their own JSON file that defines their moves, resource pool, and role.

## File Location

```
Assets/StreamingAssets/CharacterMovesets/
├── Cecelia Sylvan.json
├── Hero 2.json
├── Villain 1.json
└── Villain 2.json
```

## JSON File Format

```json
{
  "characterName": "Character Name",
  "role": "Character role description",
  "resource": {
    "name": "Mana",
    "max": 12,
    "regenPerTurn": 2
  },
  "moves": [
    {
      "id": "move_id",
      "name": "Move Name",
      "description": "Move description shown in UI",
      "type": "physical",
      "manaCost": 0,
      "damage": 5,
      "targetType": "SingleEnemy"
    }
  ]
}
```

## Move Types

### Physical Attacks
- Set `"type": "physical"`
- Do NOT cost mana (always free)
- Display as "Physical" in the UI
- Example: Basic sword slashes, punches, kicks

### Magic Attacks
- Set `"type": "magic"`
- Cost mana as specified by `manaCost`
- Display as "Mana: X" in the UI
- Example: Spells, magical abilities, teleports

## Resource System

All characters now use **Mana** as their resource:
- `name`: Always "Mana"
- `max`: Maximum mana pool size
- `regenPerTurn`: How much mana regenerates each turn

## Move Properties

### Required Properties
- `id`: Unique identifier for the move
- `name`: Display name shown in UI
- `description`: Description shown in UI
- `type`: "physical" or "magic"
- `manaCost`: Mana cost (only for magic, set to 0 for physical)
- `targetType`: Who can be targeted

### Optional Properties
- `damage`: Damage dealt
- `hits`: Number of hits (for multi-hit moves)
- `healing`: Amount of healing
- `attackBuff`: Increase target's attack
- `attackDebuff`: Decrease target's attack
- `defenseBuff`: Increase target's defense
- `defenseDebuff`: Decrease target's defense
- `evasion`: Evasion bonus
- `armor`: Armor bonus
- `ignoreGuard`: Bypass guard effects (true/false)
- `armorPierce`: Amount of armor piercing
- `bleed`: Bleed damage over time
- `counterDamage`: Counter damage
- `radius`: Area of effect radius
- `moveRange`: Movement range
- `durationTurns`: Effect duration in turns

### Target Types
- `"SingleEnemy"` - Target one enemy
- `"SingleAlly"` - Target one ally
- `"AllEnemies"` - Target all enemies
- `"AllAllies"` - Target all allies
- `"Self"` - Target self only
- `"Area"` - Area of effect

## Adding a New Character

1. Create a new JSON file in `Assets/StreamingAssets/CharacterMovesets/`
2. Name it exactly as the character name (e.g., "New Hero.json")
3. Define the character's moves following the format above
4. The character will automatically load their moveset when created

Example:
```csharp
Character newHero = new Character("New Hero", 100, 15, 5, true);
newHero.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("New Hero"));
playerSquad.Add(newHero);
```

## Example: Cecelia Sylvan

Cecelia has a mix of physical and magic moves:

**Physical Moves (Free):**
- Twin Slash - Quick two-hit combo
- Severing Cut - Damage with bleed
- Swordbind - Damage with attack debuff

**Magic Moves (Cost Mana):**
- Blink Strike (4 mana) - Teleport strike
- Flash Step (2 mana) - Teleport mobility
- Hero's Intercept (4 mana) - Protect ally
- Rally Heart (4 mana) - AoE buff
- Piercing Lunge (4 mana) - Armor-piercing
- Blade Whirl (4 mana) - AoE attack
- Afterimage (4 mana) - Evasion buff

## Technical Details

### Loading System
- Movesets are loaded from JSON files using `MoveSetLoader.LoadMoveSetFromFile(characterName)`
- If a file is not found, a default moveset is created automatically
- Files are loaded from `Application.streamingAssetsPath`

### Backward Compatibility
- Characters without config files get a default moveset with basic physical and magic attacks
- The old `MoveSetFactory` is still available but not used by default

## Notes

- Physical attacks NEVER cost mana, regardless of how powerful they are
- Magic attacks ALWAYS cost mana
- Mana regenerates at the start of each character's turn
- The UI shows "Physical" for physical moves and "Mana: X" for magic moves
