using UnityEngine;

public enum Rarity { Common, Rare, SuperRare, UltraRare }

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Gacha/Character")]
public class CharacterData : ScriptableObject
{
    [Header("기본 정보")]
    public string characterName;
    public Sprite characterImage;
    public Rarity rarity;

    [TextArea]
    public string description;
    public string characterType;

    [Header("카드/덱 관련")]
    public int cost;
    public bool isUnlocked = true;

    [Header("UI 표시용")]
    public Sprite cardSprite; // ★ 슬롯에 표시될 카드 이미지
}
