using UnityEngine;

/// <summary>
/// Example script showing how to configure different battle objectives.
/// Attach this to BattleManager or call these methods to set up different battle types.
/// </summary>
public class BattleObjectiveExamples : MonoBehaviour
{
    public BattleManager battleManager;
    
    // Example 1: Default battle - defeat all enemies
    public void SetupStandardBattle()
    {
        battleManager.SetObjectiveDefeatAllEnemies();
        Debug.Log("Standard battle configured: Defeat all enemies!");
    }
    
    // Example 2: Escort mission - protect an NPC
    public void SetupEscortMission()
    {
        // Assuming NPC is already in the player squad
        Character npc = new Character("Princess Elara", 50, 0, 2, true);
        battleManager.playerSquad.Add(npc);
        
        battleManager.SetObjectiveDefendNPC(npc);
        Debug.Log("Escort mission configured: Protect the NPC!");
    }
    
    // Example 3: Training battle - reduce enemies to low HP
    public void SetupTrainingBattle()
    {
        battleManager.SetObjectiveReduceToThreshold(15);
        Debug.Log("Training battle configured: Reduce all enemies to 15 HP or lower!");
    }
    
    // Example 4: Survival mode - last X turns
    public void SetupSurvivalBattle()
    {
        battleManager.SetObjectiveSurviveTurns(20);
        Debug.Log("Survival battle configured: Survive for 20 turns!");
    }
    
    // Example 5: Conservation challenge - finish with mana
    public void SetupConservationBattle()
    {
        battleManager.SetObjectiveFinishWithMana(8);
        Debug.Log("Conservation battle configured: Finish with at least 8 mana!");
    }
    
    // Example 6: Diplomatic mission - charm opponents
    public void SetupDiplomaticMission()
    {
        battleManager.SetObjectiveCharmOpponents(10);
        Debug.Log("Diplomatic mission configured: Charm all opponents!");
        Debug.Log("Note: Make sure character movesets include moves with charmPoints!");
    }
    
    // Example 7: Dark dungeon - limited visibility
    public void SetupDarkDungeonBattle()
    {
        battleManager.SetObjectiveLimitedVisibility(1);
        Debug.Log("Dark dungeon battle configured: Use Vein Vision to see enemies!");
        Debug.Log("Note: Implement visual effects to hide enemies when visibility is disabled");
    }
    
    // Example 8: Mixed objective - NPC escort with turn limit
    public void SetupTimedEscortMission()
    {
        // This example shows you can create custom combinations
        // by setting up the objective manually
        
        Character npc = new Character("Merchant", 40, 0, 1, true);
        battleManager.playerSquad.Add(npc);
        
        // Create custom objective with mixed conditions
        BattleObjective customObjective = new BattleObjective(BattleObjectiveType.DefendNPC);
        customObjective.npcToDefend = npc;
        customObjective.turnsToSurvive = 15;
        customObjective.objectiveDescription = "Protect the merchant for 15 turns!";
        
        battleManager.battleObjective = customObjective;
        Debug.Log("Timed escort mission configured!");
    }
    
    // Example 9: Boss battle with mana conservation
    public void SetupBossBattleWithManaConstraint()
    {
        // For battles where you need resources for the next fight
        battleManager.SetObjectiveFinishWithMana(10);
        Debug.Log("Boss battle with mana constraint configured!");
        Debug.Log("Save your mana for the next battle!");
    }
    
    // Example 10: Charm-based pacifist run
    public void SetupPacifistChallenge()
    {
        // Lower charm requirement for easier "talk no jutsu" victories
        battleManager.SetObjectiveCharmOpponents(5);
        Debug.Log("Pacifist challenge configured: Win without violence!");
        
        // Note: You would want to create special charm-focused movesets
        // with moves that have charmPoints values
    }
}
