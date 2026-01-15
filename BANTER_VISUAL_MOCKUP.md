# Battle Banter Visual Mockup

## UI Layout

```
┌─────────────────────────────────────────────────────────────┐
│                      BATTLE SCENE                           │
│                                                             │
│  Turn: Bellinor Chabbeneoux          Next: Villain 1      │
│                                                             │
│  ┌─────────────────┐                  ┌─────────────────┐ │
│  │ Bellinor        │                  │ Villain 1       │ │
│  │ HP: 120/120     │                  │ HP: 70/70       │ │
│  │ Resolve: 6/6    │                  └─────────────────┘ │
│  │ Style: 0/6      │                                       │
│  └─────────────────┘                  ┌─────────────────┐ │
│                                        │ Villain 2       │ │
│  ┌─────────────────┐                  │ HP: 90/90       │ │
│  │ Naice Ajimi     │                  └─────────────────┘ │
│  │ HP: 80/80       │                                       │
│  │ Mana: 10/10     │                                       │
│  │ Style: 0/6      │                                       │
│  └─────────────────┘                                       │
│                                                             │
│           Bellinor Chabbeneoux uses Measured Strike!       │
│                                                             │
│  ┌─────────────────────────────────────────────────────┐  │
│  │    [Naice Ajimi] Now THAT'S how it's done!         │  │
│  └─────────────────────────────────────────────────────┘  │
│                 ↑ Banter Dialogue Box ↑                    │
│              (Auto-dismisses after 3 seconds)              │
└─────────────────────────────────────────────────────────────┘
```

## Banter Panel Details

### Visual Characteristics
- **Position**: Bottom center of screen
- **Size**: 700 pixels wide × 120 pixels tall
- **Background Color**: Dark blue-gray (RGB: 38, 38, 51, Alpha: 90%)
- **Border**: Slightly rounded corners (Unity default)
- **Text Color**: Yellow-tinted white (RGB: 255, 255, 204) for better visibility

### Text Format
```
[Character Name] Dialogue text here
```

Examples:
- `[Bellinor Chabbeneoux] Well executed, Naice.`
- `[Naice Ajimi] Ooh, nice one Bell!`
- `[Bellinor Chabbeneoux] Stay focused, Naice.`
- `[Naice Ajimi] Did you even TRY to look cool?`

## Interaction Flow Diagram

```
Player Move Executed
        ↓
   Random Check (25%)
        ↓
   ┌─────────┴─────────┐
   │                   │
  Yes                 No
   │                   │
   ↓                   ↓
Select Context    Continue Battle
(move_comment,         ↓
playful_insult,   Next Action
check_in)
   ↓
Get Partner Character
(Bellinor ←→ Naice)
   ↓
Get Random Banter Line
(From Character's Dictionary)
   ↓
Format: "[Name] Line"
   ↓
┌───────────────────┐
│ Show Banter Panel │ ← Panel fades in
└───────────────────┘
   ↓
Display for 3 seconds
   ↓
┌───────────────────┐
│ Hide Banter Panel │ ← Panel disappears
└───────────────────┘
   ↓
Continue Battle
```

## Timing Diagram

```
Time: 0s          1s          2s          3s          4s
      │           │           │           │           │
      ↓           ↓           ↓           ↓           ↓
      
Move Executes
      ↓
      ┌─────────────────────────────────────────┐
      │      Banter Panel Visible               │
      │  "[Naice Ajimi] Still with me, Bell?"  │
      └─────────────────────────────────────────┘
                                                ↓
                                          Panel Hides
                                                ↓
                                          Next Battle Action
```

## Context Examples in Battle

### Scenario 1: After a Strong Attack
```
Battle Message: "Bellinor Chabbeneoux uses Bonebreaker!"
                "Villain 1 takes 7 damage!"

Banter (25% chance):
┌──────────────────────────────────────────────────────┐
│  [Naice Ajimi] Now THAT'S how it's done!            │
└──────────────────────────────────────────────────────┘
```

### Scenario 2: After a Flashy Move
```
Battle Message: "Naice Ajimi uses Now You Don't!"
                "Villain 2 takes 5 damage!"

Banter (25% chance):
┌──────────────────────────────────────────────────────┐
│  [Bellinor Chabbeneoux] A bit theatrical, don't you │
│  think?                                              │
└──────────────────────────────────────────────────────┘
```

### Scenario 3: Checking In
```
Battle Message: "Naice Ajimi uses Feinting Strike!"

Banter (25% chance):
┌──────────────────────────────────────────────────────┐
│  [Bellinor Chabbeneoux] Are you holding up alright? │
└──────────────────────────────────────────────────────┘
```

### Scenario 4: Friendly Teasing
```
Battle Message: "Bellinor Chabbeneoux uses Guard Stance!"

Banter (25% chance):
┌──────────────────────────────────────────────────────┐
│  [Naice Ajimi] So serious all the time!             │
└──────────────────────────────────────────────────────┘
```

## Color Scheme

### Banter Panel Background
- **RGB**: (38, 38, 51)
- **Hex**: #262633
- **Alpha**: 0.9 (90% opaque)
- **Description**: Dark blue-gray, slightly transparent

### Banter Text
- **RGB**: (255, 255, 204)
- **Hex**: #FFFFCC
- **Alpha**: 1.0 (fully opaque)
- **Description**: Soft yellow-white for easy reading

## Positioning Details

### Screen Coordinates (1920×1080 reference)
- **Anchor**: Center
- **Position**: (0, -350)
- **Size**: (700, 120)

### Relative to Other UI Elements
- **Below**: Main message text (center of screen)
- **Above**: Action buttons (if visible)
- **Clear of**: Character info panels (left and right sides)

## Implementation Notes

### What Makes It Work
1. **Non-Blocking**: Doesn't stop battle flow
2. **Auto-Dismiss**: No player input required
3. **Contextual**: Matches situation
4. **Personality**: Reflects character traits
5. **Frequency**: Not overwhelming (25% chance)

### Player Experience
- Adds personality without interrupting gameplay
- Reinforces friendship between characters
- Provides humor and emotional connection
- Doesn't require any interaction
- Enhances immersion naturally

## Future: Audio Integration

When audio is added (as mentioned in the issue), the system will work like this:

```
Banter Triggered
        ↓
    ┌───────┴───────┐
    │               │
Show Text        Play Audio
    │               │
    └───────┬───────┘
            ↓
    Wait 3 seconds
            ↓
    Hide Text + Stop Audio
```

The infrastructure is already in place:
- Same triggering system
- Same timing mechanism
- Text can stay or be removed
- Audio clips can be added to BattleBanter dictionary
- PlayOneShot or similar for audio playback
