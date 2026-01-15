# Move Chooser UI - Implementation Guide

## Overview

The Move Chooser UI is an enhanced, medieval fantasy-themed interface for selecting character moves during battle. It features:

- **Scrollable move list** - View all available moves with clear information
- **Separate description panel** - Hover over moves to see detailed information
- **Medieval fantasy theme** - Parchment colors, gold accents, and dark borders
- **Visual affordability feedback** - Grayed out moves you can't afford
- **Smooth interactions** - Hover effects and clear visual feedback

## Features

### 1. Move List Panel (Left Side)
- Title: "⚔ Choose Your Move ⚔"
- Scrollable list of all available moves
- Each move button shows:
  - Move name (in gold)
  - Cost (resource type and amount)
  - Button styling changes based on affordability

### 2. Description Panel (Right Side)
- Title: "📜 Move Details 📜"
- Displays when hovering over a move:
  - **Move Name** - Large, gold text
  - **Description** - Flavor text explaining the move
  - **Cost** - Detailed resource cost breakdown
  - **Effects** - Complete list of all effects:
    - Damage and hits
    - Healing
    - Buffs and debuffs
    - Special effects (ignore guard, armor pierce, etc.)
    - Target type
    - Duration

### 3. Medieval Fantasy Theme
- **Parchment Color** - RGB(0.95, 0.90, 0.75) - Main text
- **Dark Border** - RGB(0.2, 0.15, 0.1) - Panel borders
- **Gold Accent** - RGB(0.85, 0.70, 0.25) - Titles and move names
- **Affordable Moves** - RGB(0.25, 0.20, 0.15, 0.9) - Dark brown
- **Unaffordable Moves** - RGB(0.15, 0.12, 0.10, 0.6) - Darker, faded
- **Hover Effect** - RGB(0.45, 0.35, 0.25, 0.95) - Lighter brown highlight

## Setup Instructions

### Automatic Setup (Recommended)

1. Open your Battle scene in Unity
2. Find your Canvas in the Hierarchy
3. Add the `MoveChooserUISetup` component to the Canvas
4. Assign the Canvas reference
5. Assign the BattleUI reference
6. In the Inspector, click "Setup Move Chooser UI" (custom button)
7. The entire UI structure will be created automatically

### Manual Setup

If you need to set it up manually:

1. Create a main panel covering the full canvas
2. Create left panel (50% width) with scroll view
3. Create right panel (38% width) for descriptions
4. Add `MoveChooserUI` component to main panel
5. Wire up all references:
   - moveChooserPanel
   - moveListContainer
   - descriptionPanel
   - moveNameText
   - moveDescriptionText
   - moveCostText
   - moveEffectsText
   - scrollRect
   - moveButtonParent

### Integration with BattleUI

The `BattleUI` class has been updated to support the new Move Chooser UI:

```csharp
[Header("Enhanced Move Chooser")]
public MoveChooserUI moveChooserUI;
```

If `moveChooserUI` is assigned, the enhanced UI will be used automatically. Otherwise, it falls back to the original system.

## Usage

### For Game Designers

Once set up, the Move Chooser UI works automatically during player turns:

1. On player character's turn, the UI popup appears
2. Player can scroll through available moves
3. Hover over any move to see full details
4. Click a move to select it
5. UI hides after selection

### For Programmers

#### Showing the Move Chooser

```csharp
moveChooserUI.ShowMoveChooser(
    moves,                    // List of moves
    resource,                 // Primary resource (Mana, Focus, etc.)
    secondaryResource,        // Secondary resource (Style, Resolve, etc.)
    showOnlySupers,           // Filter to show only super moves
    (Move selectedMove) => {
        // Handle move selection
        ExecuteMove(selectedMove);
    }
);
```

#### Hiding the Move Chooser

```csharp
moveChooserUI.HideMoveChooser();
```

## Customization

### Colors

You can adjust the medieval theme colors in the `MoveChooserUI` component:

