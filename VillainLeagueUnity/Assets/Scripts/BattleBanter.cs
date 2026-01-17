using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// Manages battle banter between the two main characters (Bellinor and Naice).
/// Displays auto-dismissing dialogue boxes during battle and plays audio files.
/// </summary>
public class BattleBanter : MonoBehaviour
{
    [Header("Settings")]
    [Range(0f, 1f)]
    public float banterChance = 0.25f; // 25% chance to trigger banter after a move
    public float displayDuration = 3f; // How long the dialogue stays on screen
    
    [Header("Audio")]
    public AudioSource audioSource; // AudioSource for playing banter audio
    
    private BattleUI battleUI;
    private System.Random random = new System.Random();
    
    // Banter data structure
    [System.Serializable]
    public class BanterLine
    {
        public string text;
        public string audioFile;
    }
    
    // Banter categories - now stores BanterLine objects instead of just strings
    private Dictionary<string, List<BanterLine>> bellinorBanter = new Dictionary<string, List<BanterLine>>();
    private Dictionary<string, List<BanterLine>> naiceBanter = new Dictionary<string, List<BanterLine>>();
    
    // Cache for loaded audio clips
    private Dictionary<string, AudioClip> audioCache = new Dictionary<string, AudioClip>();
    
    void Start()
    {
        InitializeBanterLines();
        SetupAudioSource();
    }
    
    public void Initialize(BattleUI ui)
    {
        battleUI = ui;
        InitializeBanterLines();
        SetupAudioSource();
    }
    
    void SetupAudioSource()
    {
        // Create AudioSource if not assigned
        if (audioSource == null)
        {
            audioSource = gameObject.GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        // Configure AudioSource
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }
    
    void InitializeBanterLines()
    {
        // Load banter data from JSON config file
        string configPath = Path.Combine(Application.streamingAssetsPath, "BanterConfig.json");
        
        if (!File.Exists(configPath))
        {
            Debug.LogError($"Banter config file not found at: {configPath}");
            return;
        }
        
        try
        {
            string jsonContent = File.ReadAllText(configPath);
            BanterConfigWrapper wrapper = JsonUtility.FromJson<BanterConfigWrapper>(jsonContent);
            
            if (wrapper == null)
            {
                Debug.LogError("Failed to parse banter config or config is empty");
                return;
            }
            
            // Load Bellinor's banter
            if (wrapper.bellinor != null)
            {
                LoadCharacterBanter(wrapper.bellinor, bellinorBanter);
            }
            
            // Load Naice's banter
            if (wrapper.naice != null)
            {
                LoadCharacterBanter(wrapper.naice, naiceBanter);
            }
            
            Debug.Log("Banter config loaded successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error loading banter config: {e.Message}");
        }
    }
    
    void LoadCharacterBanter(CharacterBanterData characterData, Dictionary<string, List<BanterLine>> targetDict)
    {
        LoadBanterContext("move_comment", characterData.move_comment, targetDict);
        LoadBanterContext("playful_insult", characterData.playful_insult, targetDict);
        LoadBanterContext("check_in", characterData.check_in, targetDict);
        LoadBanterContext("low_hp", characterData.low_hp, targetDict);
    }
    
    /// <summary>
    /// Helper method to load a specific banter context into the dictionary
    /// </summary>
    void LoadBanterContext(string contextKey, BanterLine[] lines, Dictionary<string, List<BanterLine>> targetDict)
    {
        if (lines != null && lines.Length > 0)
        {
            targetDict[contextKey] = new List<BanterLine>(lines);
        }
    }
    
    // JSON data structures for deserialization (Unity JsonUtility compatible)
    [System.Serializable]
    private class BanterConfigWrapper
    {
        public CharacterBanterData bellinor;
        public CharacterBanterData naice;
    }
    
    [System.Serializable]
    private class CharacterBanterData
    {
        public BanterLine[] move_comment;
        public BanterLine[] playful_insult;
        public BanterLine[] check_in;
        public BanterLine[] low_hp;
    }
    
    /// <summary>
    /// Attempts to trigger banter after a move is used
    /// </summary>
    public IEnumerator TryTriggerBanter(Character actor, Character partner, string context = "move_comment")
    {
        Debug.Log($"try trigger banter {actor.characterName} -> {partner.characterName} [{context}]");
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
        BanterLine banterLine = GetRandomBanterLine(speakerName, context);
        
        if (banterLine != null && !string.IsNullOrEmpty(banterLine.text))
        {
            Debug.Log($"Triggering banter: {speakerName} says \"{banterLine.text}\"");
            yield return StartCoroutine(ShowBanterDialogue(speakerName, banterLine));
        }
    }
    
    /// <summary>
    /// Shows banter dialogue that auto-dismisses after displayDuration and plays audio
    /// </summary>
    IEnumerator ShowBanterDialogue(string speakerName, BanterLine banterLine)
    {
        if (battleUI != null && banterLine != null)
        {
            // Format the dialogue with speaker name
            string formattedDialogue = $"[{speakerName}] {banterLine.text}";
            battleUI.ShowBanterDialogue(formattedDialogue, displayDuration);
            
            // Play audio if available
            if (!string.IsNullOrEmpty(banterLine.audioFile))
            {
                yield return StartCoroutine(PlayBanterAudio(banterLine.audioFile));
            }
        }
        
        yield return new WaitForSeconds(displayDuration);
    }
    
    /// <summary>
    /// Loads and plays an audio file from StreamingAssets/Audio/
    /// </summary>
    IEnumerator PlayBanterAudio(string audioFileName)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource is not assigned, cannot play banter audio");
            yield break;
        }
        
