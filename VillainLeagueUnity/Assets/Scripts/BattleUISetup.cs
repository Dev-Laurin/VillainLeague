using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Automatically sets up the battle UI at runtime
/// Creates all necessary UI elements for the turn-based battle system
/// </summary>
public class BattleUISetup : MonoBehaviour
{
    private BattleUI battleUI;
    private Canvas canvas;

    void Awake()
    {
        SetupUI();
    }

    void SetupUI()
    {
        // Create Canvas
        GameObject canvasObj = new GameObject("Canvas");
        canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        
        canvasObj.AddComponent<GraphicRaycaster>();

        // Create EventSystem if it doesn't exist
        if (FindObjectOfType<UnityEngine.EventSystems.EventSystem>() == null)
        {
            GameObject eventSystemObj = new GameObject("EventSystem");
            eventSystemObj.AddComponent<UnityEngine.EventSystems.EventSystem>();
            eventSystemObj.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
        }

        // Create BattleUI component on this GameObject
        battleUI = gameObject.AddComponent<BattleUI>();

        // Create Turn Order Display (Top Center)
        battleUI.turnText = CreateText("TurnText", canvas.transform, new Vector2(0, 400), new Vector2(600, 60), 
            "Turn: Ready", 36, TextAlignmentOptions.Center);

        // Create Next Turn Display (Below Turn Text)
        battleUI.nextTurnText = CreateText("NextTurnText", canvas.transform, new Vector2(0, 350), new Vector2(600, 40), 
            "Next: ---", 24, TextAlignmentOptions.Center);
        battleUI.nextTurnText.color = new Color(0.7f, 0.7f, 0.7f);

        // Create Message Display (Center)
        battleUI.messageText = CreateText("MessageText", canvas.transform, new Vector2(0, 0), new Vector2(800, 60), 
            "Battle Start!", 28, TextAlignmentOptions.Center);

        // Setup Player Squad UI (Left Side)
        SetupPlayerUI(canvas.transform);

        // Setup Enemy Squad UI (Right Side)
        SetupEnemyUI(canvas.transform);

        // Setup Action Buttons (Bottom Center)
        SetupActionButtons(canvas.transform);

        // Setup Target Selection Panel (Center, hidden initially)
        SetupTargetSelectionPanel(canvas.transform);
        
        // Setup Banter Dialogue Panel (Bottom Center, hidden initially)
        SetupBanterDialoguePanel(canvas.transform);

        // Wire up BattleUI to BattleManager
        BattleManager battleManager = FindObjectOfType<BattleManager>();
        if (battleManager != null)
        {
            battleManager.battleUI = battleUI;
            
            // Create and wire up BattleBanter component
            BattleBanter battleBanter = battleManager.gameObject.AddComponent<BattleBanter>();
            battleManager.battleBanter = battleBanter;
        }

        Debug.Log("Battle UI setup complete!");
    }

