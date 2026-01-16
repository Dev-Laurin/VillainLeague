using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// Enhanced Move Chooser UI with medieval fantasy theme
/// Features a scrollable move list and separate description panel
/// </summary>
public class MoveChooserUI : MonoBehaviour
{
    [Header("Main Panels")]
    public GameObject moveChooserPanel;
    public GameObject moveListContainer;
    public GameObject descriptionPanel;
    
    [Header("Description Panel Elements")]
    public TextMeshProUGUI moveNameText;
    public TextMeshProUGUI moveDescriptionText;
    public TextMeshProUGUI moveCostText;
    public TextMeshProUGUI moveEffectsText;
    
    [Header("Scroll View")]
    public ScrollRect scrollRect;
    public Transform moveButtonParent;
    
    [Header("Medieval Theme Colors")]
    public Color parchmentColor = new Color(0.95f, 0.90f, 0.75f, 1f);
    public Color darkBorderColor = new Color(0.2f, 0.15f, 0.1f, 1f);
    public Color goldAccentColor = new Color(0.85f, 0.70f, 0.25f, 1f);
    public Color affordableColor = new Color(0.25f, 0.20f, 0.15f, 0.9f);
    public Color unaffordableColor = new Color(0.15f, 0.12f, 0.10f, 0.6f);
    public Color hoverColor = new Color(0.45f, 0.35f, 0.25f, 0.95f);
    
    private List<GameObject> moveButtonObjects = new List<GameObject>();
    private Move currentHoveredMove;
    private System.Action<Move> onMoveSelected;
    
    void Start()
    {
        if (moveChooserPanel != null)
            moveChooserPanel.SetActive(false);
    }
    
    /// <summary>
    /// Shows the move chooser with given moves
    /// </summary>
    public void ShowMoveChooser(List<Move> moves, CharacterResource resource, CharacterResource secondaryResource, bool showOnlySupers, System.Action<Move> onSelected)
    {
        onMoveSelected = onSelected;
        ClearMoveButtons();

        // Ensure the chooser panel is active before creating buttons so layouts calculate sizes
        if (moveChooserPanel != null && !moveChooserPanel.activeSelf)
        {
            moveChooserPanel.SetActive(true);
        }
        
        // Filter moves
        List<Move> filteredMoves = new List<Move>();
        foreach (Move move in moves)
        {
            if (showOnlySupers && move.isSuper)
                filteredMoves.Add(move);
            else if (!showOnlySupers && !move.isSuper)
                filteredMoves.Add(move);
        }
        
        // Create move buttons
        CreateMoveButtons(filteredMoves, resource, secondaryResource);
        
        // Show panel
        if (moveChooserPanel != null)
        {
            moveChooserPanel.SetActive(true);
            // Ensure full-panel background doesn't block pointer events to children
            Image mainBg = moveChooserPanel.GetComponent<Image>();
            if (mainBg != null)
                mainBg.raycastTarget = false;

            // Also disable raycast on common non-interactive children (viewport, list background, description)
            if (scrollRect != null && scrollRect.viewport != null)
            {
                Image vpImg = scrollRect.viewport.GetComponent<Image>();
                if (vpImg != null) vpImg.raycastTarget = false;
            }

            if (moveListContainer != null)
            {
                Image listImg = moveListContainer.GetComponent<Image>();
                if (listImg != null) listImg.raycastTarget = false;
            }
        }
            
        // Clear description panel initially
        ClearDescriptionPanel();
        // Make description panel background non-blocking so hover/clicks reach the move buttons
        if (descriptionPanel != null)
        {
            Image descBg = descriptionPanel.GetComponent<Image>();
            if (descBg != null)
                descBg.raycastTarget = false;

            // Also disable raycast on textual elements in description to be safe
            foreach (var tmp in descriptionPanel.GetComponentsInChildren<TextMeshProUGUI>())
            {
                tmp.raycastTarget = false;
            }
        }

        // Defensive runtime fix: If a BackgroundCanvas exists in the scene it may have an Image
        // that blocks pointer events. Disable raycastTarget on any Image under it so our
        // move buttons receive pointer events.
        GameObject bgCanvasObj = GameObject.Find("BackgroundCanvas");
        if (bgCanvasObj != null)
        {
            Image[] bgImages = bgCanvasObj.GetComponentsInChildren<Image>(true);
            foreach (var img in bgImages)
            {
                if (img != null && img.raycastTarget)
                {
                    img.raycastTarget = false;
                    Debug.Log($"[MoveChooser] Disabled raycastTarget on blocking Image: {img.gameObject.name}");
                }
            }
        }

        // Diagnostic: run a UI raycast at the current mouse position to see which elements are under pointer
        DebugUIRaycastAtPointer();
    }
    
