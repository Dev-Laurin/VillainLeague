# Screenshot Guide

Since Unity is not available in this environment, here's what you should expect to see when you run the battle scene:

## When You Press Play in Unity

### Initial View (Battle Start)
```
┌─────────────────────────────────────────────────────────────────────────┐
│                                                                         │
│                   Turn: Hero 1        Next: Hero 2                      │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌─────────────────┐                           ┌─────────────────┐    │
│  │ Hero 1          │                           │      Villain 1  │    │
│  │ 100/100         │      Battle Start!        │          70/70  │    │
│  │ ████████████    │                           │    ████████████ │    │
│  │                 │                           │                 │    │
│  │ Hero 2          │                           │      Villain 2  │    │
│  │ 80/80           │                           │          90/90  │    │
│  │ ████████████    │                           │    ████████████ │    │
│  └─────────────────┘                           └─────────────────┘    │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│             [ ATTACK ]  [ DEFEND ]  [ SPECIAL ]                         │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

### During Target Selection
```
┌─────────────────────────────────────────────────────────────────────────┐
│                                                                         │
│                   Turn: Hero 1        Next: Hero 2                      │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌─────────────────┐   ┌──────────────┐      ┌─────────────────┐     │
│  │ Hero 1          │   │ SELECT TARGET│      │      Villain 1  │     │
│  │ 100/100         │   │              │      │          70/70  │     │
│  │ ████████████    │   │ [ Villain 1 ]│      │    ████████████ │     │
│  │                 │   │              │      │                 │     │
│  │ Hero 2          │   │ [ Villain 2 ]│      │      Villain 2  │     │
│  │ 80/80           │   │              │      │          90/90  │     │
│  │ ████████████    │   └──────────────┘      │    ████████████ │     │
│  └─────────────────┘                         └─────────────────┘     │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

### After Hero 1 Attacks Villain 1
```
┌─────────────────────────────────────────────────────────────────────────┐
│                                                                         │
│                   Turn: Hero 2        Next: Villain 1                   │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌─────────────────┐                           ┌─────────────────┐    │
│  │ Hero 1          │                           │      Villain 1  │    │
│  │ 100/100         │  Hero 1 attacks           │          59/70  │    │
│  │ ████████████    │  Villain 1 for 11 damage! │    ████████     │    │
│  │                 │                           │                 │    │
│  │ Hero 2          │                           │      Villain 2  │    │
│  │ 80/80           │                           │          90/90  │    │
│  │ ████████████    │                           │    ████████████ │    │
│  └─────────────────┘                           └─────────────────┘    │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│             [ ATTACK ]  [ DEFEND ]  [ SPECIAL ]                         │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

### During Enemy Turn
```
┌─────────────────────────────────────────────────────────────────────────┐
│                                                                         │
│                   Turn: Villain 1     Next: Villain 2                   │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌─────────────────┐                           ┌─────────────────┐    │
│  │ Hero 1          │                           │      Villain 1  │    │
│  │ 93/100          │  Villain 1 attacks        │          59/70  │    │
│  │ ███████████     │  Hero 1 for 7 damage!     │    ████████     │    │
│  │                 │                           │                 │    │
│  │ Hero 2          │                           │      Villain 2  │    │
│  │ 80/80           │                           │          90/90  │    │
│  │ ████████████    │                           │    ████████████ │    │
│  └─────────────────┘                           └─────────────────┘    │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

### Victory Screen (When All Villains Defeated)
```
┌─────────────────────────────────────────────────────────────────────────┐
│                                                                         │
│                   Turn: Hero 1        Next: Hero 2                      │
│                                                                         │
├─────────────────────────────────────────────────────────────────────────┤
│                                                                         │
│  ┌─────────────────┐                           ┌─────────────────┐    │
│  │ Hero 1          │                           │      Villain 1  │    │
│  │ 85/100          │                           │          0/70   │    │
│  │ ██████████      │       Victory!            │                 │    │
│  │                 │                           │                 │    │
│  │ Hero 2          │                           │      Villain 2  │    │
│  │ 60/80           │                           │          0/90   │    │
│  │ █████████       │                           │                 │    │
│  └─────────────────┘                           └─────────────────┘    │
│                                                                         │
└─────────────────────────────────────────────────────────────────────────┘
```

## Color Reference

When you take screenshots, you should see:

### Player Team (Left Side)
- Names: **Bright Green** (#33CC33 / RGB: 51, 204, 51)
- HP Bars: **Green** (#33CC33)
- HP Text: **White**
- Background: **Dark Gray** with transparency

### Enemy Team (Right Side)
- Names: **Bright Red** (#E63333 / RGB: 230, 51, 51)
- HP Bars: **Red** (#E63333)
- HP Text: **White**
- Background: **Dark Gray** with transparency

### Turn Order (Top)
- Current Turn: **White**, Large font (36px)
- Next Turn: **Light Gray** (#B3B3B3), Smaller font (24px)

### Action Buttons (Bottom)
- Background: **Medium Gray** (#4D4D4D)
- Text: **White** (24px)
- Hover effect: Lighter gray
- Click effect: Darker gray

### Messages (Center)
- Text: **White** (28px)
- Background: None (transparent)

## What to Screenshot

For documentation/PR review, please take screenshots of:

1. **Initial Battle Start** - Showing full UI with all elements
2. **Target Selection** - Modal panel visible
3. **Mid-Battle** - After a few turns with changed HP values
4. **Victory Screen** - When battle is won

## Screenshot Tips

1. Make sure Unity Game view is visible
2. Press Play to start the battle
3. Use Unity's screenshot feature (Game view > right-click > "Screenshot")
4. Or use a screen capture tool
5. Capture at a reasonable resolution (1920x1080 recommended)

## Expected UI Layout

- **Top**: Turn order display (centered)
- **Left**: Player squad with green theme
- **Right**: Enemy squad with red theme
- **Center**: Battle messages
- **Bottom**: Action buttons (only during player turn)
- **Modal**: Target selection panel (when attacking)

## Verification Checklist

When taking screenshots, verify:
- ✅ All text is visible and readable
- ✅ HP bars are displaying correctly
- ✅ Colors match the theme (green/red)
- ✅ Turn order shows current and next
- ✅ Buttons appear during player turn
- ✅ Target selection panel appears when needed
- ✅ HP values update correctly
- ✅ Messages display battle actions
- ✅ Layout is clean and organized
- ✅ No visual glitches or overlaps

---

Since I cannot run Unity in this sandboxed environment, these mockups represent what the actual implementation will look like when run in Unity Editor. The code has been carefully designed to create this exact layout programmatically.
