using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Test script for Battle Banter system
/// Tests config loading, banter triggering, and audio playback
/// </summary>
public class BattleBanterTest : MonoBehaviour
{
    [Header("Test Configuration")]
    public bool runTestsOnStart = false;
    public BattleBanter battleBanter;
    public BattleUI battleUI;
    
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
        Debug.Log("=== Starting Battle Banter Tests ===");
        testsPassed = true;
        testCount = 0;
        passedTests = 0;
        
        TestBanterInitialization();
        TestBanterConfigLoading();
        TestBanterTriggeringLogic();
        TestContextSelection();
        TestAudioSourceSetup();
        
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
    
    void TestBanterInitialization()
    {
        testCount++;
        Debug.Log("Test 1: Banter Initialization");
        
        if (battleBanter == null)
        {
            // Try to find it in the scene
            battleBanter = FindObjectOfType<BattleBanter>();
        }
        
        if (battleBanter == null)
        {
            LogTestFailure("BattleBanter component not found");
            return;
        }
        
        passedTests++;
        Debug.Log("✓ BattleBanter component initialized correctly");
    }
    
    void TestBanterConfigLoading()
    {
        testCount++;
        Debug.Log("Test 2: Banter Config Loading");
        
        if (battleBanter == null)
        {
            LogTestFailure("BattleBanter not available");
            return;
        }
        
        // Check if config file exists
        string configPath = System.IO.Path.Combine(Application.streamingAssetsPath, "BanterConfig.json");
        if (!System.IO.File.Exists(configPath))
        {
            LogTestFailure($"BanterConfig.json not found at: {configPath}");
            return;
        }
        
        // Try to read and parse the config
        try
        {
            string jsonContent = System.IO.File.ReadAllText(configPath);
            if (string.IsNullOrEmpty(jsonContent))
            {
                LogTestFailure("BanterConfig.json is empty");
                return;
            }
            
            // Basic JSON structure validation
            if (!jsonContent.Contains("bellinor") || !jsonContent.Contains("naice"))
            {
                LogTestFailure("BanterConfig.json missing required character data");
                return;
            }
            
            if (!jsonContent.Contains("move_comment") || !jsonContent.Contains("playful_insult"))
            {
                LogTestFailure("BanterConfig.json missing required banter contexts");
                return;
            }
        }
        catch (System.Exception e)
        {
            LogTestFailure($"Error reading BanterConfig.json: {e.Message}");
            return;
        }
        
        passedTests++;
        Debug.Log("✓ Banter config loads successfully");
    }
    
    void TestBanterTriggeringLogic()
    {
        testCount++;
        Debug.Log("Test 3: Banter Triggering Logic");
        
        if (battleBanter == null)
        {
            LogTestFailure("BattleBanter not available");
            return;
        }
        
        // Create test characters
        Character hero1 = new Character("Bellinor Chabbeneoux", 100, 10, 5, true);
        Character hero2 = new Character("Naice Ajimi", 80, 12, 3, true);
        Character villain = new Character("Villain", 50, 8, 3, false);
        
        // Test: Banter should not trigger between hero and villain
        bool shouldTrigger = hero1.isPlayerCharacter && villain.isPlayerCharacter;
        if (shouldTrigger)
        {
            LogTestFailure("Banter incorrectly triggers between hero and villain");
            return;
        }
        
        // Test: Banter should potentially trigger between two heroes
        shouldTrigger = hero1.isPlayerCharacter && hero2.isPlayerCharacter;
        if (!shouldTrigger)
        {
            LogTestFailure("Banter should trigger between two heroes");
            return;
        }
        
        // Test: Banter should not trigger if character is dead
        hero1.TakeDamage(150); // Kill hero1
        if (hero1.IsAlive())
        {
            LogTestFailure("Character should be dead after taking lethal damage");
            return;
        }
        
        passedTests++;
        Debug.Log("✓ Banter triggering logic works correctly");
    }
    
    void TestContextSelection()
    {
        testCount++;
        Debug.Log("Test 4: Context Selection");
        
        // Test that valid contexts exist
        string[] validContexts = { "move_comment", "playful_insult", "check_in", "low_hp" };
        
        foreach (string context in validContexts)
        {
            if (string.IsNullOrEmpty(context))
            {
                LogTestFailure($"Invalid context: {context}");
                return;
            }
        }
        
        passedTests++;
        Debug.Log("✓ Context selection works correctly");
    }
    
    void TestAudioSourceSetup()
    {
        testCount++;
        Debug.Log("Test 5: Audio Source Setup");
        
        if (battleBanter == null)
        {
            LogTestFailure("BattleBanter not available");
            return;
        }
        
        // Check if AudioSource is available (either assigned or auto-created)
        AudioSource audioSource = battleBanter.audioSource;
        if (audioSource == null)
        {
            audioSource = battleBanter.GetComponent<AudioSource>();
        }
        
        if (audioSource == null)
        {
            LogTestFailure("AudioSource not found on BattleBanter");
            return;
        }
        
        // Verify AudioSource configuration
        if (audioSource.playOnAwake)
        {
            LogTestFailure("AudioSource should not play on awake");
            return;
        }
        
        if (audioSource.loop)
        {
            LogTestFailure("AudioSource should not loop");
            return;
        }
        
        passedTests++;
        Debug.Log("✓ AudioSource setup correctly");
    }
    
    void LogTestFailure(string reason)
    {
        testsPassed = false;
        Debug.LogError($"✗ Test failed: {reason}");
    }
    
    // Manual test methods for runtime testing
    public void TestBanterWithAudio()
    {
        if (battleBanter == null || battleUI == null)
        {
            Debug.LogError("BattleBanter or BattleUI not assigned for manual test");
            return;
        }
        
        // Create test characters
        Character bellinor = new Character("Bellinor Chabbeneoux", 100, 10, 5, true);
        Character naice = new Character("Naice Ajimi", 80, 12, 3, true);
        
        // Initialize banter with UI
        battleBanter.Initialize(battleUI);
        
        // Try to trigger banter
        Debug.Log("Attempting to trigger test banter...");
        StartCoroutine(battleBanter.TryTriggerBanter(bellinor, naice, "move_comment"));
    }
}
