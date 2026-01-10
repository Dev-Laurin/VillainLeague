# GitHub Copilot Instructions for Villain League

## Overview
When contributing to the Villain League project, follow these comprehensive guidelines to ensure high-quality, well-documented, and maintainable code.

## Core Principles

### 1. Best Coding Practices
- Write clean, readable, and maintainable code
- Follow C# coding conventions and Unity best practices
- Use meaningful variable and method names that clearly describe their purpose
- Keep methods focused and single-purpose (Single Responsibility Principle)
- Avoid code duplication - refactor common logic into reusable methods
- Handle edge cases and validate inputs appropriately
- Use proper error handling with try-catch blocks where appropriate
- Comment complex logic, but prefer self-documenting code
- Follow SOLID principles for object-oriented design

### 2. Documentation Requirements
Every code contribution must include:

#### Code-Level Documentation
- **XML Documentation Comments** for all public classes, methods, and properties
  ```csharp
  /// <summary>
  /// Calculates damage dealt to a character based on attacker's stats and defender's defense.
  /// </summary>
  /// <param name="attacker">The character performing the attack</param>
  /// <param name="defender">The character receiving the damage</param>
  /// <returns>The final damage amount after defense calculation</returns>
  public int CalculateDamage(Character attacker, Character defender)
  ```
- Inline comments for complex algorithms or non-obvious logic
- Clear explanations of game mechanics and formulas used

#### Project Documentation
- Update relevant `.md` files when adding or modifying features
- Update `BATTLE_SYSTEM_README.md` for battle system changes
- Update `PROJECT_OVERVIEW.md` for architectural changes
- Update `QUICKSTART.md` if setup steps change
- Update `README.md` for new features visible to users

### 3. Testing Requirements
- Write unit tests for all new functionality
- Follow existing test patterns in the project
- Test edge cases and boundary conditions
- Ensure tests are isolated and repeatable
- Name tests clearly: `MethodName_Scenario_ExpectedBehavior`
  ```csharp
  public void TakeDamage_WithPositiveValue_ReducesCurrentHP()
  public void TakeDamage_ExceedingHP_SetsHPToZero()
  ```
- Update `TESTING_GUIDE.md` when adding new test categories or patterns
- Verify all tests pass before submitting changes

### 4. Wiki/Guide Updates for Gameplay Mechanics
When adding or modifying gameplay mechanics, create or update documentation from a **player's perspective**:

#### What to Document
- **New Mechanics**: How they work, when they trigger, what they do
- **Combat Systems**: Damage calculations, status effects, special abilities
- **Character Stats**: What each stat does, how it affects gameplay
- **Battle Actions**: Available actions, their effects, and strategic uses
- **Win/Lose Conditions**: How players win or lose battles
- **Items and Equipment**: How to use them, what effects they have

#### Where to Document
- Create gameplay guides in `/docs/gameplay/` directory
- Update existing guides if mechanics change
- Use clear examples and scenarios players would encounter
- Include screenshots or diagrams where helpful

#### Format Example
```markdown
## Special Attack

### What It Does
Special attacks deal 2x normal damage to an enemy but can only be used once per turn.

### How to Use
1. During your turn, click the "Special" button
2. Select your target enemy
3. The attack automatically executes with double damage

### Strategic Tips
- Save special attacks for enemies with low defense
- Use when enemy HP is high to maximize impact
- Coordinate with defend actions for survivability
```

### 5. Changelog Requirements
**ALWAYS** update `CHANGELOG.md` with your changes:

#### Format
```markdown
## [Unreleased]

### Added
- New special attack system with 2x damage multiplier
- Character class system with three types: Warrior, Mage, Rogue

### Changed
- Damage calculation now considers character level
- Defense stat now reduces damage by percentage instead of flat amount

### Fixed
- Battle not ending when all enemies defeated
- HP bar not updating after healing

### Removed
- Deprecated old damage formula
```

#### When to Update
- **Added**: New features, files, or functionality
- **Changed**: Modifications to existing features
- **Fixed**: Bug fixes
- **Removed**: Deprecated or removed functionality

### 6. Feature Articles/Blog Posts
For **every significant feature** you implement, create a markdown article in `/docs/articles/` directory:

#### Naming Convention
- Use format: `YYYY-MM-DD-feature-name.md`
- Example: `2026-01-10-special-attack-system.md`

#### Required Sections
1. **Title and Metadata**
   ```markdown
   # Implementing the Special Attack System
   
   **Date**: January 10, 2026
   **Author**: GitHub Copilot
   **Feature**: Special Attack System
   **Pull Request**: #123
   ```

2. **Overview**
   - Brief description of what was built
   - Why it was needed
   - What problem it solves

