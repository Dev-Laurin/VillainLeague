using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    START,
    PLAYER_TURN,
    ENEMY_TURN,
    WAITING,
    WON,
    LOST
}

public enum BattleAction
{
    ATTACK,
    DEFEND,
    SPECIAL
}

public class BattleManager : MonoBehaviour
{
    [Header("Characters")]
    public List<Character> playerSquad = new List<Character>();
    public List<Character> enemySquad = new List<Character>();
    
    [Header("References")]
    public BattleUI battleUI;
    public TurnManager turnManager;

    private BattleState currentState;
    private BattleAction selectedAction;
    private Character selectedTarget;
    private bool isDefending = false;

    void Start()
    {
        SetupBattle();
    }

    void SetupBattle()
    {
        // Initialize player squad with 2 characters
        playerSquad.Add(new Character("Hero 1", 100, 15, 5, true));
        playerSquad.Add(new Character("Hero 2", 80, 20, 3, true));

        // Initialize enemy squad with 2 characters
        enemySquad.Add(new Character("Villain 1", 70, 12, 4, false));
        enemySquad.Add(new Character("Villain 2", 90, 10, 6, false));

        // Setup turn order
        List<Character> allCharacters = new List<Character>();
        allCharacters.AddRange(playerSquad);
        allCharacters.AddRange(enemySquad);
        turnManager.InitializeTurnOrder(allCharacters);

        // Update UI
        UpdateAllUI();
        
        currentState = BattleState.START;
        StartCoroutine(BattleFlow());
    }