    /// <summary>
    /// Hides the move chooser
    /// </summary>
    public void HideMoveChooser()
    {
        if (moveChooserPanel != null)
            moveChooserPanel.SetActive(false);
        ClearMoveButtons();
    }
    
    /// <summary>
    /// Creates interactive move buttons with medieval styling
    /// </summary>
    void CreateMoveButtons(List<Move> moves, CharacterResource resource, CharacterResource secondaryResource)
    {
        Debug.Log($"Creating move buttons {moves.Count}");
        if (moveButtonParent == null || moves == null) return;
        
        for (int i = 0; i < moves.Count; i++)
        {
            Move move = moves[i];
            
            // Create button game object
            GameObject buttonObj = new GameObject($"MoveButton_{move.id}");
            buttonObj.transform.SetParent(moveButtonParent, false);
            
            // Add RectTransform
            RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
            // Let the VerticalLayoutGroup / ContentSizeFitter manage positioning. Provide a preferred height.
            rectTransform.sizeDelta = new Vector2(0, 70);
            rectTransform.anchorMin = new Vector2(0, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(0.5f, 1f);

            // Add LayoutElement so the parent VerticalLayoutGroup can size and space children correctly
            LayoutElement layout = buttonObj.AddComponent<LayoutElement>();
            layout.preferredHeight = 70f;
            layout.preferredWidth = 500f;
            
            // Check affordability
            bool canAffordPrimary = resource == null || resource.CanAfford(move.resourceCost);
            bool canAffordSecondary = true;
            if (move.secondaryResourceCost > 0 && secondaryResource != null)
                canAffordSecondary = secondaryResource.CanAfford(move.secondaryResourceCost);
            bool canAfford = canAffordPrimary && canAffordSecondary;
            
            // Add Image (background)
            // Ensure a CanvasRenderer exists for UI rendering
            if (buttonObj.GetComponent<CanvasRenderer>() == null)
                buttonObj.AddComponent<CanvasRenderer>();

            Image image = buttonObj.AddComponent<Image>();
            image.color = canAfford ? affordableColor : unaffordableColor;
            
            // Add Button
            Button button = buttonObj.AddComponent<Button>();
            button.interactable = canAfford;
            
            // Setup button colors for medieval theme
            ColorBlock colors = button.colors;
            colors.normalColor = canAfford ? affordableColor : unaffordableColor;
            colors.highlightedColor = hoverColor;
            colors.pressedColor = darkBorderColor;
            colors.disabledColor = unaffordableColor;
            button.colors = colors;
            
            // Add click handler
            Move capturedMove = move; // Capture for closure
            button.onClick.AddListener(() => OnMoveClicked(capturedMove));
            
            // Add a small handler component to forward pointer events reliably
            MoveButtonHandler handler = buttonObj.AddComponent<MoveButtonHandler>();
            handler.move = capturedMove;
            handler.parentUI = this;
            handler.resource = resource;
            handler.secondaryResource = secondaryResource;
            
            // Create text elements with medieval styling
            CreateMoveButtonText(buttonObj, move, resource, canAfford);
            

            // Log component presence for diagnostics
            bool hasImage = buttonObj.GetComponent<Image>() != null;
            bool hasButton = buttonObj.GetComponent<Button>() != null;
            bool hasCanvasRenderer = buttonObj.GetComponent<CanvasRenderer>() != null;
            Debug.Log($"[MoveChooser] Components on {buttonObj.name}: Image={hasImage} Button={hasButton} CanvasRenderer={hasCanvasRenderer}");

            moveButtonObjects.Add(buttonObj);

            // Diagnostic logging for each created button
            RectTransform rt = buttonObj.GetComponent<RectTransform>();
            Debug.Log($"[MoveChooser] Created button idx={i} name={buttonObj.name} parent={moveButtonParent.name} rect.size={rt.rect.size} preferredH={layout.preferredHeight} interactable={button.interactable}");
        }

        // Force Unity to rebuild layouts so RectTransforms get correct sizes before input tests
        RectTransform contentRect = moveButtonParent as RectTransform;
        if (contentRect != null)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
            Canvas.ForceUpdateCanvases();
            // Log sizes after rebuild
            for (int j = 0; j < moveButtonObjects.Count; j++)
            {
                var obj = moveButtonObjects[j];
                if (obj == null) continue;
                RectTransform r = obj.GetComponent<RectTransform>();
                Debug.Log($"[MoveChooser] AfterRebuild button idx={j} name={obj.name} rect.size={r.rect.size}");
            }

            // Re-run the UI raycast diagnostic now that layout is rebuilt
            DebugUIRaycastAtPointer();

            // If widths are zero, explicitly set preferredWidth on each LayoutElement to match content width
            float contentWidth = contentRect.rect.width;
            if (contentWidth > 0f)
            {
                for (int k = 0; k < moveButtonObjects.Count; k++)
                {
                    var obj = moveButtonObjects[k];
                    if (obj == null) continue;
                    LayoutElement le = obj.GetComponent<LayoutElement>();
                    if (le != null)
                    {
                        float targetW = Mathf.Max(100f, contentWidth - 20f);
                        le.preferredWidth = targetW;
                    }
                }

                // Force rebuild again with explicit widths
                LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
                Canvas.ForceUpdateCanvases();
                for (int j = 0; j < moveButtonObjects.Count; j++)
                {
                    var obj = moveButtonObjects[j];
                    if (obj == null) continue;
                    RectTransform r = obj.GetComponent<RectTransform>();
                    Debug.Log($"[MoveChooser] Final button idx={j} name={obj.name} rect.size={r.rect.size} preferredW={(obj.GetComponent<LayoutElement>()!=null?obj.GetComponent<LayoutElement>().preferredWidth:0f)}");
                }

                DebugUIRaycastAtPointer();
            }
        }
    }
    
