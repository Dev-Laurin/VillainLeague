# Moveset System UI Mockup

## Battle Screen Layout

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
│  │ Focus: 6/6     │  │           │  └────────────────────┘    │
│  └────────────────┘  │           │                             │
│                      │           │  ┌────────────────────┐    │
│  ┌────────────────┐  │           │  │ Villain 2          │    │
│  │ Hero 2         │  │           │  │ HP: 90/90          │    │
│  │ HP: 80/80      │  │           │  │ ████████████       │    │
│  │ ████████████   │  │           │  └────────────────────┘    │
│  │ Energy: 10/10  │  │           │                             │
│  └────────────────┘  │           │                             │
└──────────────────────┘           └─────────────────────────────┘
│                                                                 │
│                  Cecelia Sylvan's turn!                         │
│                                                                 │
│  ┌───────────────── SELECT MOVE ────────────────────────────┐  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Twin Slash                           Cost: 0        │ │  │
│  │ │ Deal physical damage with quick two-hit combo      │ │  │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Blink Strike                         Cost: 2        │ │  │
│  │ │ Teleport to target and strike. Ignores Guard       │ │  │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Flash Step                           Cost: 1        │ │  │
│  │ │ Teleport to empty tile. Gain Evasion for 1 turn   │ │  │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Hero's Intercept                     Cost: 2        │ │  │
│  │ │ Teleport to ally and take next hit. Gain Armor    │ │  │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ┌─────────────────────────────────────────────────────┐ │  │
│  │ │ Rally Heart                          Cost: 2        │ │  │
│  │ │ Inspire allies, increasing their Attack for 2 turns│ │  │
│  │ └─────────────────────────────────────────────────────┘ │  │
│  │                                                           │  │
│  │ ... (5 more moves) ...                                   │  │
│  │                                                           │  │
│  └───────────────────────────────────────────────────────────┘  │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## Key UI Features

1. **Resource Display**: Shows current/max resource (Focus: 6/6) under each player character's HP
2. **Move Selection Panel**: Displays when it's the player's turn
   - Title: "SELECT MOVE"
   - List of all available moves
   - Each move button shows:
     - Move name (left aligned)
     - Resource cost (right aligned)
     - Description (smaller text below)
   - Moves you can't afford are grayed out and disabled
3. **Character Stats**: HP bars and resource pools visible at all times
4. **Turn Indicator**: Shows whose turn it is and who's next

## Move Button States

### Affordable Move (Can Use)
```
┌─────────────────────────────────────────────────────┐
│ Twin Slash                           Cost: 0        │
│ Deal physical damage with a quick two-hit combo    │
└─────────────────────────────────────────────────────┘
Background: Dark gray (#4D4D4D)
Text: White
Clickable
```

### Unaffordable Move (Cannot Use)
```
┌─────────────────────────────────────────────────────┐
│ Blink Strike                         Cost: 2        │
│ Teleport to a target and strike. Ignores Guard     │
└─────────────────────────────────────────────────────┘
Background: Darker gray (#333333)
Text: Gray (#808080)
Not clickable
```

## Interaction Flow

1. Player's turn starts
2. Resource regenerates (+1 Focus)
3. Move selection panel appears
4. Player clicks on a move
5. If move needs target:
   - Target selection panel appears
   - Player selects target
6. Move executes
7. Resource is spent
8. Message shows move effect
9. Turn ends

## Cecelia Sylvan's Complete Moveset

All 10 moves from the issue are implemented:
1. Twin Slash (Cost: 0)
2. Blink Strike (Cost: 2)
3. Flash Step (Cost: 1)
4. Hero's Intercept (Cost: 2)
5. Rally Heart (Cost: 2)
6. Severing Cut (Cost: 1)
7. Swordbind (Cost: 1)
8. Piercing Lunge (Cost: 2)
9. Blade Whirl (Cost: 2)
10. Afterimage (Cost: 2)