    IEnumerator BattleFlow()
    {
        yield return new WaitForSeconds(0.5f);
        battleUI.ShowMessage("Battle Start!");
        yield return new WaitForSeconds(1f);

        while (currentState != BattleState.WON && currentState != BattleState.LOST)
        {
            Character currentCharacter = turnManager.GetCurrentCharacter();
            
            if (currentCharacter == null)
            {
                yield break;
            }

            battleUI.UpdateTurnText(currentCharacter.characterName);

            if (currentCharacter.isPlayerCharacter)
            {
                currentState = BattleState.PLAYER_TURN;
                yield return StartCoroutine(PlayerTurn(currentCharacter));
            }
            else
            {
                currentState = BattleState.ENEMY_TURN;
                yield return StartCoroutine(EnemyTurn(currentCharacter));
            }

            UpdateAllUI();
            
            // Check win/lose conditions
            if (!turnManager.HasAliveEnemies())
            {
                currentState = BattleState.WON;
                battleUI.ShowMessage("Victory!");
                Debug.Log("Player wins!");
                yield break;
            }
            
            if (!turnManager.HasAlivePlayers())
            {
                currentState = BattleState.LOST;
                battleUI.ShowMessage("Defeat...");
                Debug.Log("Player loses!");
                yield break;
            }

            turnManager.NextTurn();
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator PlayerTurn(Character player)
    {
        battleUI.ShowMessage($"{player.characterName}'s turn!");
        battleUI.SetActionButtonsActive(true);
        
        // Wait for player to select action
        bool actionSelected = false;
        selectedAction = BattleAction.ATTACK;
        
        // Setup button listeners
        battleUI.attackButton.onClick.RemoveAllListeners();
        battleUI.attackButton.onClick.AddListener(() => 
        {
            selectedAction = BattleAction.ATTACK;
            actionSelected = true;
        });

        battleUI.defendButton.onClick.RemoveAllListeners();
        battleUI.defendButton.onClick.AddListener(() => 
        {
            selectedAction = BattleAction.DEFEND;
            actionSelected = true;
        });

        battleUI.specialButton.onClick.RemoveAllListeners();
        battleUI.specialButton.onClick.AddListener(() => 
        {
            selectedAction = BattleAction.SPECIAL;
            actionSelected = true;
        });

        // Wait for action selection
        while (!actionSelected)
        {
            yield return null;
        }

        battleUI.SetActionButtonsActive(false);

        // Execute action
        if (selectedAction == BattleAction.DEFEND)
        {
            isDefending = true;
            battleUI.ShowMessage($"{player.characterName} defends!");
            yield return new WaitForSeconds(1f);
        }
        else
        {
            // Select target
            yield return StartCoroutine(SelectTarget(enemySquad));
            
            if (selectedTarget != null && selectedTarget.IsAlive())
            {
                if (selectedAction == BattleAction.ATTACK)
                {
                    int attackValue = player.attack;
                    int actualDamage = Mathf.Max(1, attackValue - selectedTarget.defense);
                    selectedTarget.TakeDamage(attackValue);
                    battleUI.ShowMessage($"{player.characterName} attacks {selectedTarget.characterName} for {actualDamage} damage!");
                }
                else if (selectedAction == BattleAction.SPECIAL)
                {
                    int attackValue = player.attack * 2;
                    int actualDamage = Mathf.Max(1, attackValue - selectedTarget.defense);
                    selectedTarget.TakeDamage(attackValue);
                    battleUI.ShowMessage($"{player.characterName} uses special attack on {selectedTarget.characterName} for {actualDamage} damage!");
                }
                
                yield return new WaitForSeconds(1.5f);
            }
        }
    }

    IEnumerator EnemyTurn(Character enemy)
    {
        battleUI.ShowMessage($"{enemy.characterName}'s turn!");
        yield return new WaitForSeconds(1f);

        // Simple AI: attack random alive player
        List<Character> alivePlayerCharacters = new List<Character>();
        foreach (Character c in playerSquad)
        {
            if (c.IsAlive())
                alivePlayerCharacters.Add(c);
        }

        if (alivePlayerCharacters.Count > 0)
        {
            Character target = alivePlayerCharacters[Random.Range(0, alivePlayerCharacters.Count)];
            int attackValue = enemy.attack;
            int actualDamage = Mathf.Max(1, attackValue - target.defense);
            target.TakeDamage(attackValue);
            battleUI.ShowMessage($"{enemy.characterName} attacks {target.characterName} for {actualDamage} damage!");
        }

        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator SelectTarget(List<Character> possibleTargets)
    {
        battleUI.ShowMessage("Select target...");
        battleUI.ShowTargetSelection(true);
        
        bool targetSelected = false;
        selectedTarget = null;

        // Get alive targets
        List<Character> aliveTargets = new List<Character>();
        foreach (Character c in possibleTargets)
        {
            if (c.IsAlive())
                aliveTargets.Add(c);
        }

        if (aliveTargets.Count == 0)
        {
            battleUI.ShowTargetSelection(false);
            yield break;
        }

        // Setup target buttons
        battleUI.target1Button.onClick.RemoveAllListeners();
        if (aliveTargets.Count > 0)
        {
            battleUI.target1Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = aliveTargets[0].characterName;
            battleUI.target1Button.onClick.AddListener(() => 
            {
                selectedTarget = aliveTargets[0];
                targetSelected = true;
            });
            battleUI.target1Button.gameObject.SetActive(true);
        }
        else
        {
            battleUI.target1Button.gameObject.SetActive(false);
        }

        battleUI.target2Button.onClick.RemoveAllListeners();
        if (aliveTargets.Count > 1)
        {
            battleUI.target2Button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = aliveTargets[1].characterName;
            battleUI.target2Button.onClick.AddListener(() => 
            {
                selectedTarget = aliveTargets[1];
                targetSelected = true;
            });
            battleUI.target2Button.gameObject.SetActive(true);
        }
        else
        {
            battleUI.target2Button.gameObject.SetActive(false);
        }

        // Wait for selection
        while (!targetSelected)
        {
            yield return null;
        }

        battleUI.ShowTargetSelection(false);
    }

    void UpdateAllUI()
    {
        // Update player squad UI
        for (int i = 0; i < playerSquad.Count; i++)
        {
            battleUI.UpdateCharacterUI(playerSquad[i], i);
        }

        // Update enemy squad UI
        for (int i = 0; i < enemySquad.Count; i++)
        {
            battleUI.UpdateCharacterUI(enemySquad[i], i);
        }
    }
}
