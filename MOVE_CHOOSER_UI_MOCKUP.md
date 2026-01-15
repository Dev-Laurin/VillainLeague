# Move Chooser UI - Visual Mockup

## Overview

This document provides a visual description of the Medieval Fantasy-themed Move Chooser UI.

## Layout Structure

```
┌──────────────────────────────────────────────────────────────────────────┐
│                         [Semi-transparent Dark Overlay]                   │
│                                                                            │
│  ┌─────────────────────────────────┐  ┌──────────────────────────────┐  │
│  │   ⚔ Choose Your Move ⚔         │  │   📜 Move Details 📜         │  │
│  │  [Gold text on dark panel]      │  │  [Gold text on dark panel]   │  │
│  ├─────────────────────────────────┤  ├──────────────────────────────┤  │
│  │                                 │  │                              │  │
│  │  ┌─────────────────────────┐   │  │  [Move Name]                 │  │
│  │  │ Measured Strike         │   │  │  [Large gold text]           │  │
│  │  │ Physical        [Brown] │   │  │                              │  │
│  │  └─────────────────────────┘   │  │  Deal physical damage with   │  │
│  │                                 │  │  disciplined form.           │  │
│  │  ┌─────────────────────────┐   │  │  [Parchment text]            │  │
│  │  │ Guard Stance            │   │  │                              │  │
│  │  │ 1 Resolve       [Brown] │   │  │  Cost: 1 Resolve             │  │
│  │  └─────────────────────────┘   │  │  [Gold/parchment text]       │  │
│  │                                 │  │                              │  │
│  │  ┌─────────────────────────┐   │  │  ⚔ Damage: 4                │  │
│  │  │ Vein Vision      [Hover]│◄──┼──┤  🛡 Armor: +3               │  │
│  │  │ 1 Resolve  [Lt. Brown]  │   │  │  ⏱ Duration: 1 turns        │  │
│  │  └─────────────────────────┘   │  │  Target: Self                │  │
│  │                                 │  │  [Parchment text, icons]     │  │
│  │  ┌─────────────────────────┐   │  │                              │  │
│  │  │ Bonebreaker             │   │  │                              │  │
│  │  │ 2 Resolve       [Brown] │   │  │                              │  │
│  │  └─────────────────────────┘   │  │                              │  │
│  │                                 │  │                              │  │
│  │  ┌─────────────────────────┐   │  │                              │  │
│  │  │ Rapid Mend              │   │  │                              │  │
│  │  │ 2 Resolve       [Brown] │   │  │                              │  │
│  │  └─────────────────────────┘   │  │                              │  │
│  │                                 │  │                              │  │
│  │  ┌─────────────────────────┐   │  │                              │  │
│  │  │ Oathbound Chorus        │   │  │                              │  │
│  │  │ 5 Resolve + ⭐ 6 [Gray] │   │  │                              │  │
│  │  └─────────────────────────┘   │  │                              │  │
│  │  [Can't afford - grayed out]   │  │                              │  │
│  │                                 │  │                              │  │
│  │  [Scroll more moves below...]  │  │                              │  │
│  │                                 │  │                              │  │
│  └─────────────────────────────────┘  └──────────────────────────────┘  │
│   [50% width, left side]              [38% width, right side]          │
│                                                                            │
└──────────────────────────────────────────────────────────────────────────┘
```

## Color Palette - Medieval Fantasy Theme

### Primary Colors
- **Parchment** - RGB(242, 230, 191) / #F2E6BF
  - Usage: Main text, descriptions
  - Warm, aged paper color

- **Dark Brown** - RGB(51, 38, 26) / #33261A
  - Usage: Panel borders, backgrounds
  - Rich, dark wood/leather color

- **Gold** - RGB(217, 179, 64) / #D9B340
  - Usage: Titles, move names, highlights
  - Medieval gold accent

### Button States
- **Affordable** - RGB(64, 51, 38, 230) / #403326E6
  - Brown background, clearly visible
  
- **Unaffordable** - RGB(38, 31, 26, 153) / #261F1A99
  - Darker, more transparent, grayed out
  
- **Hover** - RGB(115, 89, 64, 242) / #7359402
  - Lighter brown, prominent highlight

## Component Details

### Move List Panel (Left)

**Title**
```
⚔ Choose Your Move ⚔
Size: 28pt, Bold
Color: Gold (#D9B340)
Alignment: Center
```

**Scroll View**
- Vertical scrolling enabled
- Horizontal scrolling disabled
- Dark transparent viewport
- 10px spacing between buttons

**Move Buttons**
```
┌─────────────────────────────────────┐
│ [Move Name]        [Cost/Physical]  │
│ [Gold, 22pt]       [Parchment, 18pt]│
└─────────────────────────────────────┘
Size: 650x70 pixels
Padding: 15px left/right
```

**Button Hover Effect**
- Background changes from dark brown to lighter brown
- Smooth color transition
- Mouse cursor changes to pointer
- Description panel updates immediately

### Description Panel (Right)

**Title**
```
📜 Move Details 📜
Size: 28pt, Bold
Color: Gold (#D9B340)
Alignment: Center
```

