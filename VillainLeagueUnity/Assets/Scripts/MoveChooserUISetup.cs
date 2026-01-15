using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Helper script to set up the enhanced Move Chooser UI with medieval fantasy theme
/// Attach this to your Canvas and click the "Setup Move Chooser UI" button in the Inspector
/// </summary>
[ExecuteInEditMode]
public class MoveChooserUISetup : MonoBehaviour
{
    [Header("Required References")]
    public Canvas canvas;
    public BattleUI battleUI;
    
    [Header("Medieval Theme Colors")]
    public Color parchmentColor = new Color(0.95f, 0.90f, 0.75f, 1f);
    public Color darkBorderColor = new Color(0.2f, 0.15f, 0.1f, 1f);
    public Color goldAccentColor = new Color(0.85f, 0.70f, 0.25f, 1f);
    public Color darkPanelColor = new Color(0.15f, 0.12f, 0.10f, 0.95f);
    
    /// <summary>
    /// Creates the complete Move Chooser UI structure
    /// Call this from the Unity Inspector or from code
    /// </summary>
    public GameObject SetupMoveChooserUI()
    {
        if (canvas == null)
        {
            Debug.LogError("Canvas is required for setting up Move Chooser UI!");
            return null;
        }
        
        // Create main panel
        GameObject mainPanel = CreateMainPanel();
        
        // Create move list panel (left side with scroll view)
        GameObject moveListPanel = CreateMoveListPanel(mainPanel);
        
        // Create description panel (right side)
        GameObject descriptionPanel = CreateDescriptionPanel(mainPanel);
        
        // Add MoveChooserUI component
        MoveChooserUI moveChooserUI = mainPanel.AddComponent<MoveChooserUI>();
        
        // Wire up references
        moveChooserUI.moveChooserPanel = mainPanel;
        moveChooserUI.moveListContainer = moveListPanel;
        moveChooserUI.descriptionPanel = descriptionPanel;
        
        // Find and wire description panel elements
        Transform descContent = descriptionPanel.transform.Find("Content");
        if (descContent != null)
        {
            Transform nameTransform = descContent.Find("MoveName");
            Transform descTransform = descContent.Find("Description");
            Transform costTransform = descContent.Find("Cost");
            Transform effectsTransform = descContent.Find("Effects");
            
            if (nameTransform != null)
                moveChooserUI.moveNameText = nameTransform.GetComponent<TextMeshProUGUI>();
            if (descTransform != null)
                moveChooserUI.moveDescriptionText = descTransform.GetComponent<TextMeshProUGUI>();
            if (costTransform != null)
                moveChooserUI.moveCostText = costTransform.GetComponent<TextMeshProUGUI>();
            if (effectsTransform != null)
                moveChooserUI.moveEffectsText = effectsTransform.GetComponent<TextMeshProUGUI>();
        }
        
        // Find scroll view
        Transform scrollViewTransform = moveListPanel.transform.Find("Scroll View");
        if (scrollViewTransform != null)
        {
            moveChooserUI.scrollRect = scrollViewTransform.GetComponent<ScrollRect>();
            Transform viewportTransform = scrollViewTransform.Find("Viewport");
            if (viewportTransform != null)
            {
                Transform contentTransform = viewportTransform.Find("Content");
                if (contentTransform != null)
                {
                    moveChooserUI.moveButtonParent = contentTransform;
                }
            }
        }
        
        // Wire up to BattleUI if available
        if (battleUI != null)
        {
            battleUI.moveChooserUI = moveChooserUI;
        }
        
        Debug.Log("Move Chooser UI setup complete!");
        return mainPanel;
    }
    
    GameObject CreateMainPanel()
    {
        GameObject mainPanel = new GameObject("MoveChooserPanel");
        mainPanel.transform.SetParent(canvas.transform, false);
        
        RectTransform rectTransform = mainPanel.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.sizeDelta = Vector2.zero;
        
        // Semi-transparent dark background
        Image bgImage = mainPanel.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.7f);
        
