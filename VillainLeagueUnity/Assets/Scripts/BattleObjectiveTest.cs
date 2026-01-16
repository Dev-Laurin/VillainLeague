using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Test script for Battle Objective system
/// Tests different objective types and their win/lose conditions
/// </summary>
public class BattleObjectiveTest : MonoBehaviour
{
    [Header("Test Configuration")]
    public bool runTestsOnStart = false;
    
    private bool testsPassed = true;
    private int testCount = 0;
    private int passedTests = 0;
    
    void Start()
    {
        if (runTestsOnStart)
        {
            RunAllTests();
        }
    }
    
    public void RunAllTests()
    {
        Debug.Log("=== Starting Battle Objective Tests ===");
        testsPassed = true;
        testCount = 0;
        passedTests = 0;
        
        TestDefeatAllEnemiesObjective();
        TestDefendNPCObjective();
        TestReduceToThresholdObjective();
        TestSurviveTurnsObjective();
        TestFinishWithManaObjective();
        TestCharmOpponentsObjective();
        TestLimitedVisibilityObjective();
        
        Debug.Log($"=== Test Results: {passedTests}/{testCount} passed ===");
        if (testsPassed)
        {
            Debug.Log("✓ All tests passed!");
        }
        else
        {
            Debug.LogError("✗ Some tests failed!");
        }
    }
    
    void TestDefeatAllEnemiesObjective()
    {
        testCount++;
        Debug.Log("Test: Defeat All Enemies Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.DefeatAllEnemies);
        
        // Create test characters
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true)
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy1", 50, 8, 3, false),
            new Character("Enemy2", 60, 9, 4, false)
        };
        