        // Check cache first
        if (audioCache.ContainsKey(audioFileName))
        {
            AudioClip clip = audioCache[audioFileName];
            if (clip != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                yield break;
            }
        }
        
        // Load audio file from StreamingAssets/Audio/
        string audioPath = Path.Combine(Application.streamingAssetsPath, "Audio", audioFileName);
        
        // Determine audio type from file extension
        AudioType audioType = GetAudioType(audioFileName);
        
        // Use UnityWebRequest for cross-platform compatibility
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + audioPath, audioType))
        {
            yield return www.SendWebRequest();
            
            // Check for various error conditions
            if (www.result == UnityWebRequest.Result.Success)
            {
                AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
                if (clip != null)
                {
                    // Cache the clip
                    audioCache[audioFileName] = clip;
                    
                    // Play the audio
                    audioSource.clip = clip;
                    audioSource.Play();
                    Debug.Log($"Playing banter audio: {audioFileName}");
                }
                else
                {
                    Debug.LogWarning($"Failed to load audio content from: {audioPath}");
                }
            }
            else if (www.result == UnityWebRequest.Result.ConnectionError || 
                     www.result == UnityWebRequest.Result.ProtocolError ||
                     www.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogWarning($"Audio file not found or failed to load: {audioPath}. Error: {www.error}. Banter will play without audio.");
            }
        }
    }
    
    /// <summary>
    /// Determines the audio type based on file extension
    /// </summary>
    AudioType GetAudioType(string fileName)
    {
        string extension = Path.GetExtension(fileName).ToLower();
        switch (extension)
        {
            case ".wav":
                return AudioType.WAV;
            case ".mp3":
                return AudioType.MPEG;
            case ".ogg":
                return AudioType.OGGVORBIS;
            default:
                Debug.LogWarning($"Unknown audio format: {extension}, defaulting to WAV");
                return AudioType.WAV;
        }
    }
    
    /// <summary>
    /// Gets a random banter line for the specified character and context
    /// </summary>
    BanterLine GetRandomBanterLine(string characterName, string context)
    {
        Dictionary<string, List<BanterLine>> banterSource = null;
        
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
            return null;
        }
        
        List<BanterLine> lines = banterSource[context];
        if (lines.Count == 0)
        {
            return null;
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