    /// <summary>
    /// Creates text elements for move button
    /// </summary>
    void CreateMoveButtonText(GameObject buttonObj, Move move, CharacterResource resource, bool canAfford)
    {
        // Move name (left side)
        GameObject nameObj = new GameObject("MoveName");
        nameObj.transform.SetParent(buttonObj.transform, false);
        RectTransform nameRect = nameObj.AddComponent<RectTransform>();
        nameRect.anchorMin = new Vector2(0, 0.3f);
        nameRect.anchorMax = new Vector2(0.65f, 0.9f);
        nameRect.offsetMin = new Vector2(15, 0);
        nameRect.offsetMax = new Vector2(0, 0);
        
        TextMeshProUGUI nameText = nameObj.AddComponent<TextMeshProUGUI>();
        nameText.text = move.moveName;
        nameText.fontSize = 22;
        nameText.fontStyle = FontStyles.Bold;
        nameText.alignment = TextAlignmentOptions.Left;
        nameText.color = canAfford ? goldAccentColor : new Color(0.4f, 0.35f, 0.3f);
        // Prevent text from blocking pointer events on the button
        nameText.raycastTarget = false;
        
        // Cost (right side)
        GameObject costObj = new GameObject("Cost");
        costObj.transform.SetParent(buttonObj.transform, false);
        RectTransform costRect = costObj.AddComponent<RectTransform>();
        costRect.anchorMin = new Vector2(0.65f, 0.3f);
        costRect.anchorMax = new Vector2(1, 0.9f);
        costRect.offsetMin = new Vector2(0, 0);
        costRect.offsetMax = new Vector2(-15, 0);
        
        TextMeshProUGUI costText = costObj.AddComponent<TextMeshProUGUI>();
        string resourceName = resource != null ? resource.resourceName : "Resource";
        
        if (move.isPhysical || move.resourceCost == 0)
        {
            costText.text = "Physical";
        }
        else
        {
            costText.text = $"{move.resourceCost} {resourceName}";
        }
        
        if (move.secondaryResourceCost > 0)
        {
            costText.text += $"\n⭐ {move.secondaryResourceCost}";
        }
        
        costText.fontSize = 18;
        costText.alignment = TextAlignmentOptions.Right;
        costText.color = canAfford ? parchmentColor : new Color(0.4f, 0.35f, 0.3f);
        // Prevent text from blocking pointer events on the button
        costText.raycastTarget = false;
    }
    
