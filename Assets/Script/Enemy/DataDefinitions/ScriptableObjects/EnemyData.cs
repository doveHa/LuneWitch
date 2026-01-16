using Script.Core.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Enemy.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewEnemy", menuName = "Enemy")]
    public class EnemyData : BaseData
    {
        public float moveSpeed;
    }
}