    void SetupPlayerUI(Transform parent)
    {
        // Player Panel Background
        GameObject playerPanel = CreatePanel("PlayerPanel", parent, new Vector2(-600, 250), new Vector2(400, 400));
        
        // Hero 1 (Bellinor)
        battleUI.player1NameText = CreateText("Player1Name", playerPanel.transform, 
            new Vector2(0, 160), new Vector2(380, 50), "Hero 1", 32, TextAlignmentOptions.Left);
        battleUI.player1NameText.color = new Color(0.2f, 0.8f, 0.2f);
        
        battleUI.player1HPText = CreateText("Player1HP", playerPanel.transform, 
            new Vector2(0, 120), new Vector2(380, 40), "120/120", 24, TextAlignmentOptions.Left);
        
        battleUI.player1HPBar = CreateSlider("Player1HPBar", playerPanel.transform, 
            new Vector2(0, 80), new Vector2(380, 30), 120, 120, new Color(0.2f, 0.8f, 0.2f));
        
        battleUI.player1ResourceText = CreateText("Player1Resource", playerPanel.transform,
            new Vector2(0, 45), new Vector2(380, 30), "Resolve: 6/6", 20, TextAlignmentOptions.Left);
        battleUI.player1ResourceText.color = new Color(0.7f, 0.9f, 1f);
        
        battleUI.player1SecondaryResourceText = CreateText("Player1SecondaryResource", playerPanel.transform,
            new Vector2(0, 15), new Vector2(380, 30), "Resolve: 0/6", 18, TextAlignmentOptions.Left);
        battleUI.player1SecondaryResourceText.color = new Color(1f, 0.8f, 0.3f);

        // Hero 2 (Naice)
        battleUI.player2NameText = CreateText("Player2Name", playerPanel.transform, 
            new Vector2(0, -40), new Vector2(380, 50), "Hero 2", 32, TextAlignmentOptions.Left);
        battleUI.player2NameText.color = new Color(0.2f, 0.8f, 0.2f);
        
        battleUI.player2HPText = CreateText("Player2HP", playerPanel.transform, 
            new Vector2(0, -80), new Vector2(380, 40), "80/80", 24, TextAlignmentOptions.Left);
        
        battleUI.player2HPBar = CreateSlider("Player2HPBar", playerPanel.transform, 
            new Vector2(0, -120), new Vector2(380, 30), 80, 80, new Color(0.2f, 0.8f, 0.2f));
        
        battleUI.player2ResourceText = CreateText("Player2Resource", playerPanel.transform,
            new Vector2(0, -155), new Vector2(380, 30), "Mana: 10/10", 20, TextAlignmentOptions.Left);
        battleUI.player2ResourceText.color = new Color(0.7f, 0.9f, 1f);
        
        battleUI.player2SecondaryResourceText = CreateText("Player2SecondaryResource", playerPanel.transform,
            new Vector2(0, -185), new Vector2(380, 30), "Style: 0/6", 18, TextAlignmentOptions.Left);
        battleUI.player2SecondaryResourceText.color = new Color(1f, 0.8f, 0.3f);
    }

    void SetupEnemyUI(Transform parent)
    {
        // Enemy Panel Background
        GameObject enemyPanel = CreatePanel("EnemyPanel", parent, new Vector2(600, 250), new Vector2(400, 300));
        
        // Villain 1
        battleUI.enemy1NameText = CreateText("Enemy1Name", enemyPanel.transform, 
            new Vector2(0, 100), new Vector2(380, 50), "Villain 1", 32, TextAlignmentOptions.Right);
        battleUI.enemy1NameText.color = new Color(0.9f, 0.2f, 0.2f);
        
        battleUI.enemy1HPText = CreateText("Enemy1HP", enemyPanel.transform, 
            new Vector2(0, 60), new Vector2(380, 40), "70/70", 24, TextAlignmentOptions.Right);
        
        battleUI.enemy1HPBar = CreateSlider("Enemy1HPBar", enemyPanel.transform, 
            new Vector2(0, 20), new Vector2(380, 30), 70, 70, new Color(0.9f, 0.2f, 0.2f));

        // Villain 2
        battleUI.enemy2NameText = CreateText("Enemy2Name", enemyPanel.transform, 
            new Vector2(0, -40), new Vector2(380, 50), "Villain 2", 32, TextAlignmentOptions.Right);
        battleUI.enemy2NameText.color = new Color(0.9f, 0.2f, 0.2f);
        
        battleUI.enemy2HPText = CreateText("Enemy2HP", enemyPanel.transform, 
            new Vector2(0, -80), new Vector2(380, 40), "90/90", 24, TextAlignmentOptions.Right);
        
        battleUI.enemy2HPBar = CreateSlider("Enemy2HPBar", enemyPanel.transform, 
            new Vector2(0, -120), new Vector2(380, 30), 90, 90, new Color(0.9f, 0.2f, 0.2f));
    }