    /// <summary>
    /// Handles when mouse hovers over a move
    /// </summary>
    public void OnMoveHover(Move move, CharacterResource resource, CharacterResource secondaryResource)
    {
        currentHoveredMove = move;
        Debug.Log($"[MoveChooser] Hover: {move.moveName}");
        ShowMoveDescription(move, resource, secondaryResource);
    }
    
    /// <summary>
    /// Handles when mouse leaves a move
    /// </summary>
    public void OnMoveUnhover()
    {
        currentHoveredMove = null;
        Debug.Log("[MoveChooser] Unhover");
        // Keep description visible for last hovered move
    }
    
    /// <summary>
    /// Shows detailed move description in the description panel
    /// </summary>
    void ShowMoveDescription(Move move, CharacterResource resource, CharacterResource secondaryResource)
    {
        if (descriptionPanel == null) return;
        
        descriptionPanel.SetActive(true);
        
        // Move name
        if (moveNameText != null)
        {
            moveNameText.text = move.moveName;
            moveNameText.color = goldAccentColor;
        }
        
        // Description
        if (moveDescriptionText != null)
        {
            moveDescriptionText.text = move.description;
            moveDescriptionText.color = parchmentColor;
        }
        
        // Cost info
        if (moveCostText != null)
        {
            string costStr = "";
            if (move.isPhysical || move.resourceCost == 0)
            {
                costStr = "Cost: Physical Attack (No Resource Cost)";
            }
            else
            {
                string resourceName = resource != null ? resource.resourceName : "Resource";
                costStr = $"Cost: {move.resourceCost} {resourceName}";
            }
            
            if (move.secondaryResourceCost > 0)
            {
                string secResourceName = secondaryResource != null ? secondaryResource.resourceName : "Secondary";
                costStr += $"\nUltimate Cost: {move.secondaryResourceCost} {secResourceName}";
            }
            
            moveCostText.text = costStr;
            moveCostText.color = new Color(0.9f, 0.8f, 0.6f);
        }
        
        // Effects breakdown
        if (moveEffectsText != null)
        {
            string effects = BuildEffectsText(move);
            moveEffectsText.text = effects;
            moveEffectsText.color = parchmentColor;
        }
    }
    
