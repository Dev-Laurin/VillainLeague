using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    [Header("Player Squad UI")]
    public TextMeshProUGUI player1NameText;
    public TextMeshProUGUI player1HPText;
    public Slider player1HPBar;
    
    public TextMeshProUGUI player2NameText;
    public TextMeshProUGUI player2HPText;
    public Slider player2HPBar;

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

    [Header("Target Selection")]
    public GameObject targetSelectionPanel;
    public Button target1Button;
    public Button target2Button;

    private void Start()
    {
        if (targetSelectionPanel != null)
            targetSelectionPanel.SetActive(false);
    }

    public void UpdateCharacterUI(Character character, int index)
    {
        if (character == null) return;

        TextMeshProUGUI nameText = null;
        TextMeshProUGUI hpText = null;
        Slider hpBar = null;

        if (character.isPlayerCharacter)
        {
            if (index == 0)
            {
                nameText = player1NameText;
                hpText = player1HPText;
                hpBar = player1HPBar;
            }
            else if (index == 1)
            {
                nameText = player2NameText;
                hpText = player2HPText;
                hpBar = player2HPBar;
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
}
