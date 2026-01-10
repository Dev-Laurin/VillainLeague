using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<Character> turnOrder = new List<Character>();
    private int currentTurnIndex = 0;

    public void InitializeTurnOrder(List<Character> allCharacters)
    {
        turnOrder.Clear();
        turnOrder.AddRange(allCharacters);
        currentTurnIndex = 0;
        Debug.Log("Turn order initialized");
    }

    public Character GetCurrentCharacter()
    {
        if (turnOrder.Count == 0) return null;
        
        // Skip dead characters
        while (!turnOrder[currentTurnIndex].IsAlive())
        {
            NextTurn();
            if (currentTurnIndex >= turnOrder.Count)
            {
                currentTurnIndex = 0;
            }
        }
        
        return turnOrder[currentTurnIndex];
    }

    public void NextTurn()
    {
        currentTurnIndex++;
        if (currentTurnIndex >= turnOrder.Count)
        {
            currentTurnIndex = 0;
            Debug.Log("New round started!");
        }
        
        Debug.Log($"Turn: {GetCurrentCharacter()?.characterName}");
    }

    public bool HasAlivePlayers()
    {
        foreach (Character c in turnOrder)
        {
            if (c.isPlayerCharacter && c.IsAlive())
                return true;
        }
        return false;
    }

    public bool HasAliveEnemies()
    {
        foreach (Character c in turnOrder)
        {
            if (!c.isPlayerCharacter && c.IsAlive())
                return true;
        }
        return false;
    }
}
