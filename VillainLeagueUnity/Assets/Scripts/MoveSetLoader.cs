using UnityEngine;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Serializable data structures for loading character movesets from JSON
/// </summary>
[System.Serializable]
public class MoveData
{
    public string id;
    public string name;
    public string description;
    public string type; // "physical" or "magic"
    public int manaCost; // Resource cost (generic, can be mana, style, etc.)
    public int styleCost; // Alias for resource cost (for Style-based characters)
    public int damage;
    public int hits = 1;
    public int healing;
    public int attackBuff;
    public int attackDebuff;
    public int defenseBuff;
    public int defenseDebuff;
    public int evasion;
    public int armor;
    public bool ignoreGuard;
    public int armorPierce;
    public int bleed;
    public int counterDamage;
    public int styleGain; // Secondary resource gain
    public int secondaryResourceCost; // Secondary resource cost (for super moves)
    public bool isSuper; // True if this is a super/ultimate move
    public int charmPoints; // Charm points for charm objectives
    public bool canHarmAllies; // For moves that can harm allies (e.g., counterattacks)
    public int radius;
    public int moveRange;
    public int durationTurns = 1;
    public string targetType = "SingleEnemy";
}

[System.Serializable]
public class ResourceData
{
    public string name = "Mana";
    public int max = 10;
    public int regenPerTurn = 1;
}

[System.Serializable]
public class CharacterMoveSetData
{
    public string characterName;
    public string role;
    public ResourceData resource;
    public List<MoveData> moves;
}

/// <summary>
/// Loads character movesets from JSON configuration files
/// </summary>
public static class MoveSetLoader
{
    private static string GetConfigPath()
    {
        // Use StreamingAssets for runtime loading
        return Path.Combine(Application.streamingAssetsPath, "CharacterMovesets");
    }

    /// <summary>
    /// Load a character moveset from a JSON file
    /// </summary>
    public static CharacterMoveSet LoadMoveSetFromFile(string characterName)
    {
        string filePath = Path.Combine(GetConfigPath(), $"{characterName}.json");
        
        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"Moveset file not found for {characterName} at {filePath}. Using default moveset.");
            return CreateDefaultMoveSet(characterName);
        }

        try
        {
            string jsonContent = File.ReadAllText(filePath);
            CharacterMoveSetData data = JsonUtility.FromJson<CharacterMoveSetData>(jsonContent);
            return ConvertToMoveSet(data);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading moveset for {characterName}: {e.Message}");
            return CreateDefaultMoveSet(characterName);
        }
    }

    /// <summary>
    /// Convert JSON data to CharacterMoveSet
    /// </summary>
    private static CharacterMoveSet ConvertToMoveSet(CharacterMoveSetData data)
    {
        CharacterMoveSet moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
        moveSet.characterName = data.characterName;
        moveSet.role = data.role;
        
        // Create resource (Mana)
        moveSet.resource = new CharacterResource(
            data.resource.name,
            data.resource.max,
            data.resource.regenPerTurn
        );
        
        moveSet.moves = new List<Move>();
        
        // Convert each move
        foreach (MoveData moveData in data.moves)
        {
            // Physical attacks cost 0, magic/ability attacks cost resources
            bool isPhysical = (moveData.type == "physical");
            // Support both manaCost and styleCost fields (use whichever is non-zero)
            int cost = isPhysical ? 0 : Mathf.Max(moveData.manaCost, moveData.styleCost);
            
            Move move = new Move(moveData.id, moveData.name, moveData.description, cost);
            move.isPhysical = isPhysical;
            move.damage = moveData.damage;
            move.hits = moveData.hits;
            move.healing = moveData.healing;
            move.attackBuff = moveData.attackBuff;
            move.attackDebuff = moveData.attackDebuff;
            move.defenseBuff = moveData.defenseBuff;
            move.defenseDebuff = moveData.defenseDebuff;
            move.evasion = moveData.evasion;
            move.armor = moveData.armor;
            move.ignoreGuard = moveData.ignoreGuard;
            move.armorPierce = moveData.armorPierce;
            move.bleed = moveData.bleed;
            move.counterDamage = moveData.counterDamage;
            move.styleGain = moveData.styleGain;
            move.secondaryResourceCost = moveData.secondaryResourceCost;
            move.isSuper = moveData.isSuper;
            move.charmPoints = moveData.charmPoints;
            move.canHarmAllies = moveData.canHarmAllies;
            move.radius = moveData.radius;
            move.moveRange = moveData.moveRange;
            move.durationTurns = moveData.durationTurns;
            
            // Parse target type
            move.targetType = ParseTargetType(moveData.targetType);
            
            moveSet.moves.Add(move);
        }
        
        return moveSet;
    }

    private static MoveTargetType ParseTargetType(string targetType)
    {
        switch (targetType)
        {
            case "SingleEnemy": return MoveTargetType.SingleEnemy;
            case "SingleAlly": return MoveTargetType.SingleAlly;
            case "AllEnemies": return MoveTargetType.AllEnemies;
            case "AllAllies": return MoveTargetType.AllAllies;
            case "Self": return MoveTargetType.Self;
            case "Area": return MoveTargetType.Area;
            default: return MoveTargetType.SingleEnemy;
        }
    }

    /// <summary>
    /// Create a default moveset if file is not found
    /// </summary>
    private static CharacterMoveSet CreateDefaultMoveSet(string characterName)
    {
        CharacterMoveSet moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
        moveSet.characterName = characterName;
        moveSet.role = "Fighter";
        moveSet.resource = new CharacterResource("Mana", 10, 1);
        moveSet.moves = new List<Move>();
        
        // Physical attack - no mana cost
        Move basicAttack = new Move("basic_attack", "Basic Attack", 
            "A simple physical attack.", 0);
        basicAttack.isPhysical = true;
        basicAttack.damage = 5;
        moveSet.moves.Add(basicAttack);
        
        // Magic attack - costs mana
        Move magicBlast = new Move("magic_blast", "Magic Blast",
            "A magical attack that costs mana.", 3);
        magicBlast.isPhysical = false;
        magicBlast.damage = 8;
        moveSet.moves.Add(magicBlast);
        
        return moveSet;
    }
}