        // Test: Not complete when enemies alive
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Objective should not be complete with alive enemies");
            testsPassed = false;
            return;
        }
        
        // Test: Complete when all enemies defeated
        enemySquad[0].TakeDamage(100);
        enemySquad[1].TakeDamage(100);
        
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Objective should be complete when all enemies defeated");
            testsPassed = false;
            return;
        }
        
        // Test: Failed when all players defeated
        playerSquad[0].TakeDamage(150);
        if (!objective.IsObjectiveFailed(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Objective should fail when all players defeated");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Defeat All Enemies Objective test passed");
        passedTests++;
    }
    
    void TestDefendNPCObjective()
    {
        testCount++;
        Debug.Log("Test: Defend NPC Objective");
        
        Character npc = new Character("NPC", 50, 0, 2, true);
        BattleObjective objective = new BattleObjective(BattleObjectiveType.DefendNPC);
        objective.npcToDefend = npc;
        
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true),
            npc
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy", 50, 8, 3, false)
        };
        
        // Test: Not complete with alive enemies
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ NPC objective should not be complete with alive enemies");
            testsPassed = false;
            return;
        }
        
        // Test: Failed when NPC dies
        npc.TakeDamage(100);
        if (!objective.IsObjectiveFailed(playerSquad, enemySquad))
        {
            Debug.LogError("✗ NPC objective should fail when NPC dies");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Defend NPC Objective test passed");
        passedTests++;
    }
    
    void TestReduceToThresholdObjective()
    {
        testCount++;
        Debug.Log("Test: Reduce to Threshold Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.ReduceToThreshold);
        objective.hpThreshold = 10;
        
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true)
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy1", 50, 8, 3, false),
            new Character("Enemy2", 60, 9, 4, false)
        };
        
        // Test: Not complete when enemies above threshold
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Threshold objective should not be complete with enemies above threshold");
            testsPassed = false;
            return;
        }
        
        // Test: Complete when all enemies at or below threshold
        enemySquad[0].TakeDamage(45); // Reduce to 5 HP
        enemySquad[1].TakeDamage(50); // Reduce to 10 HP
        
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Threshold objective should be complete when all enemies below threshold");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Reduce to Threshold Objective test passed");
        passedTests++;
    }
    
    void TestSurviveTurnsObjective()
    {
        testCount++;
        Debug.Log("Test: Survive Turns Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.SurviveTurns);
        objective.turnsToSurvive = 10;
        
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true)
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy", 50, 8, 3, false)
        };
        
        // Test: Not complete at start
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Survive objective should not be complete at turn 0");
            testsPassed = false;
            return;
        }
        
        // Test: Complete after required turns
        for (int i = 0; i < 10; i++)
        {
            objective.IncrementTurn();
        }
        
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Survive objective should be complete after 10 turns");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Survive Turns Objective test passed");
        passedTests++;
    }
    
    void TestFinishWithManaObjective()
    {
        testCount++;
        Debug.Log("Test: Finish With Mana Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.FinishWithMana);
        objective.manaThresholdRequired = 5;
        
        Character hero = new Character("Hero", 100, 10, 5, true);
        hero.moveSet = ScriptableObject.CreateInstance<CharacterMoveSet>();
        hero.moveSet.resource = new CharacterResource("Mana", 10, 1);
        hero.moveSet.resource.currentResource = 6; // Start with 6 mana
        
        List<Character> playerSquad = new List<Character> { hero };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy", 50, 8, 3, false)
        };
        
        // Test: Not complete with alive enemies
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Mana objective should not be complete with alive enemies");
            testsPassed = false;
            return;
        }
        
        // Test: Complete when enemies defeated and mana >= threshold
        enemySquad[0].TakeDamage(100);
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Mana objective should be complete with enough mana");
            testsPassed = false;
            return;
        }
        
        // Test: Failed when enemies defeated but mana too low
        hero.moveSet.resource.currentResource = 3;
        if (!objective.IsObjectiveFailed(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Mana objective should fail with insufficient mana");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Finish With Mana Objective test passed");
        passedTests++;
    }
    
    void TestCharmOpponentsObjective()
    {
        testCount++;
        Debug.Log("Test: Charm Opponents Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.CharmOpponents);
        objective.charmPointsRequired = 10;
        
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true)
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy1", 50, 8, 3, false),
            new Character("Enemy2", 60, 9, 4, false)
        };
        
        // Initialize charm points
        objective.charmPoints[enemySquad[0]] = 0;
        objective.charmPoints[enemySquad[1]] = 0;
        
        // Test: Not complete at start
        if (objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Charm objective should not be complete with no charm points");
            testsPassed = false;
            return;
        }
        
        // Test: Complete when all enemies charmed
        objective.AddCharmPoints(enemySquad[0], 10);
        objective.AddCharmPoints(enemySquad[1], 10);
        
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Charm objective should be complete when all enemies charmed");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Charm Opponents Objective test passed");
        passedTests++;
    }
    
    void TestLimitedVisibilityObjective()
    {
        testCount++;
        Debug.Log("Test: Limited Visibility Objective");
        
        BattleObjective objective = new BattleObjective(BattleObjectiveType.LimitedVisibility);
        objective.visibilityEnabled = false;
        objective.veinVisionManaCost = 1;
        
        List<Character> playerSquad = new List<Character>
        {
            new Character("Hero", 100, 10, 5, true)
        };
        
        List<Character> enemySquad = new List<Character>
        {
            new Character("Enemy", 50, 8, 3, false)
        };
        
        // Test: Visibility disabled
        if (objective.visibilityEnabled)
        {
            Debug.LogError("✗ Visibility should be disabled for limited visibility objective");
            testsPassed = false;
            return;
        }
        
        // Test: Complete when enemies defeated (same as normal battle)
        enemySquad[0].TakeDamage(100);
        if (!objective.IsObjectiveComplete(playerSquad, enemySquad))
        {
            Debug.LogError("✗ Limited visibility objective should complete when enemies defeated");
            testsPassed = false;
            return;
        }
        
        Debug.Log("✓ Limited Visibility Objective test passed");
        passedTests++;
    }
}
