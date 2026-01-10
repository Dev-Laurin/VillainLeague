# Moveset System Architecture

## Class Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                        Character                            │
├─────────────────────────────────────────────────────────────┤
│ - characterName: string                                     │
│ - maxHP: int                                                │
│ - currentHP: int                                            │
│ - attack: int                                               │
│ - defense: int                                              │
│ - isPlayerCharacter: bool                                   │
│ - moveSet: CharacterMoveSet                                 │
├─────────────────────────────────────────────────────────────┤
│ + SetMoveSet(CharacterMoveSet)                              │
│ + TakeDamage(int)                                           │
│ + Heal(int)                                                 │
│ + IsAlive(): bool                                           │
└──────────────────┬──────────────────────────────────────────┘
                   │
                   │ has
                   ▼
┌─────────────────────────────────────────────────────────────┐
│                   CharacterMoveSet                          │
│                  (ScriptableObject)                         │
├─────────────────────────────────────────────────────────────┤
│ + characterName: string                                     │
│ + role: string                                              │
│ + resource: CharacterResource                               │
│ + moves: List<Move>                                         │
├─────────────────────────────────────────────────────────────┤
│ + InitializeResource()                                      │
└──────────────────┬──────────┬───────────────────────────────┘
                   │          │
         has       │          │ contains
                   ▼          ▼
┌──────────────────────────┐  ┌──────────────────────────────┐
│   CharacterResource      │  │           Move               │
├──────────────────────────┤  ├──────────────────────────────┤
│ + resourceName: string   │  │ + id: string                 │
│ + maxResource: int       │  │ + moveName: string           │
│ + currentResource: int   │  │ + description: string        │
│ + regenPerTurn: int      │  │ + resourceCost: int          │
├──────────────────────────┤  │ + damage: int                │
│ + Regenerate()           │  │ + hits: int                  │
│ + CanAfford(int): bool   │  │ + healing: int               │
│ + Spend(int)             │  │ + attackBuff: int            │
└──────────────────────────┘  │ + defenseBuff: int           │
                               │ + evasion: int               │
                               │ + armor: int                 │
                               │ + ignoreGuard: bool          │
                               │ + armorPierce: int           │
                               │ + bleed: int                 │
                               │ + counterDamage: int         │
                               │ + radius: int                │
                               │ + moveRange: int             │
                               │ + durationTurns: int         │
                               │ + targetType: MoveTargetType │
                               └──────────────────────────────┘

┌─────────────────────────────────────────────────────────────┐
│                    MoveSetFactory                           │
│                      (static)                               │
├─────────────────────────────────────────────────────────────┤
│ + CreateCeceliaSylvanMoveSet(): CharacterMoveSet            │
│ + CreateDefaultMoveSet(string): CharacterMoveSet            │
└─────────────────────────────────────────────────────────────┘
```

## Data Flow Diagram

```
┌─────────────────────────────────────────────────────────────┐
│                     Game Start                              │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│            BattleManager.SetupBattle()                      │
│                                                             │
│  1. Create characters                                       │
│  2. Assign movesets via MoveSetFactory                      │
│  3. Initialize resources                                    │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────────────┐
│                  Battle Loop                                │
└──────────────────────┬──────────────────────────────────────┘
                       │
                       ▼
            ┌──────────┴──────────┐
            │                     │
            ▼                     ▼
    ┌───────────────┐     ┌───────────────┐
    │  Player Turn  │     │  Enemy Turn   │
    └───────┬───────┘     └───────┬───────┘
            │                     │
            ▼                     ▼
    ┌───────────────────────────────────┐
    │ Regenerate Resource               │
    └───────┬───────────────────────────┘
            │
            ▼
    ┌───────────────────────────────────┐
    │ Has MoveSet?                      │
    └───────┬───────────────────────────┘
            │
      ┌─────┴─────┐
      │           │
     YES         NO
      │           │
      ▼           ▼
┌─────────────┐ ┌───────────────────┐
│ Select Move │ │ Old System        │
│             │ │ (Attack/Defend)   │
└──────┬──────┘ └───────────────────┘
       │
       ▼
┌──────────────────────────────────┐
│ BattleUI.DisplayMoves()          │
│                                  │
│ - Show move list                 │
│ - Check affordability            │
│ - Display costs                  │
└──────┬───────────────────────────┘
       │
       ▼
┌──────────────────────────────────┐
│ Player Selects Move              │
└──────┬───────────────────────────┘
       │
       ▼
┌──────────────────────────────────┐
│ Need Target?                     │
└──────┬───────────────────────────┘
       │
  ┌────┴────┐
  │         │
 YES       NO
  │         │
  ▼         │
┌──────────────────┐ │
│ Select Target    │ │
└──────┬───────────┘ │
       │             │
       └─────┬───────┘
             │
             ▼
