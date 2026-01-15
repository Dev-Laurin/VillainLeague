# Battle Banter System

## Overview

The Battle Banter system adds dynamic, contextual dialogue between the two main characters (Bellinor Chabbeneoux and Naice Ajimi) during battle. The dialogue appears in an auto-dismissing popup that doesn't require player interaction.

## Features

- **Auto-Dismissing Dialogue**: Banter appears in a dialogue box that automatically disappears after 3 seconds
- **Context-Aware**: Different types of banter depending on the situation:
  - `move_comment`: Positive comments on partner's moves
  - `playful_insult`: Light-hearted teasing about combat style
  - `check_in`: Checking on partner's status
  - `low_hp`: Concern when partner is at low HP
- **Random Triggering**: 25% chance to trigger banter after a player character uses a move
- **Character-Specific Dialogue**: Each character has unique personality in their banter
  - **Bellinor**: Formal, protective, strategic
  - **Naice**: Playful, trickster-like, stylish

## Implementation

### Components

1. **BattleBanter.cs**
   - Manages all banter logic
   - Stores character-specific dialogue lines
   - Handles random triggering and timing
   - Methods:
     - `Initialize(BattleUI ui)`: Sets up the system with UI reference
     - `TryTriggerBanter(Character actor, Character partner, string context)`: Attempts to trigger banter with given context
     - `ShowBanterDialogue(string speakerName, string dialogue)`: Displays the dialogue

2. **BattleUI.cs** (Extended)
   - Added dialogue panel UI elements:
     - `banterDialoguePanel`: GameObject for the dialogue container
     - `banterDialogueText`: TextMeshProUGUI for the actual text
   - New method:
     - `ShowBanterDialogue(string dialogue, float duration)`: Shows banter and starts auto-dismiss timer

3. **BattleManager.cs** (Extended)
   - Added `BattleBanter battleBanter` reference
   - Initializes banter system in `SetupBattle()`
   - Triggers banter after move execution in `ExecuteMove()`

4. **BattleUISetup.cs** (Extended)
   - Creates banter dialogue panel at runtime
   - Positions panel at bottom center of screen
   - Wires up BattleBanter component to BattleManager

### UI Layout

The banter dialogue panel:
- Position: Bottom center of screen (Y: -350)
- Size: 700x120 pixels
- Background: Dark blue-gray with high opacity (0.15, 0.15, 0.2, 0.9)
- Text color: Slightly yellow-tinted white (1, 1, 0.8) for visibility
- Format: `[Character Name] Dialogue text`

## Example Dialogue

### Bellinor's Lines

**Move Comments:**
- "Well executed, Naice."
- "That was... surprisingly effective."
- "Impressive form, though a bit flashy."

**Playful Insults:**
- "Was that supposed to be intimidating?"
- "A bit theatrical, don't you think?"
- "All style, as usual."

**Check-ins:**
- "Stay focused, Naice."
- "Are you holding up alright?"
- "Keep your guard up."

**Low HP:**
- "Naice, fall back! I'll handle this."
- "You're hurt. Let me take point."

### Naice's Lines

**Move Comments:**
- "Now THAT'S how it's done!"
- "Ooh, nice one Bell!"
- "Didn't know you had that in you!"

**Playful Insults:**
- "Bit slow on that one, weren't you?"
- "Did you even TRY to look cool?"
- "Where's the flair, Bell?"

**Check-ins:**
- "Still with me, Bell?"
- "You good over there?"
- "We've got this, right?"

**Low HP:**
- "Bell! You're looking rough!"
- "Hey, don't be a hero now!"
- "Take a breather, I've got this!"

## Configuration

The banter system can be configured via the `BattleBanter` component:

- `banterChance` (0-1): Probability of triggering banter after each move (default: 0.25 / 25%)
- `displayDuration`: How long the dialogue stays visible in seconds (default: 3.0)

## Testing

To test the banter system:

1. Open the project in Unity
2. Play the battle scene
3. During player character turns, watch for dialogue boxes appearing at the bottom center
4. Dialogue should:
   - Appear randomly (about 25% of the time)
   - Auto-dismiss after 3 seconds
   - Be contextually appropriate
   - Only appear when both heroes are alive
   - Show the speaking character's name in brackets

## Future Enhancements

The current implementation supports future expansion:

- **Audio Integration**: Replace or supplement text with voice lines
- **Animation**: Add fade-in/fade-out effects
- **More Contexts**: Add banter for critical hits, dodges, status effects, etc.
- **Relationship System**: Modify banter based on battle performance or story progression
- **Enemy-Specific Lines**: Add special dialogue for specific enemy types
- **Combo Banter**: Special lines when both characters perform well in succession

## Technical Notes

- Banter only triggers for player characters, not enemies
- The system uses Unity Coroutines for timing
- Dialogue is stored in Dictionary structures for easy lookup
- Random number generation uses System.Random for consistency
- The partner (not the actor) speaks to comment on the actor's move
- If a new banter triggers while one is showing, the old one is replaced
