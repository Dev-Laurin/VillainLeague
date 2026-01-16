using UnityEngine;
using System.Collections.Generic;

public enum BattleObjectiveType
{
    DefeatAllEnemies,      // Default: eliminate all enemies
    DefendNPC,             // Protect an NPC from being defeated
    ReduceToThreshold,     // Reduce all enemies to specific HP or lower
    SurviveTurns,          // Survive for a certain number of turns
    FinishWithMana,        // Complete battle with specific mana threshold
    CharmOpponents,        // Charm opponents through secondary moves
    LimitedVisibility      // Limited visibility requiring vein vision
}

[System.Serializable]
public class BattleObjective
{
    public BattleObjectiveType objectiveType = BattleObjectiveType.DefeatAllEnemies;
    public string objectiveDescription;
    
    // NPC defense parameters
    public Character npcToDefend;
    
    // HP threshold parameters
    public int hpThreshold = 10; // HP to reduce enemies to
    
    // Turn survival parameters
    public int turnsToSurvive = 10;
    public int currentTurnCount = 0;
    
    // Mana threshold parameters
    public int manaThresholdRequired = 5;
    
    // Charm parameters
    public int charmPointsRequired = 10;
    public Dictionary<Character, int> charmPoints = new Dictionary<Character, int>();
    
    // Visibility parameters
    public bool visibilityEnabled = true; // When false, need vein vision
    public int veinVisionManaCost = 1;
    
    public BattleObjective(BattleObjectiveType type)
    {
        objectiveType = type;
        SetDefaultDescription();
    }
    
    private void SetDefaultDescription()
    {
        switch (objectiveType)
        {
            case BattleObjectiveType.DefeatAllEnemies:
                objectiveDescription = "Defeat all enemies";
                break;
            case BattleObjectiveType.DefendNPC:
                objectiveDescription = "Protect the NPC";
                break;
            case BattleObjectiveType.ReduceToThreshold:
                objectiveDescription = $"Reduce all enemies to {hpThreshold} HP or lower";
                break;
            case BattleObjectiveType.SurviveTurns:
                objectiveDescription = $"Survive for {turnsToSurvive} turns";
                break;
            case BattleObjectiveType.FinishWithMana:
                objectiveDescription = $"Finish with at least {manaThresholdRequired} mana";
                break;
            case BattleObjectiveType.CharmOpponents:
                objectiveDescription = $"Charm all opponents ({charmPointsRequired} points each)";
                break;
            case BattleObjectiveType.LimitedVisibility:
                objectiveDescription = "Survive in darkness (use vein vision to see)";
                break;
        }
    }
    
    public void UpdateDescription()
    {
        SetDefaultDescription();
    }
    
    // Check if objective is complete
    public bool IsObjectiveComplete(List<Character> playerSquad, List<Character> enemySquad)
    {
        switch (objectiveType)
        {
            case BattleObjectiveType.DefeatAllEnemies:
                return CheckAllEnemiesDefeated(enemySquad);
                
            case BattleObjectiveType.DefendNPC:
                // Success if NPC is still alive and all enemies defeated
                return npcToDefend != null && npcToDefend.IsAlive() && CheckAllEnemiesDefeated(enemySquad);
                
            case BattleObjectiveType.ReduceToThreshold:
                return CheckAllEnemiesBelowThreshold(enemySquad);
                
            case BattleObjectiveType.SurviveTurns:
                return currentTurnCount >= turnsToSurvive;
                
            case BattleObjectiveType.FinishWithMana:
                // Only check when battle is otherwise complete
                return CheckAllEnemiesDefeated(enemySquad) && CheckManaThreshold(playerSquad);
                
            case BattleObjectiveType.CharmOpponents:
                return CheckAllEnemiesCharmed(enemySquad);
                
            case BattleObjectiveType.LimitedVisibility:
                // Same as defeat all enemies, but with visibility mechanics
                return CheckAllEnemiesDefeated(enemySquad);
                
            default:
                return false;
        }
    }
    
    // Check if objective has failed
    public bool IsObjectiveFailed(List<Character> playerSquad, List<Character> enemySquad)
    {
        // Common failure: all players defeated
        if (!HasAlivePlayers(playerSquad))
        {
            return true;
        }
        
        switch (objectiveType)
        {
            case BattleObjectiveType.DefendNPC:
                // Fail if NPC dies
                return npcToDefend != null && !npcToDefend.IsAlive();
                
            case BattleObjectiveType.FinishWithMana:
                // Fail if enemies defeated but mana too low
                if (CheckAllEnemiesDefeated(enemySquad) && !CheckManaThreshold(playerSquad))
                {
                    return true;
                }
                break;
        }
        
        return false;
    }
    
    private bool CheckAllEnemiesDefeated(List<Character> enemySquad)
    {
        foreach (Character enemy in enemySquad)
        {
            if (enemy.IsAlive())
                return false;
        }
        return true;
    }
    
    private bool CheckAllEnemiesBelowThreshold(List<Character> enemySquad)
    {
        foreach (Character enemy in enemySquad)
        {
            if (enemy.IsAlive() && enemy.currentHP > hpThreshold)
                return false;
        }
        return true;
    }
    
    private bool CheckManaThreshold(List<Character> playerSquad)
    {
        int totalMana = 0;
        foreach (Character player in playerSquad)
        {
            if (player.IsAlive() && player.moveSet != null && player.moveSet.resource != null)
            {
                totalMana += player.moveSet.resource.currentResource;
            }
        }
        return totalMana >= manaThresholdRequired;
    }
    
    private bool CheckAllEnemiesCharmed(List<Character> enemySquad)
    {
        foreach (Character enemy in enemySquad)
        {
            if (!charmPoints.ContainsKey(enemy) || charmPoints[enemy] < charmPointsRequired)
                return false;
        }
        return true;
    }
    
    private bool HasAlivePlayers(List<Character> playerSquad)
    {
        foreach (Character player in playerSquad)
        {
            if (player.IsAlive())
                return true;
        }
        return false;
    }
    
    // Add charm points to an enemy
    public void AddCharmPoints(Character enemy, int points)
    {
        if (!charmPoints.ContainsKey(enemy))
        {
            charmPoints[enemy] = 0;
        }
        charmPoints[enemy] += points;
    }
    
    // Increment turn counter
    public void IncrementTurn()
    {
        currentTurnCount++;
    }
}
