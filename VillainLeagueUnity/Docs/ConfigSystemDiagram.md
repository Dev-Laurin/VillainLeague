# Config File Loading Flow

## Directory Structure

```
VillainLeagueUnity/
└── Assets/
    └── StreamingAssets/
        └── CharacterMovesets/
            ├── Cecelia Sylvan.json
            ├── Hero 2.json
            ├── Villain 1.json
            └── Villain 2.json
```

## Loading Flow

```
┌─────────────────────────────────────────────────────────────┐
│                  BattleManager.SetupBattle()                │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│     MoveSetLoader.LoadMoveSetFromFile("Cecelia Sylvan")    │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│     Read from: StreamingAssets/CharacterMovesets/           │
│     File: Cecelia Sylvan.json                               │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│              Parse JSON to CharacterMoveSetData             │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│            Convert to CharacterMoveSet object               │
│                                                             │
│  For each move:                                             │
│    - If type = "physical" → cost = 0, isPhysical = true    │
│    - If type = "magic" → cost = manaCost, isPhysical = false│
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│              Return CharacterMoveSet to character           │
└─────────────────────────────────────────────────────────────┘
```

## Example JSON Config

### Cecelia Sylvan (Cecelia Sylvan.json)

```json
{
  "characterName": "Cecelia Sylvan",
  "role": "Mobile swordswoman / skirmisher / protector",
  "resource": {
    "name": "Mana",
    "max": 12,
    "regenPerTurn": 2
  },
  "moves": [
    {
      "id": "cece_basic_slash",
      "name": "Twin Slash",
      "description": "Deal physical damage with a quick two-hit sword combo.",
      "type": "physical",    ← Physical = No mana cost
      "manaCost": 0,
      "damage": 3,
      "hits": 2
    },
    {
      "id": "cece_teleport_strike",
      "name": "Blink Strike",
      "description": "Teleport to a target and strike.",
      "type": "magic",       ← Magic = Costs mana
      "manaCost": 4,
      "damage": 5,
      "ignoreGuard": true
    }
  ]
}
```

## Physical vs Magic Moves

### Physical Moves
```
┌─────────────────────────────────┐
│ Twin Slash           Physical   │ ← Always available
│ Quick two-hit combo             │
└─────────────────────────────────┘
```
- Type: "physical"
- Cost: 0 mana (always free)
- Display: "Physical" in UI
- Examples: Sword slashes, punches, kicks

### Magic Moves
```
┌─────────────────────────────────┐
│ Blink Strike        Mana: 4     │ ← Requires mana
│ Teleport and strike             │
└─────────────────────────────────┘
```
- Type: "magic"
- Cost: Specified mana amount
- Display: "Mana: X" in UI
- Examples: Spells, teleports, magical effects

## Mana System

```
Character Stats Panel:
┌──────────────────────┐
│ Cecelia Sylvan       │
│ HP: 100/100          │
│ ██████████████       │
│ Mana: 12/12          │ ← New mana display
└──────────────────────┘
```

- All characters have Mana
- Regenerates each turn (configurable per character)
- Physical moves: Free (0 mana)
- Magic moves: Cost mana

## Adding New Characters

1. Create JSON file with character name:
   ```
   Assets/StreamingAssets/CharacterMovesets/New Hero.json
   ```

2. Define moves with types:
   ```json
   {
     "characterName": "New Hero",
     "resource": {
       "name": "Mana",
       "max": 15,
       "regenPerTurn": 2
     },
     "moves": [
       {
         "name": "Sword Strike",
         "type": "physical",  ← Free
         "damage": 6
       },
       {
         "name": "Fire Blast",
         "type": "magic",     ← Costs mana
         "manaCost": 5,
         "damage": 12
       }
     ]
   }
   ```

3. Load in code:
   ```csharp
   Character newHero = new Character("New Hero", 100, 15, 5, true);
   newHero.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("New Hero"));
   ```

## Benefits

✅ **Easy to Modify**
- Change moves without recompiling code
- Add new characters with just JSON files
- Balance changes are quick

✅ **Clear Distinction**
- Physical vs Magic is explicit
- UI clearly shows move type
- Players understand costs immediately

✅ **Designer-Friendly**
- JSON is human-readable
- No programming required to add characters
- Can be edited with any text editor

✅ **Scalable**
- Add unlimited characters
- No code changes needed
- Version control friendly