    /// <summary>
    /// Builds a detailed effects text for the move
    /// </summary>
    string BuildEffectsText(Move move)
    {
        List<string> effects = new List<string>();
        
        // Damage
        if (move.damage > 0)
        {
            string damageStr = $"⚔ Damage: {move.damage}";
            if (move.hits > 1)
                damageStr += $" x{move.hits} hits = {move.damage * move.hits} total";
            effects.Add(damageStr);
        }
        
        // Healing
        if (move.healing > 0)
            effects.Add($"❤ Healing: {move.healing}");
        
        // Buffs
        if (move.attackBuff > 0)
            effects.Add($"↑ Attack Buff: +{move.attackBuff}");
        if (move.defenseBuff > 0)
            effects.Add($"↑ Defense Buff: +{move.defenseBuff}");
        if (move.armor > 0)
            effects.Add($"🛡 Armor: +{move.armor}");
        if (move.evasion > 0)
            effects.Add($"↝ Evasion: +{move.evasion}");
        
        // Debuffs
        if (move.attackDebuff > 0)
            effects.Add($"↓ Attack Debuff: -{move.attackDebuff}");
        if (move.defenseDebuff > 0)
            effects.Add($"↓ Defense Debuff: -{move.defenseDebuff}");
        
        // Special effects
        if (move.ignoreGuard)
            effects.Add("⚡ Ignores Guard");
        if (move.armorPierce > 0)
            effects.Add($"🗡 Armor Pierce: {move.armorPierce}");
        if (move.bleed > 0)
            effects.Add($"🩸 Bleed: {move.bleed}");
        if (move.counterDamage > 0)
            effects.Add($"↩ Counter Damage: {move.counterDamage}");
        if (move.styleGain > 0)
            effects.Add($"⭐ Gain: {move.styleGain}");
        
        // Duration
        if (move.durationTurns > 1)
            effects.Add($"⏱ Duration: {move.durationTurns} turns");
        
        // Target type
        string targetStr = "Target: ";
        switch (move.targetType)
        {
            case MoveTargetType.SingleEnemy:
                targetStr += "Single Enemy";
                break;
            case MoveTargetType.SingleAlly:
                targetStr += "Single Ally";
                break;
            case MoveTargetType.AllEnemies:
                targetStr += "All Enemies";
                break;
            case MoveTargetType.AllAllies:
                targetStr += "All Allies";
                break;
            case MoveTargetType.Self:
                targetStr += "Self";
                break;
            case MoveTargetType.Area:
                targetStr += $"Area (Radius: {move.radius})";
                break;
        }
        effects.Add(targetStr);
        
        return string.Join("\n", effects);
    }
    
    /// <summary>
    /// Clears the description panel
    /// </summary>
    void ClearDescriptionPanel()
    {
        if (descriptionPanel != null)
            descriptionPanel.SetActive(false);
        
        if (moveNameText != null)
            moveNameText.text = "";
        if (moveDescriptionText != null)
            moveDescriptionText.text = "Hover over a move to see details";
        if (moveCostText != null)
            moveCostText.text = "";
        if (moveEffectsText != null)
            moveEffectsText.text = "";
    }

    /// <summary>
    /// Diagnostic: performs a UI GraphicRaycaster raycast at current mouse position and logs hit results
    /// </summary>
    void DebugUIRaycastAtPointer()
    {
        if (EventSystem.current == null)
        {
            Debug.Log("[MoveChooser] DebugUIRaycastAtPointer: No EventSystem.current present");
            return;
        }

        Vector2 pointerPos = Input.mousePosition;
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = pointerPos;

        List<RaycastResult> results = new List<RaycastResult>();

        // Try all GraphicRaycasters in scene
        GraphicRaycaster[] raycasters = FindObjectsOfType<GraphicRaycaster>();
        if (raycasters == null || raycasters.Length == 0)
        {
            Debug.Log("[MoveChooser] DebugUIRaycastAtPointer: No GraphicRaycaster found in scene");
            return;
        }

        foreach (var gr in raycasters)
        {
            results.Clear();
            gr.Raycast(ped, results);
            Debug.Log($"[MoveChooser] Raycast on GraphicRaycaster '{gr.gameObject.name}' found {results.Count} hits at screen {pointerPos}");
            for (int i = 0; i < results.Count; i++)
            {
                var r = results[i];
                Debug.Log($"[MoveChooser]   Hit[{i}] go={r.gameObject.name} module={r.module} distance={r.distance} index={r.index} world={r.worldPosition}");
            }
        }
    }
    
    /// <summary>
    /// Handles move selection
    /// </summary>
    public void OnMoveClicked(Move move)
    {
        Debug.Log($"[MoveChooser] Clicked: {move.moveName}");
        if (onMoveSelected != null)
        {
            onMoveSelected(move);
        }
    }
    
    /// <summary>
    /// Clears all move buttons
    /// </summary>
    void ClearMoveButtons()
    {
        foreach (GameObject obj in moveButtonObjects)
        {
            if (obj != null)
            {
                // Use DestroyImmediate in edit mode, Destroy in play mode
                if (Application.isPlaying)
                    Destroy(obj);
                else
                    DestroyImmediate(obj);
            }
        }
        moveButtonObjects.Clear();
    }
}
