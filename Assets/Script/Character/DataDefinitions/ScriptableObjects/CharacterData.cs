using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Character.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
    public class CharacterData : ScriptableObject
    {
        public string name;
        [TextArea] public string introduction;
        [TextArea] public string skillIntroduction;
        [TextArea] public string dialogueText;

        public GameObject prefab;
        public Sprite sdSprite;
        public Sprite ldSprite;
    }
}