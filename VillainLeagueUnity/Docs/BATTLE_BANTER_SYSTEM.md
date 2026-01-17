# Battle Banter System

## Overview

The Battle Banter system adds dynamic, contextual dialogue between the two main characters (Bellinor Chabbeneoux and Naice Ajimi) during battle. The dialogue appears in an auto-dismissing popup with optional audio playback.

## Features

- **Auto-Dismissing Dialogue**: Banter appears in a dialogue box that automatically disappears after 3 seconds
- **Audio Playback**: Each banter line can have an associated audio file that plays when the line is displayed
- **Config-Based System**: All banter lines are stored in a JSON configuration file for easy editing
- **Context-Aware**: Different types of banter depending on the situation:
  - `move_comment`: Positive comments on partner's moves
  - `playful_insult`: Light-hearted teasing about combat style
  - `check_in`: Checking on partner's status
  - `low_hp`: Concern when partner is at low HP
- **Random Triggering**: 25% chance to trigger banter after a player character uses a move
- **Character-Specific Dialogue**: Each character has unique personality in their banter
  - **Bellinor**: Formal, protective, strategic
  - **Naice**: Playful, trickster-like, stylish
- **Audio Caching**: Loaded audio clips are cached for efficient playback

## Implementation

### Components

1. **BattleBanter.cs**
   - Manages all banter logic
   - Loads dialogue lines from JSON configuration
   - Handles random triggering and timing
   - Manages audio playback and caching
   - Key Methods:
     - `Initialize(BattleUI ui)`: Sets up the system with UI reference
     - `InitializeBanterLines()`: Loads banter config from JSON file
     - `TryTriggerBanter(Character actor, Character partner, string context)`: Attempts to trigger banter with given context
     - `ShowBanterDialogue(string speakerName, BanterLine banterLine)`: Displays the dialogue and plays audio
     - `PlayBanterAudio(string audioFileName)`: Loads and plays audio file from StreamingAssets/Audio/
     - `SetupAudioSource()`: Initializes AudioSource component

2. **BanterConfig.json** (New)
   - Located in `Assets/StreamingAssets/BanterConfig.json`
   - Contains all banter lines with associated audio file names
   - Structure:
     ```json
     {
       "bellinor": {
         "move_comment": [
           { "text": "Well executed, Naice.", "audioFile": "bellinor_well_executed.wav" }
         ],
         "playful_insult": [...],
         "check_in": [...],
         "low_hp": [...]
       },
       "naice": {
         "move_comment": [...],
         "playful_insult": [...],
         "check_in": [...],
         "low_hp": [...]
       }
     }
     ```

3. **BattleUI.cs** (Extended)
   - Added dialogue panel UI elements:
     - `banterDialoguePanel`: GameObject for the dialogue container
     - `banterDialogueText`: TextMeshProUGUI for the actual text
   - New method:
     - `ShowBanterDialogue(string dialogue, float duration)`: Shows banter and starts auto-dismiss timer

4. **BattleManager.cs** (Extended)
   - Added `BattleBanter battleBanter` reference
   - Initializes banter system in `SetupBattle()`
   - Triggers banter after move execution in `ExecuteMove()`

5. **BattleUISetup.cs** (Extended)
   - Creates banter dialogue panel at runtime
   - Positions panel at bottom center of screen
   - Wires up BattleBanter component to BattleManager

6. **BattleBanterTest.cs** (New)
   - Comprehensive test suite for banter system
   - Tests config loading, triggering logic, and audio setup
   - Can be run in Unity Editor or at runtime

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

The banter system can be configured in multiple ways:

### BattleBanter Component Settings

- `banterChance` (0-1): Probability of triggering banter after each move (default: 0.25 / 25%)
- `displayDuration`: How long the dialogue stays visible in seconds (default: 3.0)
- `audioSource`: AudioSource component for playing audio (auto-created if not assigned)

### BanterConfig.json

Edit `Assets/StreamingAssets/BanterConfig.json` to:
- Add new banter lines
- Change existing dialogue
- Update audio file references
- Add new contexts or characters

**Adding a New Banter Line:**
```json
{
  "text": "Your new dialogue line here!",
  "audioFile": "audio_filename.wav"
}
```

### Audio Files

- Audio files should be placed in `Assets/StreamingAssets/Audio/`
- Supported format: WAV (recommended for Unity)
- Files are loaded on-demand and cached for performance
- If an audio file is missing, the text will still display with a warning in the console

## Testing

### Automated Tests

The `BattleBanterTest.cs` script provides comprehensive automated testing:

1. **Run Tests in Unity:**
   - Attach `BattleBanterTest` component to a GameObject
   - Set `runTestsOnStart` to true
   - Play the scene to see test results in Console

2. **Test Coverage:**
   - Config file loading and parsing
   - Banter triggering logic (hero vs enemy, alive vs dead)
   - Context selection validation
   - AudioSource setup and configuration

### Manual Testing

To manually test the banter system in-game:

1. Open the project in Unity
2. Play the battle scene
3. During player character turns, watch for dialogue boxes appearing at the bottom center
4. Listen for audio playback (if audio files are present)
5. Dialogue should:
   - Appear randomly (about 25% of the time)
   - Auto-dismiss after 3 seconds
   - Be contextually appropriate
   - Only appear when both heroes are alive
   - Show the speaking character's name in brackets
   - Play audio if available

### Testing Audio

To test audio playback:
1. Place test audio files in `Assets/StreamingAssets/Audio/`
2. Ensure filenames match those in `BanterConfig.json`
3. Play the battle scene and trigger banter
4. Audio should play simultaneously with text display
5. Check Console for audio loading messages or warnings
   - Auto-dismiss after 3 seconds
   - Be contextually appropriate
   - Only appear when both heroes are alive
   - Show the speaking character's name in brackets

## Future Enhancements

The current implementation supports future expansion:

- **Voice Acting**: Replace placeholder audio filenames with actual voice recordings
- **Animation**: Add fade-in/fade-out effects for dialogue panel
- **More Contexts**: Add banter for critical hits, dodges, status effects, etc.
- **Relationship System**: Modify banter based on battle performance or story progression
- **Enemy-Specific Lines**: Add special dialogue for specific enemy types
- **Combo Banter**: Special lines when both characters perform well in succession
- **Dynamic Audio Volume**: Adjust volume based on battle intensity
- **Audio Mixing**: Layer dialogue with background music appropriately
- **Localization**: Support multiple languages via separate config files

## Technical Notes

- Banter only triggers for player characters, not enemies
- The system uses Unity Coroutines for timing
- Dialogue is loaded from JSON config file at initialization
- Audio clips are cached after first load for performance
- Random number generation uses System.Random for consistency
- The partner (not the actor) speaks to comment on the actor's move
- If a new banter triggers while one is showing, the old one is replaced
- Audio files are loaded using UnityWebRequest for cross-platform compatibility
- Missing audio files gracefully degrade to text-only display

## File Structure

```
VillainLeagueUnity/
├── Assets/
│   ├── Scripts/
│   │   ├── BattleBanter.cs           # Main banter system
│   │   └── BattleBanterTest.cs       # Test suite
│   └── StreamingAssets/
│       ├── BanterConfig.json         # Banter configuration
│       └── Audio/                    # Audio files directory
│           ├── bellinor_*.wav
│           └── naice_*.wav
```
