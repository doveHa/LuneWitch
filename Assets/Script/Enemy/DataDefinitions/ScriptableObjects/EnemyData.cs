using UnityEngine;

namespace Script.Enemy.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
    public class EnemyData : ScriptableObject
    {
        public string enemyName;

        public float speed;

        public int attack;
        public int health;
        public float attackTerm;

        public GameObject creaturePrefab;
    }
}