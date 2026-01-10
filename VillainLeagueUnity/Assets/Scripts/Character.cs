using UnityEngine;

[System.Serializable]
public class Character
{
    public string characterName;
    public int maxHP;
    public int currentHP;
    public int attack;
    public int defense;
    public bool isPlayerCharacter;
    
    public Sprite characterSprite;
    public GameObject characterObject;

    public Character(string name, int hp, int atk, int def, bool isPlayer)
    {
        characterName = name;
        maxHP = hp;
        currentHP = hp;
        attack = atk;
        defense = def;
        isPlayerCharacter = isPlayer;
    }

    public void TakeDamage(int attackValue)
    {
        int actualDamage = Mathf.Max(1, attackValue - defense);
        currentHP -= actualDamage;
        currentHP = Mathf.Max(0, currentHP);
        Debug.Log($"{characterName} takes {actualDamage} damage! HP: {currentHP}/{maxHP}");
    }

    public bool IsAlive()
    {
        return currentHP > 0;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Min(currentHP, maxHP);
        Debug.Log($"{characterName} heals for {amount}! HP: {currentHP}/{maxHP}");
    }
}