    void SetupActionButtons(Transform parent)
    {
        // Create a move selection panel
        GameObject movePanel = CreatePanel("MoveSelectionPanel", parent, new Vector2(0, -200), new Vector2(750, 450));
        movePanel.SetActive(false);
        battleUI.moveSelectionPanel = movePanel;
        
        // Panel title
        CreateText("MoveTitle", movePanel.transform, new Vector2(0, 190), new Vector2(700, 50),
            "SELECT MOVE", 32, TextAlignmentOptions.Center);
        
        // Create a scroll view container for moves
        GameObject scrollContent = new GameObject("MoveButtonContainer");
        scrollContent.transform.SetParent(movePanel.transform, false);
        RectTransform scrollRect = scrollContent.AddComponent<RectTransform>();
        scrollRect.anchoredPosition = new Vector2(0, -10);
        scrollRect.sizeDelta = new Vector2(700, 350);
        
        // Add Vertical Layout Group for automatic positioning
        VerticalLayoutGroup layoutGroup = scrollContent.AddComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = 10;
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = false;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.childForceExpandHeight = false;
        
        battleUI.moveButtonContainer = scrollContent.transform;
        
        // Keep original action buttons (will be hidden when move system is active)
        battleUI.attackButton = CreateButton("AttackButton", parent, 
            new Vector2(-280, -380), new Vector2(240, 80), "NORMAL TURN", OnAttackButtonSetup);
        
        battleUI.defendButton = CreateButton("DefendButton", parent, 
            new Vector2(0, -380), new Vector2(240, 80), "DEFEND", OnDefendButtonSetup);
        
        battleUI.specialButton = CreateButton("SpecialButton", parent, 
            new Vector2(280, -380), new Vector2(240, 80), "SPECIAL", OnSpecialButtonSetup);
        
        // Super buttons (initially hidden)
        battleUI.superButton = CreateButton("SuperButton", parent,
            new Vector2(-150, -380), new Vector2(280, 80), "USE SUPER", OnSuperButtonSetup);
        battleUI.superButton.gameObject.SetActive(false);
        
        battleUI.teamSuperButton = CreateButton("TeamSuperButton", parent,
            new Vector2(150, -380), new Vector2(280, 80), "TEAM SUPER", OnTeamSuperButtonSetup);
        battleUI.teamSuperButton.gameObject.SetActive(false);
    }

    void SetupTargetSelectionPanel(Transform parent)
    {
        // Target Selection Panel (initially hidden)
        GameObject targetPanel = CreatePanel("TargetSelectionPanel", parent, new Vector2(0, 0), new Vector2(500, 200));
        targetPanel.SetActive(false);
        battleUI.targetSelectionPanel = targetPanel;

        // Panel title
        CreateText("TargetTitle", targetPanel.transform, new Vector2(0, 60), new Vector2(450, 50), 
            "SELECT TARGET", 28, TextAlignmentOptions.Center);

        // Target buttons
        battleUI.target1Button = CreateButton("Target1Button", targetPanel.transform, 
            new Vector2(0, -10), new Vector2(400, 70), "Target 1", OnTargetButtonSetup);
        
        battleUI.target2Button = CreateButton("Target2Button", targetPanel.transform, 
            new Vector2(0, -90), new Vector2(400, 70), "Target 2", OnTargetButtonSetup);
    }
    
    void SetupBanterDialoguePanel(Transform parent)
    {
        // Banter Dialogue Panel (bottom center, initially hidden)
        GameObject banterPanel = CreatePanel("BanterDialoguePanel", parent, new Vector2(0, -350), new Vector2(700, 120));
        banterPanel.SetActive(false);
        battleUI.banterDialoguePanel = banterPanel;
        
        // Change panel color to be more distinct (darker with slight transparency)
        Image panelImage = banterPanel.GetComponent<Image>();
        if (panelImage != null)
        {
            panelImage.color = new Color(0.15f, 0.15f, 0.2f, 0.9f);
        }
        
        // Banter text
        battleUI.banterDialogueText = CreateText("BanterText", banterPanel.transform, 
            new Vector2(0, 0), new Vector2(680, 100), 
            "", 22, TextAlignmentOptions.Center);
        
        // Add slight yellow tint to banter text to make it stand out
        battleUI.banterDialogueText.color = new Color(1f, 1f, 0.8f);
    }

