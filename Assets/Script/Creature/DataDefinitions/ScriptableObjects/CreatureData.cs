using Script.Core.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Creature.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewCreature", menuName = "Creature")]
    public class CreatureData : BaseData
    {
        public bool isActive;
        public string name_Kr;
        [TextArea] public string description;

        public int cost;

        public Sprite characterImage;

        public Vector2Int unitSize;
        public Sprite Thumbnail;
    }
}