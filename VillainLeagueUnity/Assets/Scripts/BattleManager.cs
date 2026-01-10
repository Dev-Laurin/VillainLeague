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
    private Move selectedMove;

    void Start()
    {
        SetupBattle();
    }

    void SetupBattle()
    {
        // Initialize player squad with 2 characters
        Character cecelia = new Character("Cecelia Sylvan", 100, 15, 5, true);
        cecelia.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("Cecelia Sylvan"));
        playerSquad.Add(cecelia);
        
        Character naice = new Character("Naice Ajimi", 80, 20, 3, true);
        naice.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("Naice Ajimi"));
        // Initialize Style as a secondary resource for Naice
        naice.secondaryResource = new CharacterResource("Style", 6, 0); // Max 6, no auto-regen (gained through moves)
        playerSquad.Add(naice);

        // Initialize enemy squad with 2 characters
        Character villain1 = new Character("Villain 1", 70, 12, 4, false);
        villain1.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("Villain 1"));
        enemySquad.Add(villain1);
        
        Character villain2 = new Character("Villain 2", 90, 10, 6, false);
        villain2.SetMoveSet(MoveSetLoader.LoadMoveSetFromFile("Villain 2"));
        enemySquad.Add(villain2);

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
            
            // Update next turn display
            Character nextCharacter = turnManager.GetNextCharacter();
            if (nextCharacter != null)
            {
                battleUI.UpdateNextTurnText(nextCharacter.characterName);
            }

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
        
        // Regenerate resource at start of turn
        if (player.moveSet != null && player.moveSet.resource != null)
        {
            player.moveSet.resource.Regenerate();
            UpdateAllUI(); // Update resource display
        }
        
        // Check if character has a moveset
        if (player.moveSet != null && player.moveSet.moves != null && player.moveSet.moves.Count > 0)
        {
            // Use move selection system
            yield return StartCoroutine(SelectMove(player));
            
            if (selectedMove != null)
            {
                yield return StartCoroutine(ExecuteMove(player, selectedMove));
            }
        }
        else
        {
            // Fall back to old system for characters without movesets
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
    }
    
    IEnumerator SelectMove(Character character)
    {
        battleUI.ShowMessage("Select a move...");
        battleUI.ShowMoveSelection(true);
        
        bool moveSelected = false;
        selectedMove = null;
        
        // Display moves with resource info
        battleUI.DisplayMoves(character.moveSet.moves, character.moveSet.resource, (Move move) =>
        {
            selectedMove = move;
            moveSelected = true;
        });
        
        // Wait for selection
        while (!moveSelected)
        {
            yield return null;
        }
        
        battleUI.ShowMoveSelection(false);
    }
    
    IEnumerator ExecuteMove(Character caster, Move move)
    {
        // Check if can afford
        if (caster.moveSet.resource != null && !caster.moveSet.resource.CanAfford(move.resourceCost))
        {
            battleUI.ShowMessage($"Not enough {caster.moveSet.resource.resourceName}!");
            yield return new WaitForSeconds(1f);
            yield break;
        }
        
        // Determine if we need a target
        bool needsTarget = move.targetType == MoveTargetType.SingleEnemy || move.targetType == MoveTargetType.SingleAlly;
        
        if (needsTarget)
        {
            // Select target based on move type
            List<Character> targetList = move.targetType == MoveTargetType.SingleEnemy ? enemySquad : playerSquad;
            yield return StartCoroutine(SelectTarget(targetList));
            
            if (selectedTarget == null || !selectedTarget.IsAlive())
            {
                yield break;
            }
        }
        
        // Spend resource
        if (caster.moveSet.resource != null)
        {
            caster.moveSet.resource.Spend(move.resourceCost);
            UpdateAllUI();
        }
        
        // Execute move effects
        battleUI.ShowMessage($"{caster.characterName} uses {move.moveName}!");
        yield return new WaitForSeconds(0.5f);
        
        if (needsTarget && selectedTarget != null)
        {
            // Apply damage
            if (move.damage > 0)
            {
                int totalDamage = move.damage * move.hits;
                int actualDamage = Mathf.Max(1, totalDamage - selectedTarget.defense);
                selectedTarget.TakeDamage(totalDamage);
                
                string hitText = move.hits > 1 ? $" ({move.hits} hits)" : "";
                battleUI.ShowMessage($"{caster.characterName}'s {move.moveName} hits {selectedTarget.characterName} for {actualDamage} damage{hitText}!");
            }
            
            // Apply healing
            if (move.healing > 0)
            {
                selectedTarget.Heal(move.healing);
                battleUI.ShowMessage($"{caster.characterName}'s {move.moveName} heals {selectedTarget.characterName} for {move.healing}!");
            }
        }
        else if (move.targetType == MoveTargetType.Self)
        {
            if (move.healing > 0)
            {
                caster.Heal(move.healing);
                battleUI.ShowMessage($"{caster.characterName} heals for {move.healing}!");
            }
            else
            {
                battleUI.ShowMessage($"{caster.characterName} uses {move.moveName}!");
            }
        }
        else if (move.targetType == MoveTargetType.AllEnemies)
        {
            foreach (Character enemy in enemySquad)
            {
                if (enemy.IsAlive() && move.damage > 0)
                {
                    int actualDamage = Mathf.Max(1, move.damage - enemy.defense);
                    enemy.TakeDamage(move.damage);
                }
            }
            battleUI.ShowMessage($"{caster.characterName}'s {move.moveName} hits all enemies!");
        }
        else if (move.targetType == MoveTargetType.AllAllies)
        {
            battleUI.ShowMessage($"{caster.characterName} uses {move.moveName} on all allies!");
        }
        
        // Gain secondary resource (Style) if move has styleGain
        if (move.styleGain > 0 && caster.secondaryResource != null)
        {
            int oldStyle = caster.secondaryResource.currentResource;
            caster.secondaryResource.currentResource = Mathf.Min(
                caster.secondaryResource.currentResource + move.styleGain,
                caster.secondaryResource.maxResource
            );
            int styleGained = caster.secondaryResource.currentResource - oldStyle;
            if (styleGained > 0)
            {
                battleUI.ShowMessage($"{caster.characterName} gains {styleGained} {caster.secondaryResource.resourceName}!");
                UpdateAllUI();
                yield return new WaitForSeconds(0.8f);
            }
        }
        
        yield return new WaitForSeconds(1.5f);
    }

    IEnumerator EnemyTurn(Character enemy)
    {
        battleUI.ShowMessage($"{enemy.characterName}'s turn!");
        yield return new WaitForSeconds(1f);
        
        // Regenerate resource
        if (enemy.moveSet != null && enemy.moveSet.resource != null)
        {
            enemy.moveSet.resource.Regenerate();
        }

        // Simple AI: attack random alive player with a random affordable move
        List<Character> alivePlayerCharacters = new List<Character>();
        foreach (Character c in playerSquad)
        {
            if (c.IsAlive())
                alivePlayerCharacters.Add(c);
        }

        if (alivePlayerCharacters.Count > 0)
        {
            Character target = alivePlayerCharacters[Random.Range(0, alivePlayerCharacters.Count)];
            
            // Try to use a move if available
            if (enemy.moveSet != null && enemy.moveSet.moves != null && enemy.moveSet.moves.Count > 0)
            {
                // Find affordable offensive moves
                List<Move> affordableMoves = new List<Move>();
                foreach (Move move in enemy.moveSet.moves)
                {
                    if ((enemy.moveSet.resource == null || enemy.moveSet.resource.CanAfford(move.resourceCost)) &&
                        (move.damage > 0 || move.targetType == MoveTargetType.AllEnemies))
                    {
                        affordableMoves.Add(move);
                    }
                }
                
                if (affordableMoves.Count > 0)
                {
                    Move chosenMove = affordableMoves[Random.Range(0, affordableMoves.Count)];
                    selectedMove = chosenMove;
                    selectedTarget = target;
                    yield return StartCoroutine(ExecuteMove(enemy, chosenMove));
                }
                else
                {
                    // No affordable moves, basic attack
                    int attackValue = enemy.attack;
                    int actualDamage = Mathf.Max(1, attackValue - target.defense);
                    target.TakeDamage(attackValue);
                    battleUI.ShowMessage($"{enemy.characterName} attacks {target.characterName} for {actualDamage} damage!");
                }
            }
            else
            {
                // No moveset, use basic attack
                int attackValue = enemy.attack;
                int actualDamage = Mathf.Max(1, attackValue - target.defense);
                target.TakeDamage(attackValue);
                battleUI.ShowMessage($"{enemy.characterName} attacks {target.characterName} for {actualDamage} damage!");
            }
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
