using UnityEngine;

namespace Script.Character.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Character")]
    public class CharacterData : ScriptableObject
    {
        public string name;
        [TextArea] public string introduction;
        [TextArea] public string skillIntroduction;

        public GameObject prefab;
        public Sprite sprite;
    }
}