- `parchmentColor` - Main text color
- `darkBorderColor` - Border and pressed button color
- `goldAccentColor` - Titles and move names
- `affordableColor` - Background for moves you can use
- `unaffordableColor` - Background for moves you can't afford
- `hoverColor` - Highlight color on mouse hover

### Layout

Adjust panel sizes in code:

**Move List Panel:**
```csharp
rectTransform.anchorMin = new Vector2(0.05f, 0.1f);
rectTransform.anchorMax = new Vector2(0.55f, 0.9f);
```

**Description Panel:**
```csharp
rectTransform.anchorMin = new Vector2(0.57f, 0.1f);
rectTransform.anchorMax = new Vector2(0.95f, 0.9f);
```

### Move Button Styling

Each move button is 650x70 pixels and includes:
- Move name (left, 65% width, gold)
- Cost (right, 35% width, parchment)
- Hover detection via EventTrigger

## Technical Details

### Architecture

The Move Chooser UI consists of three main scripts:

1. **MoveChooserUI.cs** - Core UI logic and interaction handling
2. **MoveChooserUISetup.cs** - Helper for setting up the UI structure
3. **MoveChooserUITest.cs** - Automated tests for UI functionality

### Integration Points

The system integrates with:
- `BattleManager.cs` - Calls move selection via BattleUI
- `BattleUI.cs` - Routes to either enhanced or legacy UI
- `Character.cs` - Provides moves and resources
- `Move.cs` - Move data structure with all properties

### Move Effects Display

The UI shows all possible move properties:
- Basic: damage, hits, healing
- Buffs: attackBuff, defenseBuff, armor, evasion
- Debuffs: attackDebuff, defenseDebuff
- Special: ignoreGuard, armorPierce, bleed, counterDamage, styleGain
- Meta: durationTurns, targetType, radius

## Testing

### Automated Tests

Run the automated tests:

1. Add `MoveChooserUITest` component to a GameObject
2. Assign the `moveChooserUI` reference
3. Check "Run Tests On Start"
4. Enter Play Mode
5. Check console for test results

Tests cover:
- Initialization
- Move display
- Move filtering (normal vs super)
- Affordability checking
- Selection callbacks

### Manual Testing

1. Enter Play Mode
2. Wait for battle to start
3. On player character turn, verify:
   - Move list appears with scroll
   - Description panel starts empty
   - Hovering shows move details
   - Clicking a move selects it
   - UI hides after selection
   - Colors match medieval theme

## Troubleshooting

### UI doesn't appear

- Check that `moveChooserUI` is assigned in BattleUI
- Verify the main panel is a child of Canvas
- Ensure the panel starts inactive (script handles showing)

### Hover not working

- Verify EventTrigger component is on move buttons
- Check that pointer events are not blocked by another UI element
- Ensure Canvas has GraphicRaycaster component

### Moves not showing

- Verify character has a moveset assigned
- Check that moves list is not empty
- Look for console errors during button creation

### Description not updating

- Check that description panel references are assigned
- Verify TextMeshProUGUI components exist
- Ensure description panel is active when hovering

### Scroll view not working

- Verify ScrollRect component is assigned
- Check that Content has ContentSizeFitter
- Ensure Viewport has Mask component

## Future Enhancements

Possible improvements for future versions:

1. **Animations**
   - Slide in/out transitions
   - Move button pulse effects
   - Selection animation

2. **Sound Effects**
   - Hover sound
   - Click sound
   - Panel open/close sound

3. **Enhanced Visuals**
   - Border images for medieval look
   - Background texture (parchment/wood)
   - Icon images for move types

4. **Additional Features**
   - Keyboard navigation
   - Move comparison (side-by-side)
   - Recently used moves highlight
   - Favorite moves system

## Credits

- Implemented for "Move choosing ui" issue
- Medieval fantasy theme for immersive gameplay
- Designed for accessibility and usability
