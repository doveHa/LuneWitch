using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using Script.Manager;
using UnityEngine;

namespace Temp
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private EnemyData data;
        
        
        public void Spawn()
        {
            GameObject enemy = Instantiate(
                data.prefab,
                parent.position,
                Quaternion.identity
            );
                
            enemy.transform.parent = parent;
            enemy.GetComponentInChildren<EnemyHandler>().Initialize(data);
        }
    }
}