# UI Mockup - Config File System with Physical/Magic Distinction

## Battle Screen with New Labels

```
┌─────────────────────────────────────────────────────────────────┐
│                    Turn: Cecelia Sylvan                         │
│                    Next: Hero 2                                 │
│                                                                 │
├──────────────────────┐           ┌─────────────────────────────┤
│  Player Squad        │           │      Enemy Squad            │
│  ┌────────────────┐  │           │  ┌────────────────────┐    │
│  │ Cecelia Sylvan │  │           │  │ Villain 1          │    │
│  │ HP: 100/100    │  │           │  │ HP: 70/70          │    │
│  │ ████████████   │  │           │  │ ████████████       │    │
│  │ Mana: 12/12    │  │← NEW!     │  └────────────────────┘    │
│  └────────────────┘  │           │                             │
│                      │           │  ┌────────────────────┐    │
│  ┌────────────────┐  │           │  │ Villain 2          │    │
│  │ Hero 2         │  │           │  │ HP: 90/90          │    │
│  │ HP: 80/80      │  │           │  │ ████████████       │    │
│  │ ████████████   │  │           │  └────────────────────┘    │
│  │ Mana: 10/10    │  │← NEW!     │                             │
│  └────────────────┘  │           │                             │
└──────────────────────┘           └─────────────────────────────┘
│                                                                 │
│                  Cecelia Sylvan's turn!                         │
│                                                                 │
│  ┌───────────────── SELECT MOVE ────────────────────────────┐  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Twin Slash                    Physical    │ ← FREE!  │ │
│  │ │ Deal physical damage with two-hit combo             │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Severing Cut                  Physical    │ ← FREE!  │ │
│  │ │ Deal damage and apply Bleed                         │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Swordbind                     Physical    │ ← FREE!  │ │
│  │ │ Deal light damage and reduce attack                 │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Flash Step                    Mana: 2     │ ← Costs  │ │
│  │ │ Teleport to empty tile. Gain Evasion               │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Blink Strike                  Mana: 4     │ ← Costs  │ │
│  │ │ Teleport to target and strike. Ignores Guard       │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Hero's Intercept              Mana: 4     │ ← Costs  │ │
│  │ │ Teleport to ally and take next hit                 │ │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ... (4 more magic moves) ...                             │  │
│  │                                                           │  │
│  └───────────────────────────────────────────────────────────┘  │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Key UI Changes

### Before (Old System)
```
┌─────────────────────────────────────────────────────┐
│ Twin Slash                           Cost: 0        │
│ Deal physical damage with two-hit combo            │
└─────────────────────────────────────────────────────┘
```

### After (New System)
```
┌─────────────────────────────────────────────────────┐
│ Twin Slash                           Physical       │ ← Clear label
│ Deal physical damage with two-hit combo            │
└─────────────────────────────────────────────────────┘
```

## Move Type Comparison

### Physical Moves (Always Available)
```
┌─────────────────────────────────────────────────────┐
│ Twin Slash                           Physical       │
│ Deal physical damage with two-hit combo            │
└─────────────────────────────────────────────────────┘
Background: Normal gray (#4D4D4D)
Label: "Physical" in cyan
Always clickable (never grayed out)
```

### Magic Moves (When Affordable)
```
┌─────────────────────────────────────────────────────┐
│ Blink Strike                         Mana: 4        │
│ Teleport to target and strike. Ignores Guard      │
└─────────────────────────────────────────────────────┘
Background: Normal gray (#4D4D4D)
Label: "Mana: 4" in cyan
Clickable if you have enough mana
```

### Magic Moves (When NOT Affordable)
```
┌─────────────────────────────────────────────────────┐
│ Rally Heart                          Mana: 4        │
│ Inspire allies, increasing their Attack            │
└─────────────────────────────────────────────────────┘
Background: Darker gray (#333333)
Label: "Mana: 4" in gray (#808080)
Not clickable (grayed out)
```

## Resource Display Changes

### Before
```
┌────────────────┐
│ Cecelia Sylvan │
│ HP: 100/100    │
│ ████████████   │
│ Focus: 6/6     │ ← Generic resource name
└────────────────┘
```

### After
```
┌────────────────┐
│ Cecelia Sylvan │
│ HP: 100/100    │
│ ████████████   │
│ Mana: 12/12    │ ← Always "Mana"
└────────────────┘
```

## Benefits of New System

### 1. Clear Visual Distinction
- Physical attacks are INSTANTLY recognizable (shows "Physical")
- Magic attacks show exact mana cost (shows "Mana: X")
- No confusion about what costs resources

### 2. Better Game Balance
- Physical attacks can be used any time
- Players must manage mana for powerful magic abilities
- Strategic decision-making: save mana or use it now?

### 3. Intuitive for Players
- "Physical" = Free = Can always use
- "Mana: X" = Costs = Must have mana
- Simple and clear

## Config File Example

This is loaded from `Cecelia Sylvan.json`:

```json
{
  "moves": [
    {
      "name": "Twin Slash",
      "type": "physical",     ← Shows as "Physical"
      "manaCost": 0,
      "damage": 3
    },
    {
      "name": "Blink Strike",
      "type": "magic",        ← Shows as "Mana: 4"
      "manaCost": 4,
      "damage": 5
    }
  ]
}
```

## Usage Flow

1. **Turn Starts**
   - Mana regenerates (+2 for Cecelia)
   - Display updates: "Mana: 12/12"

2. **Move Selection Panel Opens**
   - Physical moves show "Physical" label
   - Magic moves show "Mana: X" label
   - All physical moves are available
   - Magic moves are grayed out if not affordable

3. **Player Clicks Physical Move**
   - No mana check needed
   - Move executes immediately
   - Mana stays the same

4. **Player Clicks Magic Move**
   - System checks if enough mana
   - If yes: Deduct mana, execute move
   - If no: Button is already grayed out
   - Display updates: "Mana: 8/12"

5. **Next Turn**
   - Mana regenerates again
   - Cycle repeats

## Comparison Table

| Feature | Old System | New System |
|---------|-----------|------------|
| Resource Name | "Focus" or custom | Always "Mana" |
| Physical Attacks | Cost: 0 | "Physical" label |
| Magic Attacks | Cost: X | "Mana: X" label |
| Configuration | Hard-coded in C# | JSON config files |
| Adding Characters | Edit code | Create JSON file |
| Attack Type | Implicit | Explicit (physical/magic) |
