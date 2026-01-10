# Super Abilities - Visual Reference

## UI Layout

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        Turn: Naice Ajimi                                 │
│                        Next: Villain 1                                   │
└─────────────────────────────────────────────────────────────────────────┘

┌──────────────────────────┐                  ┌──────────────────────────┐
│  PLAYER SQUAD            │                  │  ENEMY SQUAD             │
│                          │                  │                          │
│  Bellinor Chabbeneoux    │                  │  Villain 1               │
│  HP: ██████████ 120/120  │                  │  HP: ██████ 70/70        │
│  Resolve: 6/6            │                  │                          │
│  Resolve: 6/6 ⭐ READY!  │                  │  Villain 2               │
│                          │                  │  HP: █████████ 90/90     │
│  Naice Ajimi             │                  │                          │
│  HP: ████████ 80/80      │                  │                          │
│  Mana: 10/10             │                  │                          │
│  Style: 6/6 ⭐ READY!    │                  │                          │
└──────────────────────────┘                  └──────────────────────────┘

                      ┌─────────────────────────┐
                      │  Choose your action...  │
                      └─────────────────────────┘

    ┌─────────────┐  ┌─────────────┐  ┌─────────────┐
    │ NORMAL TURN │  │  USE SUPER  │  │ TEAM SUPER  │
    └─────────────┘  └─────────────┘  └─────────────┘
```

## Flow Diagram

### Scenario 1: One Hero Charged

```
Player Turn (Naice - Style: 6/6)
        ↓
   ┌────────────────┐
   │ Select Moves   │
   └────────────────┘
        ↓
   [Regular Moves]  [Super Moves]
        ↓                ↓
   Normal Attack    Final Act
   Feinting Strike  (Ultimate)
   Now You See Me
   ...
```

### Scenario 2: Both Heroes Charged

```
Player Turn (Either Hero - Both at 6/6)
        ↓
   ┌──────────────────┐
   │  Choose Action   │
   └──────────────────┘
        ↓
   ┌────┴────┬────────┬────────┐
   ↓         ↓        ↓        
NORMAL    USE      TEAM
 TURN    SUPER    SUPER
   ↓         ↓        ↓
Select    Super    Combo
Normal    Moves    Attack!
Moves     Only     (15 dmg)
          ↓
       Ultimate
       (Costs 6)
```

## Charging Process

### Naice's Style Bar
```
Start of Battle: Style: 0/6 [______]
Use Feinting Strike: +1 [█_____]
Use Smoke & Mirrors: +1 [██____]
Use Cutting Remark: +1 [███___]
Use False Opening: +2 [█████_]
Use Take a Bow: +2 [██████] ⭐ READY!
```

### Bellinor's Resolve Bar
```
Start of Battle: Resolve: 0/6 [______]
Use Guard Stance: +1 [█_____]
Use Intercept: +2 [███___]
Use Guard Stance: +1 [████__]
Use Rapid Mend: +1 [█████_]
Use Intercept: +2 [██████] ⭐ READY!
```

## Battle Example

### Turn-by-Turn
```
Turn 1: Bellinor
  → Uses Guard Stance (+1 Resolve)
  → Resolve: 1/6

Turn 2: Naice
  → Uses Feinting Strike (+1 Style)
  → Style: 1/6

Turn 3: Villain 1
  → Attacks Bellinor (12 damage)

Turn 4: Villain 2
  → Attacks Naice (10 damage)

Turn 5: Bellinor
  → Uses Intercept on Naice (+2 Resolve)
  → Resolve: 3/6

Turn 6: Naice
  → Uses Take a Bow (+2 Style)
  → Style: 3/6

...several turns later...

Turn 15: Bellinor
  → Uses Rapid Mend (+1 Resolve)
  → Resolve: 6/6 ⭐ SUPER READY!

Turn 16: Naice
  → Uses False Opening (+2 Style)
  → Style: 6/6 ⭐ SUPER READY!

Turn 17: Bellinor
  ┌──────────────────────────────────┐
  │ Both heroes have supers ready!   │
  │ What will you do?                │
  └──────────────────────────────────┘
  → Player selects TEAM SUPER
  → Both heroes attack together!
  → 15 damage to Villain 1
  → 15 damage to Villain 2
  → Both Resolve & Style drop to 0/6

Victory Screen: "You won!"
```

## UI States

### State 1: No Supers Ready
```
┌─────────────┐  ┌─────────────┐  ┌─────────────┐
│   ATTACK    │  │   DEFEND    │  │   SPECIAL   │
└─────────────┘  └─────────────┘  └─────────────┘
```

### State 2: One Super Ready
```
┌─────────────┐              ┌─────────────┐
│ NORMAL TURN │              │  USE SUPER  │
└─────────────┘              └─────────────┘
       ↓                            ↓
  [Move List]                [Super Moves Only]
```

### State 3: Both Supers Ready
```
┌─────────────┐  ┌─────────────┐  ┌─────────────┐
│ NORMAL TURN │  │  USE SUPER  │  │ TEAM SUPER  │
└─────────────┘  └─────────────┘  └─────────────┘
       ↓                 ↓                 ↓
  [Regular         [This Hero's      [Combined
   Moves]           Ultimate]         Attack]
```

## Resource Costs

### Individual Super
```
Before: Naice
  Mana: 10/10
  Style: 6/6

Uses "Final Act" (costs 6 Mana + 6 Style)

After: Naice
  Mana: 4/10
  Style: 0/6
```

### Team Super
```
Before:
  Bellinor - Resolve(Primary): 6/6, Resolve(Secondary): 6/6
  Naice - Mana: 10/10, Style: 6/6

Uses "Team Super Attack"

After:
  Bellinor - Resolve(Primary): 6/6, Resolve(Secondary): 0/6
  Naice - Mana: 10/10, Style: 0/6
  (Only secondary resources are spent!)
```

## Key Visual Indicators

- ⭐ = Super Ready
- [█████_] = Resource bar (5/6 filled)
- 🔥 = Team super available
- ⚡ = Individual super available

## Color Coding

- **Green**: Player HP bars
- **Red**: Enemy HP bars
- **Light Blue**: Primary resources (Mana/Resolve)
- **Gold/Yellow**: Secondary resources (Style/Resolve)
- **White**: Available buttons
- **Gray**: Unavailable buttons
