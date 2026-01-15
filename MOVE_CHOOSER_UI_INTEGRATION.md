# Move Chooser UI - Integration Example

## Quick Integration Guide

This guide shows how to integrate the Move Chooser UI into an existing battle scene.

## Step 1: Understand the Architecture

The Move Chooser UI is designed to work seamlessly with the existing battle system:

```
BattleManager.cs (PlayerTurn)
        ↓
    Calls SelectMove()
        ↓
BattleUI.DisplayMoves()
        ↓
    Checks if moveChooserUI exists
        ↓
    Yes → MoveChooserUI.ShowMoveChooser()
    No  → Legacy button-based UI
```

## Step 2: Set Up the UI in Unity Editor

### Automatic Setup (Recommended)

1. **Open your Battle scene**
   - File → Open Scene
   - Select your battle scene

2. **Locate your Canvas**
   - Find the Canvas GameObject in the Hierarchy
   - This should be the parent of your existing BattleUI

3. **Add the Setup Component**
   - Select the Canvas GameObject
   - Add Component → MoveChooserUISetup
   - This component will help create the UI structure

4. **Assign References**
   - Canvas: Drag your Canvas GameObject here
   - BattleUI: Drag your BattleUI GameObject here

5. **Run the Setup**
   ```
   Note: In a real Unity project, you would add a button
   to the Inspector for MoveChooserUISetup that calls
   SetupMoveChooserUI(). For now, you can call it from
   another script or create the UI manually.
   ```

### Manual Setup Alternative

If you prefer manual setup:

1. Create the UI structure as shown in MOVE_CHOOSER_UI_MOCKUP.md
2. Add MoveChooserUI component to the main panel
3. Wire up all references in the Inspector
4. Assign moveChooserUI in BattleUI component

## Step 3: Verify Integration

### Check BattleUI Component

In the Inspector for your BattleUI GameObject, you should see:

```
Battle UI (Script)
├── Player Squad UI
├── Enemy UI
├── Turn Info
├── Action Buttons
├── Super Buttons
├── Move Selection (Legacy)
├── Target Selection
├── Banter Dialogue
└── Enhanced Move Chooser
    └── Move Chooser UI: [Your MoveChooserPanel GameObject]
```

### Verify MoveChooserUI Component

Your MoveChooserPanel GameObject should have:

```
Move Chooser UI (Script)
├── Main Panels
│   ├── Move Chooser Panel: [Self]
│   ├── Move List Container: [MoveListPanel]
│   └── Description Panel: [DescriptionPanel]
├── Description Panel Elements
│   ├── Move Name Text: [MoveName TextMeshProUGUI]
│   ├── Move Description Text: [Description TextMeshProUGUI]
│   ├── Move Cost Text: [Cost TextMeshProUGUI]
│   └── Move Effects Text: [Effects TextMeshProUGUI]
├── Scroll View
│   ├── Scroll Rect: [Scroll View ScrollRect]
│   └── Move Button Parent: [Content Transform]
└── Medieval Theme Colors
    ├── Parchment Color
    ├── Dark Border Color
    ├── Gold Accent Color
    ├── Affordable Color
    ├── Unaffordable Color
    └── Hover Color
```

## Step 4: Test in Play Mode

1. **Enter Play Mode**
   - Click the Play button in Unity Editor
   - Wait for the battle to initialize

2. **Wait for Player Turn**
   - Battle will start automatically
   - Wait for a player character's turn

3. **Observe Move Chooser**
   - The Move Chooser UI should appear
   - Move list should be populated
   - Description panel should say "Hover over a move to see details"

4. **Test Interactions**
   - Hover over different moves
   - Verify description panel updates
   - Check that affordable moves are clickable
   - Verify unaffordable moves are grayed out

5. **Select a Move**
   - Click on an affordable move
   - UI should hide
   - Move should execute normally

## Step 5: Customize (Optional)

### Adjust Colors

Select your MoveChooserPanel GameObject and modify the colors:

```csharp
// In the Inspector, expand "Medieval Theme Colors"
Parchment Color:      RGB(242, 230, 191) - Light tan text
Dark Border Color:    RGB(51, 38, 26)    - Dark brown borders
Gold Accent Color:    RGB(217, 179, 64)  - Gold highlights
Affordable Color:     RGB(64, 51, 38)    - Brown background
Unaffordable Color:   RGB(38, 31, 26)    - Dark gray
Hover Color:          RGB(115, 89, 64)   - Light brown highlight
```

### Adjust Layout

To change panel sizes, modify the code in MoveChooserUISetup.cs:

```csharp
// Move List Panel position (left side)
rectTransform.anchorMin = new Vector2(0.05f, 0.1f);  // 5% from left, 10% from bottom
rectTransform.anchorMax = new Vector2(0.55f, 0.9f);  // 55% from left, 90% from bottom

// Description Panel position (right side)
rectTransform.anchorMin = new Vector2(0.57f, 0.1f);  // 57% from left, 10% from bottom
rectTransform.anchorMax = new Vector2(0.95f, 0.9f);  // 95% from left, 90% from bottom
```

