# Battle Scene Visual Mockup

```
╔═══════════════════════════════════════════════════════════════════════════════╗
║                                                                               ║
║                    Turn: Hero 1          Next: Hero 2                         ║
║                                                                               ║
╠═══════════════════════════════════════════════════════════════════════════════╣
║                                                                               ║
║  ┌─────────────────────┐                           ┌─────────────────────┐  ║
║  │  YOUR TEAM (GREEN)  │                           │  ENEMIES (RED)      │  ║
║  │                     │                           │                     │  ║
║  │  Hero 1             │                           │          Villain 1  │  ║
║  │  100/100            │                           │              70/70  │  ║
║  │  ████████████████   │                           │  ████████████████   │  ║
║  │                     │                           │                     │  ║
║  │  Hero 2             │        Battle             │          Villain 2  │  ║
║  │  80/80              │        Message:           │              90/90  │  ║
║  │  ████████████████   │     "Battle Start!"       │  ████████████████   │  ║
║  │                     │                           │                     │  ║
║  └─────────────────────┘                           └─────────────────────┘  ║
║                                                                               ║
╠═══════════════════════════════════════════════════════════════════════════════╣
║                                                                               ║
║                     ┌─────────┐ ┌─────────┐ ┌─────────┐                     ║
║                     │ ATTACK  │ │ DEFEND  │ │ SPECIAL │                     ║
║                     └─────────┘ └─────────┘ └─────────┘                     ║
║                                                                               ║
╚═══════════════════════════════════════════════════════════════════════════════╝

When selecting a target, a panel appears:

╔══════════════════════════════════╗
║       SELECT TARGET              ║
║                                  ║
║    ┌──────────────────────┐     ║
║    │    Villain 1         │     ║
║    └──────────────────────┘     ║
║                                  ║
║    ┌──────────────────────┐     ║
║    │    Villain 2         │     ║
║    └──────────────────────┘     ║
║                                  ║
╚══════════════════════════════════╝
```

## Color Scheme

```
┌─────────────────────────────────────┐
│ Player Team - Green Theme           │
│ ■ Names: Bright Green (#33CC33)    │
│ ■ HP Text: White                    │
│ ■ HP Bars: Green (#33CC33)          │
│ ■ Background: Dark Gray (semi-α)    │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Enemy Team - Red Theme              │
│ ■ Names: Bright Red (#E63333)      │
│ ■ HP Text: White                    │
│ ■ HP Bars: Red (#E63333)            │
│ ■ Background: Dark Gray (semi-α)    │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Turn Order - Top Display            │
│ ■ Current Turn: White, Large (36px) │
│ ■ Next Turn: Light Gray (24px)      │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Action Buttons - Bottom             │
│ ■ Background: Medium Gray (#4D4D4D) │
│ ■ Text: White (24px)                │
│ ■ Hover: Lighter Gray               │
│ ■ Pressed: Darker Gray              │
└─────────────────────────────────────┘
```

## Layout Dimensions

```
Screen Reference Resolution: 1920 x 1080

Turn Display:
  Position: Center Top (Y: 400)
  Size: 600 x 60

Next Turn Display:
  Position: Center (Y: 350)
  Size: 600 x 40

Player Panel:
  Position: Left (-600, 250)
  Size: 400 x 300

Enemy Panel:
  Position: Right (600, 250)
  Size: 400 x 300

Message Display:
  Position: Center (0, 0)
  Size: 800 x 60

Action Buttons:
  Position: Bottom Center (Y: -380)
  Size: 240 x 80 each
  Spacing: 280 pixels apart

Target Selection Panel:
  Position: Center (0, 0)
  Size: 500 x 200
```

## Battle Flow Visualization

```
START
  │
  ├─► UI Created Automatically
  │
  ├─► Battle Initialized
  │     • 2 Heroes (Player Squad)
  │     • 2 Villains (Enemy Squad)
  │     • Turn order established
  │
  └─► BATTLE LOOP ────────────────────┐
                                      │
      ┌─────────────────────────────┘
      │
      ├─► Current Turn: Hero/Villain?
      │
      ├─► IF HERO: ──────────────────┐
      │   │                           │
      │   ├─► Show Action Buttons     │
      │   │                           │
      │   ├─► Player Clicks:          │
      │   │   • ATTACK → Select Target│
      │   │   • DEFEND → End Turn     │
      │   │   • SPECIAL → Select Target
      │   │                           │
      │   └─► Execute Action          │
      │                               │
      ├─► IF VILLAIN: ────────────────┤
      │   │                           │
      │   ├─► AI Selects Target       │
      │   │                           │
      │   └─► Execute Attack          │
      │                               │
      ├─► Update HP Bars              │
      │                               │
      ├─► Show Damage Message         │
      │                               │
      ├─► Check Win/Lose              │
      │   │                           │
      │   ├─► All Villains Dead? → VICTORY
      │   │                           │
      │   └─► All Heroes Dead? → DEFEAT
      │                               │
      └─► Next Turn ──────────────────┘
            (Loop back up)

VICTORY or DEFEAT → END
```

## Gameplay Example

```
Turn 1: Hero 1
┌────────────────────────────────┐
│ Turn: Hero 1    Next: Hero 2   │
│                                │
│ [Click ATTACK]                 │
│ Select Target: Villain 1       │
│                                │
│ Result: Villain 1 takes 11 dmg│
│ Villain 1: 70 → 59 HP          │
└────────────────────────────────┘

Turn 2: Hero 2
┌────────────────────────────────┐
│ Turn: Hero 2    Next: Villain 1│
│                                │
│ [Click SPECIAL]                │
│ Select Target: Villain 1       │
│                                │
│ Result: Villain 1 takes 36 dmg│
│ Villain 1: 59 → 23 HP          │
└────────────────────────────────┘

Turn 3: Villain 1
┌────────────────────────────────┐
│ Turn: Villain 1  Next: Villain 2│
│                                │
│ Villain 1 attacks Hero 1       │
│                                │
│ Result: Hero 1 takes 7 damage  │
│ Hero 1: 100 → 93 HP            │
└────────────────────────────────┘

Turn 4: Villain 2
┌────────────────────────────────┐
│ Turn: Villain 2  Next: Hero 1  │
│                                │
│ Villain 2 attacks Hero 2       │
│                                │
│ Result: Hero 2 takes 7 damage  │
│ Hero 2: 80 → 73 HP             │
└────────────────────────────────┘

... Battle continues until one side is defeated
```

## UI Features

✓ Auto-generated at runtime
✓ No manual setup required
✓ Responsive layout
✓ Real-time HP updates
✓ Color-coded teams
✓ Clear turn indicators
✓ Modal target selection
✓ Descriptive messages
✓ Skip dead characters
✓ Victory/Defeat detection