┌──────────────────────────────────┐
│ Execute Move                     │
│                                  │
│ 1. Spend resource                │
│ 2. Apply damage/healing          │
│ 3. Apply effects                 │
│ 4. Update UI                     │
└──────────────────────────────────┘
```

## UI Flow

```
┌────────────────────────────────────────────────────────┐
│                   Player Turn Starts                   │
│                                                        │
│  ┌──────────────────────────────────────────────────┐ │
│  │ Player Squad        │        Enemy Squad         │ │
│  │                     │                            │ │
│  │ Cecelia Sylvan      │        Villain 1          │ │
│  │ HP: 100/100         │        HP: 70/70          │ │
│  │ Focus: 6/6  ← Resource Display                  │ │
│  └──────────────────────────────────────────────────┘ │
└─────────────────┬──────────────────────────────────────┘
                  │
                  ▼
┌────────────────────────────────────────────────────────┐
│             Move Selection Panel Appears               │
│                                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │           SELECT MOVE                          │   │
│  │                                                │   │
│  │  [Twin Slash            Cost: 0  ] ← Clickable│   │
│  │  Deal physical damage...                      │   │
│  │                                                │   │
│  │  [Blink Strike          Cost: 2  ] ← Clickable│   │
│  │  Teleport to target...                        │   │
│  │                                                │   │
│  │  [Flash Step            Cost: 1  ] ← Clickable│   │
│  │  Teleport to empty tile...                    │   │
│  │                                                │   │
│  │  ... (7 more moves) ...                       │   │
│  │                                                │   │
│  └────────────────────────────────────────────────┘   │
└─────────────────┬──────────────────────────────────────┘
                  │ Player clicks move
                  ▼
┌────────────────────────────────────────────────────────┐
│              Target Selection (if needed)              │
│                                                        │
│  ┌────────────────────────────────────────────────┐   │
│  │         SELECT TARGET                          │   │
│  │                                                │   │
│  │  [Villain 1  HP: 70/70]                       │   │
│  │                                                │   │
│  │  [Villain 2  HP: 90/90]                       │   │
│  │                                                │   │
│  └────────────────────────────────────────────────┘   │
└─────────────────┬──────────────────────────────────────┘
                  │ Player clicks target
                  ▼
┌────────────────────────────────────────────────────────┐
│                  Move Executes                         │
│                                                        │
│  Message: "Cecelia Sylvan uses Blink Strike!"         │
│  Message: "Hits Villain 1 for 5 damage!"              │
│                                                        │
│  Resource Updated: Focus: 4/6 ← Spent 2               │
│  Target HP Updated: Villain 1 HP: 65/70               │
└────────────────────────────────────────────────────────┘
```

## Interaction Sequence

```
Player → BattleManager → BattleUI → Player → BattleManager
  │                                            │
  │ Turn starts                                │
  │                                            │
  │ ───────────────→ Regenerate resource       │
  │                  Check for moveset         │
  │                                            │
  │                  Show move selection       │
  │                  ─────────────────→        │
  │                                   │        │
  │                            Display moves   │
  │                            Check costs     │
  │                            Show UI         │
  │                                   │        │
  │ ←────────────────────────────────┘        │
  │ Select move (click)                        │
  │                                            │
  │ ──────────────────────────────────────────→│
  │                               Validate cost│
  │                               Need target? │
  │                                            │
  │                  Show target selection     │
  │                  ─────────────────→        │
  │                                   │        │
  │ ←────────────────────────────────┘        │
  │ Select target (click)                      │
  │                                            │
  │ ──────────────────────────────────────────→│
  │                            Execute move    │
  │                            Spend resource  │
  │                            Apply effects   │
  │                            Update UI       │
  │                                            │
  │ ←─────────────────────────── Turn ends     │
```

## Key Design Decisions

1. **Factory Pattern**: Used MoveSetFactory for creating movesets
   - Easy to add new characters
   - Centralized move definitions
   - Consistent initialization

2. **ScriptableObject**: CharacterMoveSet as ScriptableObject
   - Can be created as assets in Unity
   - Supports inspector editing
   - Reusable and serializable

3. **Backward Compatibility**: Old system still works
   - Characters without movesets use attack/defend/special
   - No breaking changes
   - Smooth transition

4. **Dynamic UI**: Move buttons created programmatically
   - Supports any number of moves
   - Automatic layout
   - Visual affordability feedback

5. **Resource Management**: Per-character resource pools
   - Different types (Focus, Energy, etc.)
   - Automatic regeneration
   - Cost validation

6. **Extensibility**: Easy to add new features
   - New move properties can be added to Move class
   - New target types can be added to enum
   - New movesets can be added to factory
