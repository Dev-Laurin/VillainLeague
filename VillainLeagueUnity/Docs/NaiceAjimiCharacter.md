# Naice Ajimi Character - Trickster/Disruptor

## Overview

Naice Ajimi is a trickster character focused on disruption, battlefield control, and evasion. Uses the **Style** resource system instead of Mana.

## Character Stats

- **Name:** Naice Ajimi
- **Role:** Trickster / Disruptor / Battlefield Control
- **Resource:** Style (max 6, regen +1 per turn)
- **HP:** 80
- **Attack:** 20
- **Defense:** 3

## Resource System: Style

Unlike Mana-based characters, Naice uses **Style** points:
- Maximum: 6 Style
- Regeneration: +1 Style per turn
- Special Rule: Gains +1 Style when an enemy misses or is fooled by an illusion (future implementation)

## Moveset (14 Moves)

### Physical Moves (Free - Always Available)

1. **Feinting Strike** (Cost: 0)
   - Deal light damage
   - If target has debuff, gain Style
   - Target: Single Enemy

2. **Take a Bow** (Cost: 0)
   - Recover Style and grant allies minor buffs
   - Conditional: Must avoid damage last turn
   - Grants +1 Attack and +1 Evasion to nearby allies
   - Target: All Allies (radius 2)

### Style Moves (Cost Style Points)

#### Low Cost (1 Style)

3. **Now You See Me** (Cost: 1)
   - Become Invisible for 1 turn
   - Gain +3 Evasion
   - Cannot be targeted directly
   - Target: Self

4. **Now You Don't** (Cost: 1)
   - Break invisibility to deal bonus damage
   - Always crits if used from Invisible
   - 5 damage
   - Target: Single Enemy

5. **Pocket Surprise** (Cost: 1)
   - Pull hidden item from coat
   - Random effect (damage, blind, stun, heal)
   - Effect power: 4
   - Target: Single Enemy

6. **Pocket Sand** (Cost: 1)
   - Blind target, reducing accuracy
   - Defense debuff: -3
   - Duration: 2 turns
   - Target: Single Enemy

7. **Cutting Remark** (Cost: 1)
   - Insult enemy, lowering Attack
   - Attack debuff: -2
   - Increases chance they target Naice
   - Duration: 2 turns
   - Target: Single Enemy

#### Medium Cost (2 Style)

8. **Smoke & Mirrors** (Cost: 2)
   - Summon illusion that draws attacks
   - Grants +5 Armor for 1 turn
   - Target: Self

9. **Change the Stage** (Cost: 2)
   - Swap positions of any two units
   - Range: 6 tiles
   - Target: Single Ally (for positioning)

10. **False Opening** (Cost: 2)
    - Trick enemy into attacking
    - If they miss, they are Stunned
    - Duration: 1 turn
    - Target: Single Enemy

11. **Escape Artist** (Cost: 2)
    - Remove all control effects from self
    - Teleport to safe tile (range 6)
    - Target: Self

#### High Cost (3 Style)

12. **Grand Distraction** (Cost: 3)
    - All enemies suffer debuffs
    - Attack debuff: -2
    - Defense debuff: -2 (accuracy)
    - Duration: 2 turns
    - Target: All Enemies

13. **Sleight of Time** (Cost: 3)
    - Delay target's next action
    - Give Naice bonus turn
    - Target: Single Enemy

#### Ultimate (5 Style)

14. **Final Act** (Cost: 5)
    - Create multiple illusions
    - Vanish and reappear
    - 5 damage to all enemies
    - Gain +1 Style per enemy affected
    - Target: All Enemies

## Playstyle Tips

### Early Game (Low Style)
- Use **Feinting Strike** and **Take a Bow** (free moves) to build Style
- **Pocket Sand** (1) to debuff priority targets
- **Cutting Remark** (1) to control enemy targeting

### Mid Game (2-4 Style)
- **Now You See Me** (1) + **Now You Don't** (1) combo for burst damage
- **Smoke & Mirrors** (2) for survivability
- **Change the Stage** (2) for tactical positioning

### Late Game (5+ Style)
- **Final Act** (5) ultimate for massive damage
- **Grand Distraction** (3) to cripple entire enemy team
- **Sleight of Time** (3) for turn manipulation

## Synergies

### With Cecelia Sylvan
- Naice can use **Change the Stage** to reposition Cecelia for optimal strikes
- Cecelia's **Rally Heart** buffs Naice's attack
- Naice's **Grand Distraction** makes enemies easier for Cecelia to hit

### General Strategy
- Focus on debuffing enemies rather than direct damage
- Use invisibility and evasion to survive
- Save Style for key moments (ultimates or critical disruptions)
- **Take a Bow** is powerful if you can avoid damage

## UI Display

When playing as Naice Ajimi:
```
┌────────────────┐
│ Naice Ajimi    │
│ HP: 80/80      │
│ ████████████   │
│ Style: 6/6     │ ← Style resource instead of Mana
└────────────────┘
```

Move buttons show:
```
┌─────────────────────────────────────┐
│ Feinting Strike        Physical     │ ← Free
│ Deal light damage...                │
└─────────────────────────────────────┘

┌─────────────────────────────────────┐
│ Now You See Me         Style: 1     │ ← Costs Style
│ Become Invisible...                 │
└─────────────────────────────────────┘
```

## Config File

Located at: `Assets/StreamingAssets/CharacterMovesets/Naice Ajimi.json`

To modify Naice's moves or stats, edit this JSON file. No code changes required!

## Implementation Notes

- Uses the same moveset system as other characters
- Supports both "manaCost" and "styleCost" fields in JSON
- UI automatically displays "Style: X" instead of "Mana: X"
- All existing game systems work with Style resource
- Easy to add more Style-based characters in the future
