using UnityEngine;

public enum Rarity { Common, Rare, SuperRare, UltraRare }

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Gacha/Character")]
public class CharacterData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string characterName;
    public Sprite characterImage;
    public Rarity rarity;

    [TextArea]
    public string description;
    public string characterType;

    [Header("ī��/�� ����")]
    public int cost;
    public bool isUnlocked = true;

    [Header("UI ǥ�ÿ�")]
    public Sprite cardSprite; // �� ���Կ� ǥ�õ� ī�� �̹���
}
