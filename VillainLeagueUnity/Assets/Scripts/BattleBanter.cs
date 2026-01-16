using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages battle banter between the two main characters (Bellinor and Naice).
/// Displays auto-dismissing dialogue boxes during battle.
/// </summary>
public class BattleBanter : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    public float banterChance = 0.25f; // 25% chance to trigger banter after a move
    public float displayDuration = 3f; // How long the dialogue stays on screen
    
    private BattleUI battleUI;
    private System.Random random = new System.Random();
    
    // Banter categories
    private Dictionary<string, List<string>> bellinorBanter = new Dictionary<string, List<string>>();
    private Dictionary<string, List<string>> naiceBanter = new Dictionary<string, List<string>>();
    
    void Start()
    {
        InitializeBanterLines();
    }
    
    public void Initialize(BattleUI ui)
    {
        battleUI = ui;
        InitializeBanterLines();
    }
    
    void InitializeBanterLines()
    {
        // Bellinor's banter (formal, protective, strategic)
        bellinorBanter["move_comment"] = new List<string>
        {
            "Well executed, Naice.",
            "That was... surprisingly effective.",
            "Impressive form, though a bit flashy.",
            "Your technique improves daily.",
            "I see you've been practicing."
        };
        
        bellinorBanter["playful_insult"] = new List<string>
        {
            "Was that supposed to be intimidating?",
            "A bit theatrical, don't you think?",
            "All style, as usual.",
            "Subtlety is not your forte.",
            "Must you make such a show of it?"
        };
        
        bellinorBanter["check_in"] = new List<string>
        {
            "Stay focused, Naice.",
            "Are you holding up alright?",
            "Keep your guard up.",
            "Don't get reckless now.",
            "Remember your training."
        };
        
        bellinorBanter["low_hp"] = new List<string>
        {
            "Naice, fall back! I'll handle this.",
            "You're hurt. Let me take point.",
            "Don't push yourself too hard."
        };
        
        // Naice's banter (playful, trickster, stylish)
        naiceBanter["move_comment"] = new List<string>
        {
            "Now THAT'S how it's done!",
            "Ooh, nice one Bell!",
            "Didn't know you had that in you!",
            "Textbook perfect, as always.",
            "That's my partner!"
        };
        
        naiceBanter["playful_insult"] = new List<string>
        {
            "Bit slow on that one, weren't you?",
            "Did you even TRY to look cool?",
            "So serious all the time!",
            "Where's the flair, Bell?",
            "You call that a special move?"
        };
        
        naiceBanter["check_in"] = new List<string>
        {
            "Still with me, Bell?",
            "You good over there?",
            "Don't go getting all heroic on me!",
            "Keep it together, partner!",
            "We've got this, right?"
        };
        
        naiceBanter["low_hp"] = new List<string>
        {
            "Bell! You're looking rough!",
            "Hey, don't be a hero now!",
            "Take a breather, I've got this!"
        };
    }
    
    /// <summary>
    /// Attempts to trigger banter after a move is used
    /// </summary>
    public IEnumerator TryTriggerBanter(Character actor, Character partner, string context = "move_comment")
    {
        // Only trigger banter between the two heroes
        if (actor == null || partner == null || !actor.isPlayerCharacter || !partner.isPlayerCharacter)
        {
            yield break;
        }
        
        // Random chance to trigger
        if (random.NextDouble() > banterChance)
        {
            Debug.Log("Banter chance not met, skipping banter.");
            yield break;
        }
        
        // Don't trigger if either character is dead
        if (!actor.IsAlive() || !partner.IsAlive())
        {
            Debug.Log("One of the characters is dead, skipping banter.");
            yield break;
        }
        
        // Determine who speaks (the partner comments on the actor's move)
        string speakerName = partner.characterName;
        string banterLine = GetRandomBanterLine(speakerName, context);
        
        if (!string.IsNullOrEmpty(banterLine))
        {
            Debug.Log($"Triggering banter: {speakerName} says \"{banterLine}\"");
            yield return StartCoroutine(ShowBanterDialogue(speakerName, banterLine));
        }
    }
    
    /// <summary>
    /// Shows banter dialogue that auto-dismisses after displayDuration
    /// </summary>
    IEnumerator ShowBanterDialogue(string speakerName, string dialogue)
    {
        if (battleUI != null)
        {
            // Format the dialogue with speaker name
            string formattedDialogue = $"[{speakerName}] {dialogue}";
            battleUI.ShowBanterDialogue(formattedDialogue, displayDuration);
        }
        
        yield return new WaitForSeconds(displayDuration);
    }
    
    /// <summary>
    /// Gets a random banter line for the specified character and context
    /// </summary>
    string GetRandomBanterLine(string characterName, string context)
    {
        Dictionary<string, List<string>> banterSource = null;
        
        // Determine which character's banter to use
        if (characterName.Contains("Bellinor"))
        {
            banterSource = bellinorBanter;
        }
        else if (characterName.Contains("Naice"))
        {
            banterSource = naiceBanter;
        }
        
        if (banterSource == null || !banterSource.ContainsKey(context))
        {
            return "";
        }
        
        List<string> lines = banterSource[context];
        if (lines.Count == 0)
        {
            return "";
        }
        
        int index = random.Next(lines.Count);
        return lines[index];
    }
    
    /// <summary>
    /// Check if partner should comment on low HP
    /// </summary>
    public IEnumerator CheckLowHPBanter(Character damaged, Character partner)
    {
        if (damaged == null || partner == null || !damaged.isPlayerCharacter || !partner.isPlayerCharacter)
        {
            yield break;
        }
        
        // Check if HP is below 30%
        float hpPercent = (float)damaged.currentHP / damaged.maxHP;
        if (hpPercent < 0.3f && hpPercent > 0)
        {
            // 50% chance to trigger low HP banter
            if (random.NextDouble() < 0.5f)
            {
                yield return StartCoroutine(TryTriggerBanter(damaged, partner, "low_hp"));
            }
        }
    }
}