    // Helper methods to create UI elements
    GameObject CreatePanel(string name, Transform parent, Vector2 position, Vector2 size)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        
        RectTransform rectTransform = panel.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        
        Image image = panel.AddComponent<Image>();
        image.color = new Color(0.1f, 0.1f, 0.1f, 0.8f);
        
        return panel;
    }

    TextMeshProUGUI CreateText(string name, Transform parent, Vector2 position, Vector2 size, 
        string text, int fontSize, TextAlignmentOptions alignment)
    {
        GameObject textObj = new GameObject(name);
        textObj.transform.SetParent(parent, false);
        
        RectTransform rectTransform = textObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.fontSize = fontSize;
        textComponent.alignment = alignment;
        textComponent.color = Color.white;
        
        return textComponent;
    }

    Slider CreateSlider(string name, Transform parent, Vector2 position, Vector2 size, 
        float maxValue, float currentValue, Color fillColor)
    {
        GameObject sliderObj = new GameObject(name);
        sliderObj.transform.SetParent(parent, false);
        
        RectTransform rectTransform = sliderObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        
        Slider slider = sliderObj.AddComponent<Slider>();
        slider.maxValue = maxValue;
        slider.value = currentValue;
        
        // Background
        GameObject background = new GameObject("Background");
        background.transform.SetParent(sliderObj.transform, false);
        RectTransform bgRect = background.AddComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0, 0.25f);
        bgRect.anchorMax = new Vector2(1, 0.75f);
        bgRect.sizeDelta = Vector2.zero;
        Image bgImage = background.AddComponent<Image>();
        bgImage.color = new Color(0.2f, 0.2f, 0.2f);
        
        // Fill Area
        GameObject fillArea = new GameObject("Fill Area");
        fillArea.transform.SetParent(sliderObj.transform, false);
        RectTransform fillAreaRect = fillArea.AddComponent<RectTransform>();
        fillAreaRect.anchorMin = new Vector2(0, 0.25f);
        fillAreaRect.anchorMax = new Vector2(1, 0.75f);
        fillAreaRect.sizeDelta = Vector2.zero;
        
        // Fill
        GameObject fill = new GameObject("Fill");
        fill.transform.SetParent(fillArea.transform, false);
        RectTransform fillRect = fill.AddComponent<RectTransform>();
        fillRect.sizeDelta = Vector2.zero;
        Image fillImage = fill.AddComponent<Image>();
        fillImage.color = fillColor;
        
        slider.fillRect = fillRect;
        slider.targetGraphic = fillImage;
        
        return slider;
    }

    Button CreateButton(string name, Transform parent, Vector2 position, Vector2 size, 
        string text, System.Action<Button> onSetup)
    {
        GameObject buttonObj = new GameObject(name);
        buttonObj.transform.SetParent(parent, false);
        
        RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
        rectTransform.anchoredPosition = position;
        rectTransform.sizeDelta = size;
        
        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.3f, 0.3f, 0.3f);
        
        Button button = buttonObj.AddComponent<Button>();
        
        // Button colors
        ColorBlock colors = button.colors;
        colors.normalColor = new Color(0.3f, 0.3f, 0.3f);
        colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f);
        colors.pressedColor = new Color(0.2f, 0.2f, 0.2f);
        button.colors = colors;
        
        // Button text
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        
        RectTransform textRect = textObj.AddComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI textComponent = textObj.AddComponent<TextMeshProUGUI>();
        textComponent.text = text;
        textComponent.fontSize = 24;
        textComponent.alignment = TextAlignmentOptions.Center;
        textComponent.color = Color.white;
        
        onSetup?.Invoke(button);
        
        return button;
    }

    // Placeholder callback methods (actual logic is in BattleManager)
    void OnAttackButtonSetup(Button button) { }
    void OnDefendButtonSetup(Button button) { }
    void OnSpecialButtonSetup(Button button) { }
    void OnSuperButtonSetup(Button button) { }
    void OnTeamSuperButtonSetup(Button button) { }
    void OnTargetButtonSetup(Button button) { }
}
