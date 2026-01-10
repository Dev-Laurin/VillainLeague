using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class BattleUI : MonoBehaviour
{
    [Header("Player Squad UI")]
    public TextMeshProUGUI player1NameText;
    public TextMeshProUGUI player1HPText;
    public Slider player1HPBar;
    public TextMeshProUGUI player1ResourceText;
    public TextMeshProUGUI player1SecondaryResourceText;
    
    public TextMeshProUGUI player2NameText;
    public TextMeshProUGUI player2HPText;
    public Slider player2HPBar;
    public TextMeshProUGUI player2ResourceText;
    public TextMeshProUGUI player2SecondaryResourceText;

    [Header("Enemy UI")]
    public TextMeshProUGUI enemy1NameText;
    public TextMeshProUGUI enemy1HPText;
    public Slider enemy1HPBar;
    
    public TextMeshProUGUI enemy2NameText;
    public TextMeshProUGUI enemy2HPText;
    public Slider enemy2HPBar;

    [Header("Turn Info")]
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI nextTurnText;
    public TextMeshProUGUI messageText;

    [Header("Action Buttons")]
    public Button attackButton;
    public Button defendButton;
    public Button specialButton;
    
    [Header("Super Buttons")]
    public Button superButton;
    public Button teamSuperButton;

    [Header("Move Selection")]
    public GameObject moveSelectionPanel;
    public Transform moveButtonContainer;
    private List<Button> moveButtons = new List<Button>();

    [Header("Target Selection")]
    public GameObject targetSelectionPanel;
    public Button target1Button;
    public Button target2Button;
    
    [Header("Banter Dialogue")]
    public GameObject banterDialoguePanel;
    public TextMeshProUGUI banterDialogueText;
    private Coroutine banterFadeCoroutine;

    private void Start()
    {
        if (targetSelectionPanel != null)
            targetSelectionPanel.SetActive(false);
        if (moveSelectionPanel != null)
            moveSelectionPanel.SetActive(false);
        if (superButton != null)
            superButton.gameObject.SetActive(false);
        if (teamSuperButton != null)
            teamSuperButton.gameObject.SetActive(false);
        if (banterDialoguePanel != null)
            banterDialoguePanel.SetActive(false);
    }

    public void UpdateCharacterUI(Character character, int index)
    {
        if (character == null) return;

        TextMeshProUGUI nameText = null;
        TextMeshProUGUI hpText = null;
        Slider hpBar = null;
        TextMeshProUGUI resourceText = null;
        TextMeshProUGUI secondaryResourceText = null;

        if (character.isPlayerCharacter)
        {
            if (index == 0)
            {
                nameText = player1NameText;
                hpText = player1HPText;
                hpBar = player1HPBar;
                resourceText = player1ResourceText;
                secondaryResourceText = player1SecondaryResourceText;
            }
            else if (index == 1)
            {
                nameText = player2NameText;
                hpText = player2HPText;
                hpBar = player2HPBar;
                resourceText = player2ResourceText;
                secondaryResourceText = player2SecondaryResourceText;
            }
        }
        else
        {
            if (index == 0)
            {
                nameText = enemy1NameText;
                hpText = enemy1HPText;
                hpBar = enemy1HPBar;
            }
            else if (index == 1)
            {
                nameText = enemy2NameText;
                hpText = enemy2HPText;
                hpBar = enemy2HPBar;
            }
        }

        if (nameText != null) nameText.text = character.characterName;
        if (hpText != null) hpText.text = $"{character.currentHP}/{character.maxHP}";
        if (hpBar != null)
        {
            hpBar.maxValue = character.maxHP;
            hpBar.value = character.currentHP;
        }
        
        // Update resource display for player characters
        if (resourceText != null && character.moveSet != null && character.moveSet.resource != null)
        {
            CharacterResource res = character.moveSet.resource;
            resourceText.text = $"{res.resourceName}: {res.currentResource}/{res.maxResource}";
        }
        
        // Update secondary resource display (e.g., Style for Naice)
        if (secondaryResourceText != null && character.secondaryResource != null)
        {
            CharacterResource secRes = character.secondaryResource;
            secondaryResourceText.text = $"{secRes.resourceName}: {secRes.currentResource}/{secRes.maxResource}";
        }
        else if (secondaryResourceText != null)
        {
            // Clear text if no secondary resource
            secondaryResourceText.text = "";
        }
    }

    public void UpdateTurnText(string characterName)
    {
        if (turnText != null)
            turnText.text = $"Turn: {characterName}";
    }

    public void UpdateNextTurnText(string characterName)
    {
        if (nextTurnText != null)
            nextTurnText.text = $"Next: {characterName}";
    }

    public void ShowMessage(string message)
    {
        if (messageText != null)
            messageText.text = message;
    }

    public void SetActionButtonsActive(bool active)
    {
        if (attackButton != null) attackButton.gameObject.SetActive(active);
        if (defendButton != null) defendButton.gameObject.SetActive(active);
        if (specialButton != null) specialButton.gameObject.SetActive(active);
    }

    public void ShowTargetSelection(bool show)
    {
        if (targetSelectionPanel != null)
            targetSelectionPanel.SetActive(show);
    }
    
    public void ShowMoveSelection(bool show)
    {
        if (moveSelectionPanel != null)
            moveSelectionPanel.SetActive(show);
    }
    
    public void SetSuperButtonsActive(bool superReady, bool teamSuperReady)
    {
        if (superButton != null)
            superButton.gameObject.SetActive(superReady);
        if (teamSuperButton != null)
            teamSuperButton.gameObject.SetActive(teamSuperReady);
    }
    
    public void DisplayMoves(List<Move> moves, CharacterResource resource, CharacterResource secondaryResource, bool showOnlySupers, System.Action<Move> onMoveSelected)
    {
        // Clear existing move buttons
        foreach (Button btn in moveButtons)
        {
            if (btn != null)
                Destroy(btn.gameObject);
        }
        moveButtons.Clear();
        
        if (moveButtonContainer == null || moves == null) return;
        
        // Filter moves based on showOnlySupers flag
        List<Move> filteredMoves = new List<Move>();
        foreach (Move move in moves)
        {
            if (showOnlySupers && move.isSuper)
            {
                filteredMoves.Add(move);
            }
            else if (!showOnlySupers && !move.isSuper)
            {
                filteredMoves.Add(move);
            }
        }
        
        // Create a button for each move
        for (int i = 0; i < filteredMoves.Count; i++)
        {
            Move move = filteredMoves[i];
            GameObject buttonObj = new GameObject($"MoveButton_{i}");
            buttonObj.transform.SetParent(moveButtonContainer, false);
            
            RectTransform rectTransform = buttonObj.AddComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(700, 80);
            
            Image image = buttonObj.AddComponent<Image>();
            Button button = buttonObj.AddComponent<Button>();
            
            // Check if move can be afforded (both primary and secondary resources)
            bool canAffordPrimary = resource == null || resource.CanAfford(move.resourceCost);
            bool canAffordSecondary = true;
            if (move.secondaryResourceCost > 0 && secondaryResource != null)
            {
                canAffordSecondary = secondaryResource.CanAfford(move.secondaryResourceCost);
            }
            bool canAfford = canAffordPrimary && canAffordSecondary;
            
            if (canAfford)
            {
                image.color = new Color(0.3f, 0.3f, 0.3f);
                ColorBlock colors = button.colors;
                colors.normalColor = new Color(0.3f, 0.3f, 0.3f);
                colors.highlightedColor = new Color(0.5f, 0.5f, 0.5f);
                colors.pressedColor = new Color(0.2f, 0.2f, 0.2f);
                button.colors = colors;
                
                button.onClick.AddListener(() => onMoveSelected(move));
            }
            else
            {
                image.color = new Color(0.2f, 0.2f, 0.2f);
                button.interactable = false;
            }
            
            // Move name
            GameObject nameTextObj = new GameObject("Name");
            nameTextObj.transform.SetParent(buttonObj.transform, false);
            RectTransform nameRect = nameTextObj.AddComponent<RectTransform>();
            nameRect.anchorMin = new Vector2(0, 0.5f);
            nameRect.anchorMax = new Vector2(0.7f, 1);
            nameRect.sizeDelta = Vector2.zero;
            
            TextMeshProUGUI nameText = nameTextObj.AddComponent<TextMeshProUGUI>();
            nameText.text = move.moveName;
            nameText.fontSize = 24;
            nameText.alignment = TextAlignmentOptions.Left;
            nameText.margin = new Vector4(10, 0, 0, 0);
            nameText.color = canAfford ? Color.white : new Color(0.5f, 0.5f, 0.5f);
            
            // Cost
            GameObject costTextObj = new GameObject("Cost");
            costTextObj.transform.SetParent(buttonObj.transform, false);
            RectTransform costRect = costTextObj.AddComponent<RectTransform>();
            costRect.anchorMin = new Vector2(0.7f, 0.5f);
            costRect.anchorMax = new Vector2(1, 1);
            costRect.sizeDelta = Vector2.zero;
            
            TextMeshProUGUI costText = costTextObj.AddComponent<TextMeshProUGUI>();
            // Display "Physical" for physical attacks, resource cost for magic
            if (move.isPhysical || move.resourceCost == 0)
            {
                costText.text = "Physical";
            }
            else
            {
                string resourceName = resource != null ? resource.resourceName : "Resource";
                costText.text = $"{resourceName}: {move.resourceCost}";
            }
            costText.fontSize = 20;
            costText.alignment = TextAlignmentOptions.Right;
            costText.margin = new Vector4(0, 0, 10, 0);
            costText.color = canAfford ? new Color(0.7f, 0.9f, 1f) : new Color(0.5f, 0.5f, 0.5f);
            
            // Description
            GameObject descTextObj = new GameObject("Description");
            descTextObj.transform.SetParent(buttonObj.transform, false);
            RectTransform descRect = descTextObj.AddComponent<RectTransform>();
            descRect.anchorMin = new Vector2(0, 0);
            descRect.anchorMax = new Vector2(1, 0.5f);
            descRect.sizeDelta = Vector2.zero;
            
            TextMeshProUGUI descText = descTextObj.AddComponent<TextMeshProUGUI>();
            descText.text = move.description;
            descText.fontSize = 16;
            descText.alignment = TextAlignmentOptions.Left;
            descText.margin = new Vector4(10, 0, 10, 0);
            descText.color = canAfford ? new Color(0.8f, 0.8f, 0.8f) : new Color(0.5f, 0.5f, 0.5f);
            
            moveButtons.Add(button);
        }
    }
    
    /// <summary>
    /// Shows banter dialogue that auto-dismisses after the specified duration
    /// </summary>
    public void ShowBanterDialogue(string dialogue, float duration)
    {
        if (banterDialoguePanel == null || banterDialogueText == null)
            return;
        
        // Stop any existing fade coroutine
        if (banterFadeCoroutine != null)
        {
            StopCoroutine(banterFadeCoroutine);
        }
        
        // Set the dialogue text
        banterDialogueText.text = dialogue;
        
        // Show the panel
        banterDialoguePanel.SetActive(true);
        
        // Start auto-dismiss coroutine
        banterFadeCoroutine = StartCoroutine(AutoDismissBanter(duration));
    }
    
    /// <summary>
    /// Coroutine that auto-dismisses the banter dialogue after duration
    /// </summary>
    private System.Collections.IEnumerator AutoDismissBanter(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        if (banterDialoguePanel != null)
        {
            banterDialoguePanel.SetActive(false);
        }
    }
}