**Content Layout**
```
┌──────────────────────────────────┐
│ [Move Name - 26pt Gold, Center]  │
│ -------------------------------- │
│                                  │
│ [Description - 18pt Parchment]   │
│ Multiple lines, word wrap        │
│                                  │
│ [Cost - 18pt Bold]               │
│ Cost: X Resource                 │
│                                  │
│ [Effects - 16pt Parchment]       │
│ ⚔ Damage: X                      │
│ ❤ Healing: X                     │
│ ↑ Attack Buff: +X                │
│ 🛡 Armor: +X                     │
│ ⏱ Duration: X turns              │
│ Target: [Type]                   │
└──────────────────────────────────┘
```

**Initial State**
- Text reads: "Hover over a move to see details"
- Centered, parchment color
- No specific move selected

### Icons Used

The UI uses Unicode/Emoji symbols for visual clarity:
- ⚔ - Swords (title, damage)
- 📜 - Scroll (title)
- ❤ - Heart (healing)
- 🛡 - Shield (armor, defense)
- ↑ - Up arrow (buffs)
- ↓ - Down arrow (debuffs)
- ⚡ - Lightning (special effects)
- 🗡 - Dagger (armor pierce)
- 🩸 - Blood (bleed)
- ↩ - Return (counter damage)
- ⭐ - Star (secondary resource cost/gain)
- ⏱ - Clock (duration)
- ↝ - Curved arrow (evasion)

## Interaction Flow

### 1. Player Turn Starts
```
[Battle Scene] → [Move Chooser appears]
   - Fade in/instant display
   - Overlay covers main battle view
   - Mouse cursor enabled
```

### 2. Viewing Moves
```
[Player hovers over move]
   ↓
[Button highlights (lighter brown)]
   ↓
[Description panel updates with details]
   - Move name
   - Full description
   - Complete cost breakdown
   - All effects listed
```

### 3. Selecting Move
```
[Player clicks affordable move]
   ↓
[Button press animation (darkest brown)]
   ↓
[Move selected callback fires]
   ↓
[UI hides/fades out]
   ↓
[Move execution begins]
```

### 4. Attempting Unaffordable Move
```
[Player clicks grayed out move]
   ↓
[No action - button is disabled]
   - Visual feedback: button doesn't respond
   - Cursor may show "not allowed" symbol
```

## Responsive Design

### Panel Sizes
- **Move List**: 5% from left, 5% from top/bottom, 50% width
- **Description**: 57% from left, 5% from top/bottom, 38% width
- **Spacing**: 2% gap between panels

### Adaptability
- Panels maintain aspect ratio
- Text wraps appropriately
- Scroll view adjusts to content length
- Works with various move counts (3-15+ moves)

## Accessibility Features

### Visual Clarity
- High contrast between text and backgrounds
- Clear affordability indication (color + opacity)
- Large, readable fonts (minimum 16pt)
- Icons supplement text descriptions

### Interaction Feedback
- Hover state clearly visible
- Click state distinct from hover
- Disabled buttons obviously different
- Description updates immediately on hover

### Information Hierarchy
1. **Primary**: Move names (largest, gold)
2. **Secondary**: Cost (medium, parchment)
3. **Tertiary**: Description/effects (smaller, parchment)

## Medieval Fantasy Aesthetic

### Visual Theme Elements
- **Parchment**: Aged paper color for text
- **Dark Wood/Leather**: Brown panels like medieval furniture
- **Gold Leaf**: Accent color like illuminated manuscripts
- **Scrollwork**: Scroll emoji for "Details" panel
- **Weapons**: Sword emojis for battle theme

### Atmosphere
- Warm color palette (browns, golds, creams)
- Dark but readable backgrounds
- Clear hierarchies like medieval documents
- Functional design with thematic colors

## Technical Specifications

### Unity Components Used
- **Canvas**: Full-screen overlay, Screen Space - Overlay mode
- **Image**: Panel backgrounds
- **TextMeshProUGUI**: All text rendering
- **Button**: Interactive move selection
- **ScrollRect**: Scrollable move list
- **EventTrigger**: Hover detection
- **Mask**: Viewport clipping
- **VerticalLayoutGroup**: Auto-layout move buttons
- **ContentSizeFitter**: Dynamic content sizing

### Performance Considerations
- Dynamic button creation (on-demand)
- Proper cleanup (Destroy old buttons)
- Efficient hover detection (EventTrigger)
- Minimal redraw (only update description on hover)

## Example Move Display Scenarios

### Scenario 1: Physical Attack (Free)
```
┌─────────────────────────────┐
│ Measured Strike             │
│ Physical           [Brown]  │
└─────────────────────────────┘
```

### Scenario 2: Standard Magic
```
┌─────────────────────────────┐
│ Guard Stance                │
│ 1 Resolve          [Brown]  │
└─────────────────────────────┘
```

### Scenario 3: Ultimate Move
```
┌─────────────────────────────┐
│ Oathbound Chorus            │
│ 5 Resolve + ⭐ 6    [Brown] │
└─────────────────────────────┘
```

### Scenario 4: Unaffordable Move
```
┌─────────────────────────────┐
│ Judgment Strike             │
│ 3 Resolve       [Dark Gray] │
└─────────────────────────────┘
```

## Summary

The Move Chooser UI provides a rich, thematic interface for move selection that:
- Clearly displays all available moves
- Provides detailed information on hover
- Uses medieval fantasy colors and styling
- Gives immediate visual feedback on affordability
- Maintains good usability and accessibility
- Integrates seamlessly with the existing battle system
