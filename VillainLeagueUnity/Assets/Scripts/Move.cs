using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Move
{
    public string id;
    public string moveName;
    public string description;
    public int resourceCost;
    public bool isPhysical; // True for physical attacks (no mana cost), false for magic
    
    // Damage and effect values
    public int damage;
    public int hits = 1;
    public int healing;
    
    // Buff/Debuff values
    public int attackBuff;
    public int attackDebuff;
    public int defenseBuff;
    public int defenseDebuff;
    public int evasion;
    public int armor;
    
    // Special effect flags
    public bool ignoreGuard;
    public int armorPierce;
    public int bleed;
    public int counterDamage;
    
    // Secondary resource gain (e.g., Style)
    public int styleGain;
    
    // Secondary resource cost (e.g., Style) - for super/ultimate moves
    public int secondaryResourceCost;
    public bool isSuper; // True if this is a super/ultimate move
    
    // Area of effect
    public int radius;
    public int moveRange;
    
    // Duration
    public int durationTurns = 1;
    
    // Target type
    public MoveTargetType targetType = MoveTargetType.SingleEnemy;
    
    public Move(string id, string name, string desc, int cost = 0)
    {
        this.id = id;
        this.moveName = name;
        this.description = desc;
        this.resourceCost = cost;
    }
}

public enum MoveTargetType
{
    SingleEnemy,
    SingleAlly,
    AllEnemies,
    AllAllies,
    Self,
    Area
}
