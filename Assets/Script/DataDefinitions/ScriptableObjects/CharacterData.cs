using UnityEngine;

namespace Script.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Gacha/Character")]
    public class CharacterData : ScriptableObject
    {
        public string characterName;
        public string characterName_Kr;
        [TextArea] public string description;
        
        public int cost;
        public int attack;
        public int health;
        public float attackTerm;

        public GameObject creaturePrefab;
        public Sprite characterImage;
        
    }
}