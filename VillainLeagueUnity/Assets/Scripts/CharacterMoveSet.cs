using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CharacterResource
{
    public string resourceName = "Energy";
    public int maxResource = 10;
    public int currentResource;
    public int regenPerTurn = 1;
    
    public CharacterResource(string name, int max, int regen)
    {
        resourceName = name;
        maxResource = max;
        currentResource = max;
        regenPerTurn = regen;
    }
    
    public void Regenerate()
    {
        currentResource = Mathf.Min(currentResource + regenPerTurn, maxResource);
    }
    
    public bool CanAfford(int cost)
    {
        return currentResource >= cost;
    }
    
    public void Spend(int cost)
    {
        currentResource = Mathf.Max(0, currentResource - cost);
    }
}

[CreateAssetMenu(fileName = "CharacterMoveSet", menuName = "Battle/CharacterMoveSet")]
public class CharacterMoveSet : ScriptableObject
{
    public string characterName;
    public string role;
    public CharacterResource resource;
    public List<Move> moves = new List<Move>();
    
    public void InitializeResource()
    {
        if (resource != null)
        {
            resource.currentResource = resource.maxResource;
        }
    }
}
