using UnityEngine;

namespace Script.Enemy
{
    public class SpawnManager : MonoBehaviour
    {
        public static SpawnManager Manager { get; private set; }

        [SerializeField] private EnemySpawner[] spawnPool;

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }
    }
}