using Script.DataDefinitions.Enum;
using UnityEngine;

namespace Script.DataDefinitions.ScriptableObjects
{
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

        public GameObject creaturePrefab;
    }
}