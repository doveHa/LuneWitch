using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    SuperRare,
    UltraRare
}

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Gacha/Character")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public string characterName_Kr;
    public Sprite characterImage;
    public Rarity rarity;

    public string description;
    public string characterType;

    public int cost;
    public int attack;
    public int health;
    public bool isUnlocked = true;
}