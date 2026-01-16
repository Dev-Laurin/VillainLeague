# Changelog

## v2.0.0

Added test text for gitbook test

## v1.9 - 2026-01-15

### PR #18 - Add comprehensive README.md for repository
- Created root README.md as single entry point for the project
- Added project description with game mechanics documentation
- Documented Character Moveset System, Super Abilities, Battle Banter, and Move Chooser UI
- Added documentation index with 25+ links organized by category
- Included project structure and architecture overview
- Added character details for Bellinor, Naice, and Cecelia
- Added installation instructions and roadmap

## v1.8 - 2026-01-15

### PR #16 - Add medieval fantasy-themed Move Chooser UI with hover details panel
- Implemented popup move selection UI with scrollable move list
- Added separate description panel that updates on hover
- Created MoveChooserUI.cs with dynamic button generation
- Added MoveChooserUISetup.cs for programmatic UI structure
- Added MoveChooserUITest.cs with 5 automated tests
- Integrated with BattleUI with backward compatibility
- Designed medieval theme with parchment colors and Unicode icons
- Added affordability checking and hover feedback

## v1.7 - 2026-01-15

### PR #14 - Add auto-dismissing battle banter dialogue between heroes
- Implemented mid-battle dialogue between Bellinor and Naice
- Added BattleBanter.cs with 25% trigger chance
- Added 20+ character-specific lines per character
- Created auto-dismissing popup (3s) requiring no player interaction
- Modified BattleManager.cs to trigger banter post-move execution
- Updated BattleUI.cs with dialogue panel display
- Added dialogue panel creation in BattleUISetup.cs

## v1.6 - 2026-01-10

### PR #13 - Add super ability system with secondary resource charging and team attacks
- Implemented super ability system with secondary resources (Style, Resolve)
- Added IsSuperReady() method to check if resource is at maximum
- Added isSuper and secondaryResourceCost fields to Move class
- Created AreBothHeroSupersReady() logic for team super attacks
- Added team super button and three-button choice UI
- Updated character JSON files with super move flags
- Added styleGain to 9 moves for charging mechanics
- Modified MoveSetLoader to parse new super move fields

## v1.5 - 2026-01-10

### PR #12 - Re-implement character-specific movesets with JSON config system and dual resource types
- Re-applied character moveset system after revert

## v1.4 - 2026-01-10

### PR #11 - Revert "Implement character-specific movesets with JSON config system and dual resource types"
- Temporarily reverted PR #9 changes

## v1.3 - 2026-01-10

### PR #9 - Implement character-specific movesets with JSON config system and dual resource types
- Created Move class for move properties with physical/magic distinction
- Added CharacterResource class with regeneration logic
- Created CharacterMoveSet container for character moves and resources
- Implemented MoveSetFactory and MoveSetLoader for JSON-based config
- Added moveSet field and SetMoveSet() method to Character
- Updated BattleManager to use MoveSetLoader with config files
- Modified BattleUI to display move types and resource costs
- Added support for primary and secondary resources
- Created character config files for Bellinor Chabbeneoux (14 moves, Resolve)
- Created character config files for Naice Ajimi (14 moves, Mana + Style)
- Created character config files for Cecelia Sylvan (10 moves, Mana)
- Updated player squad to use Bellinor and Naice

## v1.2 - 2026-01-10

### PR #6 - Implement battle scene UI with automatic runtime generation
- Added BattleUISetup.cs for programmatic UI generation at runtime
- Created Canvas, EventSystem, and all UI elements in Awake()
- Generated player squad display (left, green) and enemy squad display (right, red)
- Built turn order display showing current and next character
- Constructed action buttons (Attack/Defend/Special) and target selection modal
- Enhanced TurnManager with GetNextCharacter() for lookahead
- Added BattleUI.nextTurnText for upcoming turn display
- Updated BattleManager to display both current and next turn
- Eliminated manual Unity Editor UI setup requirement
- Added live HP bar updates during battle

## v1.1 - 2026-01-10

### PR #1 - Implement Unity 2D turn-based battle system with squad mechanics
- Created BattleManager.cs (256 lines) with state machine for battle flow
- Added TurnManager.cs (65 lines) for turn order rotation
- Implemented Character.cs (43 lines) with HP/ATK/DEF stats
- Created BattleUI.cs (115 lines) for UI framework
- Added BattleSystemExample.cs (95 lines) with usage examples
- Implemented 2-hero squad vs 2-villain combat
- Added three action types: Attack, Defend, Special
- Created damage calculation system: max(1, attackValue - defense)
- Implemented enemy AI with random targeting
- Added battle end conditions when squad is defeated

## v1.0.0

### Added or Changed

* Added this changelog :)
* Fixed typos in both templates
* Back to top links
* Added more "Built With" frameworks/libraries
* Changed table of contents to start collapsed
* Added checkboxes for major features on roadmap

### Removed

* Some packages/libraries from acknowledgements I no longer use