3. **Technical Approach**
   - Architecture decisions made
   - Design patterns used
   - Key algorithms or logic

4. **Implementation Details**
   - Step-by-step explanation of how you built it
   - Include code snippets with explanations
   ```csharp
   // Example code snippet
   public void ExecuteSpecialAttack(Character attacker, Character target)
   {
       int baseDamage = attacker.AttackPower;
       int specialDamage = baseDamage * 2; // Double damage for special
       target.TakeDamage(specialDamage);
   }
   ```

5. **Challenges and Solutions**
   - Problems encountered during development
   - How you solved them
   - Lessons learned

6. **Testing**
   - How you tested the feature
   - Test cases created
   - Edge cases considered

7. **Player Impact**
   - How this feature improves gameplay
   - What players can now do
   - Strategic implications

8. **Future Improvements**
   - Potential enhancements
   - Known limitations
   - Ideas for expansion

#### Example Article Structure
```markdown
# Implementing the Special Attack System

**Date**: January 10, 2026
**Author**: GitHub Copilot
**Feature**: Special Attack System
**Pull Request**: #123

## Overview

The Special Attack System adds a powerful new combat option that allows characters to deal double damage once per turn. This feature was requested to add more strategic depth to battles and give players meaningful tactical choices beyond basic attacks.

## Technical Approach

I implemented this using the Command pattern, creating a `SpecialAttackCommand` class that inherits from the base `BattleCommand` class. This keeps the code modular and follows the existing battle system architecture.

### Key Design Decisions

1. **Damage Multiplier**: Set at 2x for balance
2. **Cooldown System**: One use per turn to prevent spam
3. **Target Selection**: Single target for focused damage

## Implementation Details

### Step 1: Creating the Command Class

First, I created the `SpecialAttackCommand.cs` file:

```csharp
public class SpecialAttackCommand : BattleCommand
{
    private const int DAMAGE_MULTIPLIER = 2;
    
    public override void Execute(Character attacker, Character target)
    {
        int damage = attacker.AttackPower * DAMAGE_MULTIPLIER;
        target.TakeDamage(damage);
        BattleLog.AddEntry($"{attacker.Name} used Special Attack on {target.Name} for {damage} damage!");
    }
}
```

### Step 2: Integrating with Battle System

[Continue with detailed explanation...]

## Challenges and Solutions

### Challenge 1: Balance Issues
Initially, the 2x multiplier was too powerful in early battles...

[Continue with challenges and solutions...]

## Testing

Created comprehensive tests in `SpecialAttackTests.cs`:
- Test that damage is exactly 2x base attack
- Test that special attack respects defense calculations
- Test edge cases like defeated targets

## Player Impact

Players now have more tactical options in battle:
- Save special attacks for tough enemies
- Use regular attacks on weak foes
- Plan turn sequences for maximum efficiency

## Future Improvements

- Add visual effects for special attacks
- Implement character-specific special moves
- Add combo system for chaining specials
```

## Project-Specific Guidelines

### Unity Best Practices
- Use Unity's component-based architecture
- Leverage ScriptableObjects for game data
- Use Unity Events for decoupled systems
- Follow Unity's serialization guidelines
- Use proper prefab workflows

### Battle System Specifics
- All battle mechanics must respect turn order
- Damage calculations use the formula: `max(1, AttackPower - Defense)`
- Always validate character state before actions (IsAlive check)
- Update UI immediately after stat changes
- Log all battle events for debugging

### Code Organization
- Scripts go in `Assets/Scripts/`
- Tests go in `Assets/Tests/`
- Documentation goes in `/docs/` or root-level `.md` files
- Articles go in `/docs/articles/`
- Gameplay guides go in `/docs/gameplay/`

## Checklist for Every Contribution

Before submitting any code changes, ensure:

- [ ] Code follows C# and Unity best practices
- [ ] XML documentation added for public APIs
- [ ] Inline comments added for complex logic
- [ ] Unit tests written and passing
- [ ] Relevant .md files updated
- [ ] CHANGELOG.md updated with changes
- [ ] Gameplay guide created/updated (if applicable)
- [ ] Feature article created in `/docs/articles/`
- [ ] All existing tests still pass
- [ ] Code has been manually tested in Unity
- [ ] No debug code or console logs left in production code

## Summary

Quality contributions to Villain League require:
1. **Clean, well-documented code** with tests
2. **Updated documentation** for users and developers
3. **Changelog entries** tracking all changes
4. **Feature articles** explaining how and why you built it
5. **Gameplay guides** helping players understand mechanics

By following these guidelines, we maintain a high-quality, well-documented codebase that's easy for contributors to understand and for players to enjoy.
