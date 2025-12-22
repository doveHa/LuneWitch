using UnityEngine;

namespace Script.Core.DataDefinitions.ScriptableObjects
{
    public class BaseData : ScriptableObject
    {
        public string name;
        public int attack;
        public int health;
        public float attackTerm;

        public GameObject prefab;
    }
}