### Adjust Fonts

To change font sizes, modify MoveChooserUI.cs:

```csharp
// In CreateMoveButtonText method
nameText.fontSize = 22;  // Move name size

// In ShowMoveDescription method
moveNameText.fontSize = 26;        // Title size
moveDescriptionText.fontSize = 18;  // Description size
moveCostText.fontSize = 18;         // Cost size
moveEffectsText.fontSize = 16;      // Effects list size
```

## Step 6: Add to Version Control

Make sure these files are in your repository:

```
VillainLeagueUnity/Assets/Scripts/
├── MoveChooserUI.cs
├── MoveChooserUI.cs.meta
├── MoveChooserUISetup.cs
├── MoveChooserUISetup.cs.meta
├── MoveChooserUITest.cs
├── MoveChooserUITest.cs.meta
└── BattleUI.cs (modified)

Root Directory/
├── MOVE_CHOOSER_UI_GUIDE.md
├── MOVE_CHOOSER_UI_MOCKUP.md
└── MOVE_CHOOSER_UI_INTEGRATION.md (this file)
```

## Troubleshooting

### Issue: UI doesn't appear on player turn

**Solution:**
1. Check that BattleUI has moveChooserUI assigned
2. Verify the MoveChooserPanel starts inactive
3. Look for errors in Console

### Issue: Hover doesn't work

**Solution:**
1. Verify EventTrigger components on buttons
2. Check that Canvas has GraphicRaycaster
3. Ensure no UI element is blocking pointer events

### Issue: Moves list is empty

**Solution:**
1. Check that character has moveSet assigned
2. Verify moves list is not null or empty
3. Check filter (showOnlySupers) isn't filtering all moves

### Issue: Description doesn't update

**Solution:**
1. Verify all TextMeshProUGUI references are assigned
2. Check that description panel becomes active
3. Look for null reference errors in Console

### Issue: Colors don't match medieval theme

**Solution:**
1. Adjust colors in Inspector
2. Check that Image components use the colors
3. Verify TextMeshProUGUI components have correct colors

## Advanced: Creating Custom Themes

You can create theme presets by modifying the MoveChooserUI colors:

### Dark Theme
```csharp
parchmentColor = new Color(0.7f, 0.7f, 0.7f);     // Light gray
darkBorderColor = new Color(0.1f, 0.1f, 0.1f);    // Almost black
goldAccentColor = new Color(0.6f, 0.6f, 0.6f);    // Medium gray
affordableColor = new Color(0.2f, 0.2f, 0.2f);    // Dark gray
unaffordableColor = new Color(0.15f, 0.15f, 0.15f); // Darker gray
hoverColor = new Color(0.35f, 0.35f, 0.35f);      // Medium-dark gray
```

### Bright Fantasy Theme
```csharp
parchmentColor = new Color(1f, 0.95f, 0.85f);     // Cream
darkBorderColor = new Color(0.4f, 0.25f, 0.15f);  // Brown
goldAccentColor = new Color(1f, 0.85f, 0.3f);     // Bright gold
affordableColor = new Color(0.5f, 0.4f, 0.3f);    // Light brown
unaffordableColor = new Color(0.3f, 0.25f, 0.2f); // Medium brown
hoverColor = new Color(0.7f, 0.55f, 0.4f);        // Very light brown
```

## Testing Checklist

Before considering integration complete:

- [ ] Move Chooser appears on player turn
- [ ] All moves are listed in scroll view
- [ ] Hover updates description panel
- [ ] Affordable moves are clickable
- [ ] Unaffordable moves are grayed out
- [ ] Clicking move selects it
- [ ] UI hides after selection
- [ ] Move executes normally
- [ ] Colors match medieval theme
- [ ] No console errors
- [ ] Works with multiple characters
- [ ] Works with different moveset sizes
- [ ] Scroll works with many moves
- [ ] Target selection works after move selection

## Next Steps

After successful integration:

1. **Test with all characters** - Verify UI works with Bellinor, Naice, and other characters
2. **Test edge cases** - Try with no moves, one move, many moves
3. **Performance test** - Check frame rate during heavy use
4. **Polish animations** - Add smooth transitions if desired
5. **Add sound effects** - Hook up audio for hover/click events
6. **Document for your team** - Share this guide with other developers

## Support

For issues or questions:
- Check MOVE_CHOOSER_UI_GUIDE.md for detailed documentation
- Check MOVE_CHOOSER_UI_MOCKUP.md for visual reference
- Review MoveChooserUITest.cs for automated tests
- Check Unity Console for error messages
- Review code comments in MoveChooserUI.cs

## Credits

- Implemented as part of "Move choosing ui" issue
- Medieval fantasy theme for immersive gameplay experience
- Designed for easy integration with existing battle system
- Backward compatible with legacy move selection UI
