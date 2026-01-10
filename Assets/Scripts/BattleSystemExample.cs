using UnityEngine;

/// <summary>
/// Example script showing how to use the battle system components
/// This is for reference and testing purposes
/// </summary>
public class BattleSystemExample : MonoBehaviour
{
    // Example: How to create characters programmatically
    void ExampleCreateCharacters()
    {
        // Create a player character
        Character hero = new Character(
            name: "Warrior",
            hp: 120,
            atk: 18,
            def: 7,
            isPlayer: true
        );

        // Create an enemy character
        Character villain = new Character(
            name: "Dark Knight",
            hp: 100,
            atk: 15,
            def: 5,
            isPlayer: false
        );

        Debug.Log($"Created {hero.characterName} with {hero.maxHP} HP");
        Debug.Log($"Created {villain.characterName} with {villain.maxHP} HP");
    }

    // Example: How damage works
    void ExampleDamageCalculation()
    {
        Character attacker = new Character("Hero", 100, 20, 5, true);
        Character defender = new Character("Enemy", 80, 10, 3, false);

        Debug.Log($"Before attack: {defender.characterName} has {defender.currentHP} HP");
        
        // Attack deals (Attack - Defense) damage, minimum 1
        int damage = attacker.attack;
        defender.TakeDamage(damage);
        
        Debug.Log($"After attack: {defender.characterName} has {defender.currentHP} HP");
    }

    // Example: How healing works
    void ExampleHealing()
    {
        Character hero = new Character("Healer", 100, 10, 5, true);
        
        // Damage the hero first
        hero.TakeDamage(30);
        Debug.Log($"After damage: {hero.currentHP} HP");
        
        // Heal the hero
        hero.Heal(20);
        Debug.Log($"After healing: {hero.currentHP} HP");
        
        // Healing can't exceed max HP
        hero.Heal(100);
        Debug.Log($"After over-healing: {hero.currentHP} HP (max: {hero.maxHP})");
    }

    // Example: Check if character is alive
    void ExampleCheckAlive()
    {
        Character hero = new Character("Hero", 100, 15, 5, true);
        
        Debug.Log($"Is {hero.characterName} alive? {hero.IsAlive()}");
        
        // Deal fatal damage
        hero.TakeDamage(150);
        
        Debug.Log($"Is {hero.characterName} alive? {hero.IsAlive()}");
        Debug.Log($"Current HP: {hero.currentHP}");
    }

    // Example usage in Start (commented out to not interfere with actual battle)
    void Start()
    {
        // Uncomment these to see examples in console
        // ExampleCreateCharacters();
        // ExampleDamageCalculation();
        // ExampleHealing();
        // ExampleCheckAlive();
    }
}