        return mainPanel;
    }
    
    GameObject CreateMoveListPanel(GameObject parent)
    {
        GameObject moveListPanel = new GameObject("MoveListPanel");
        moveListPanel.transform.SetParent(parent.transform, false);
        
        RectTransform rectTransform = moveListPanel.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.05f, 0.1f);
        rectTransform.anchorMax = new Vector2(0.55f, 0.9f);
        rectTransform.sizeDelta = Vector2.zero;
        
        // Dark panel with border
        Image panelImage = moveListPanel.AddComponent<Image>();
        panelImage.color = darkPanelColor;
        
        // Add title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(moveListPanel.transform, false);
        RectTransform titleRect = titleObj.AddComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0, 0.92f);
        titleRect.anchorMax = new Vector2(1, 1);
        titleRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "⚔ Choose Your Move ⚔";
        titleText.fontSize = 28;
        titleText.fontStyle = FontStyles.Bold;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = goldAccentColor;
        
        // Create scroll view
        CreateScrollView(moveListPanel);
        
        return moveListPanel;
    }
    
    void CreateScrollView(GameObject parent)
    {
        GameObject scrollView = new GameObject("Scroll View");
        scrollView.transform.SetParent(parent.transform, false);
        
        RectTransform scrollRect = scrollView.AddComponent<RectTransform>();
        scrollRect.anchorMin = new Vector2(0.05f, 0.05f);
        scrollRect.anchorMax = new Vector2(0.95f, 0.90f);
        scrollRect.sizeDelta = Vector2.zero;
        
        ScrollRect scrollComponent = scrollView.AddComponent<ScrollRect>();
        scrollComponent.horizontal = false;
        scrollComponent.vertical = true;
        
        // Create viewport
        GameObject viewport = new GameObject("Viewport");
        viewport.transform.SetParent(scrollView.transform, false);
        
        RectTransform viewportRect = viewport.AddComponent<RectTransform>();
        viewportRect.anchorMin = Vector2.zero;
        viewportRect.anchorMax = Vector2.one;
        viewportRect.sizeDelta = Vector2.zero;
        
        Image viewportImage = viewport.AddComponent<Image>();
        viewportImage.color = new Color(0, 0, 0, 0.2f);
        
        Mask viewportMask = viewport.AddComponent<Mask>();
        viewportMask.showMaskGraphic = false;
        
        // Create content
        GameObject content = new GameObject("Content");
        content.transform.SetParent(viewport.transform, false);
        
        RectTransform contentRect = content.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0, 1);
        contentRect.anchorMax = new Vector2(1, 1);
        contentRect.pivot = new Vector2(0.5f, 1);
        contentRect.sizeDelta = new Vector2(0, 1000);
        
        // Add vertical layout group
        VerticalLayoutGroup layoutGroup = content.AddComponent<VerticalLayoutGroup>();
        layoutGroup.spacing = 10;
        layoutGroup.padding = new RectOffset(10, 10, 10, 10);
        layoutGroup.childAlignment = TextAnchor.UpperCenter;
        layoutGroup.childControlWidth = true;
        layoutGroup.childControlHeight = false;
        layoutGroup.childForceExpandWidth = true;
        layoutGroup.childForceExpandHeight = false;
        
        ContentSizeFitter sizeFitter = content.AddComponent<ContentSizeFitter>();
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        
        // Wire up scroll rect
        scrollComponent.content = contentRect;
        scrollComponent.viewport = viewportRect;
    }
    
    GameObject CreateDescriptionPanel(GameObject parent)
    {
        GameObject descPanel = new GameObject("DescriptionPanel");
        descPanel.transform.SetParent(parent.transform, false);
        
        RectTransform rectTransform = descPanel.AddComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.57f, 0.1f);
        rectTransform.anchorMax = new Vector2(0.95f, 0.9f);
        rectTransform.sizeDelta = Vector2.zero;
        
        // Panel background
        Image panelImage = descPanel.AddComponent<Image>();
        panelImage.color = darkPanelColor;
        
        // Add title
        GameObject titleObj = new GameObject("Title");
        titleObj.transform.SetParent(descPanel.transform, false);
        RectTransform titleRect = titleObj.AddComponent<RectTransform>();
        titleRect.anchorMin = new Vector2(0, 0.92f);
        titleRect.anchorMax = new Vector2(1, 1);
        titleRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI titleText = titleObj.AddComponent<TextMeshProUGUI>();
        titleText.text = "📜 Move Details 📜";
        titleText.fontSize = 28;
        titleText.fontStyle = FontStyles.Bold;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = goldAccentColor;
        
        // Create content area
        GameObject content = new GameObject("Content");
        content.transform.SetParent(descPanel.transform, false);
        RectTransform contentRect = content.AddComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0.05f, 0.05f);
        contentRect.anchorMax = new Vector2(0.95f, 0.90f);
        contentRect.sizeDelta = Vector2.zero;
        
        // Move Name
        GameObject nameObj = new GameObject("MoveName");
        nameObj.transform.SetParent(content.transform, false);
        RectTransform nameRect = nameObj.AddComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0, 0.85f);
        nameRect.anchorMax = new Vector2(1, 0.95f);
        nameRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI nameText = nameObj.AddComponent<TextMeshProUGUI>();
        nameText.text = "";
        nameText.fontSize = 26;
        nameText.fontStyle = FontStyles.Bold;
        nameText.alignment = TextAlignmentOptions.Center;
        nameText.color = goldAccentColor;
        
        // Description
        GameObject descObj = new GameObject("Description");
        descObj.transform.SetParent(content.transform, false);
        RectTransform descRect = descObj.AddComponent<RectTransform>();
        descRect.anchorMin = new Vector2(0, 0.65f);
        descRect.anchorMax = new Vector2(1, 0.83f);
        descRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI descText = descObj.AddComponent<TextMeshProUGUI>();
        descText.text = "Hover over a move to see details";
        descText.fontSize = 18;
        descText.alignment = TextAlignmentOptions.TopLeft;
        descText.color = parchmentColor;
        descText.enableWordWrapping = true;
        
        // Cost
        GameObject costObj = new GameObject("Cost");
        costObj.transform.SetParent(content.transform, false);
        RectTransform costRect = costObj.AddComponent<RectTransform>();
        costRect.anchorMin = new Vector2(0, 0.55f);
        costRect.anchorMax = new Vector2(1, 0.63f);
        costRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI costText = costObj.AddComponent<TextMeshProUGUI>();
        costText.text = "";
        costText.fontSize = 18;
        costText.fontStyle = FontStyles.Bold;
        costText.alignment = TextAlignmentOptions.TopLeft;
        costText.color = new Color(0.9f, 0.8f, 0.6f);
        costText.enableWordWrapping = true;
        
        // Effects
        GameObject effectsObj = new GameObject("Effects");
        effectsObj.transform.SetParent(content.transform, false);
        RectTransform effectsRect = effectsObj.AddComponent<RectTransform>();
        effectsRect.anchorMin = new Vector2(0, 0);
        effectsRect.anchorMax = new Vector2(1, 0.53f);
        effectsRect.sizeDelta = Vector2.zero;
        
        TextMeshProUGUI effectsText = effectsObj.AddComponent<TextMeshProUGUI>();
        effectsText.text = "";
        effectsText.fontSize = 16;
        effectsText.alignment = TextAlignmentOptions.TopLeft;
        effectsText.color = parchmentColor;
        effectsText.enableWordWrapping = true;
        
        return descPanel;
    }
}
