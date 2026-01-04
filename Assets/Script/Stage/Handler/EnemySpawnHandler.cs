using System.Collections;
using System.Collections.Generic;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Stage.Handler
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        [SerializeField] private Slider spawnProgressSlider;
        [SerializeField] private Transform[] spawnPoints;
        private float healthRate = 1.0f;
        private float minSpawnDelay = 5f;
        private float maxSpawnDelay = 10f;

        public void SpawnStart(int spawnCount, List<EnemyData> enemies)
        {
            StartCoroutine(SpawnEnemies(spawnCount, enemies));
        }

        public void SetEnemyHealthRate(float healthRate)
        {
            this.healthRate = healthRate;
        }

        public Transform[] SpawnPoints()
        {
            return spawnPoints;
        }

        private IEnumerator SpawnEnemies(int spawnCount, List<EnemyData> enemies)
        {
            spawnProgressSlider.maxValue = spawnCount;

            for (int i = 0; i < spawnCount; i++)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSecondsRealtime(delay);

                int enemyIndex = Random.Range(0, enemies.Count);
                int positionIndex = Random.Range(0, spawnPoints.Length);

                EnemyData data = enemies[enemyIndex];

                GameObject enemy = Instantiate(
                    data.prefab,
                    spawnPoints[positionIndex].position,
                    spawnPoints[positionIndex].rotation
                );
                enemy.transform.parent = spawnPoints[positionIndex];
                enemy.GetComponentInChildren<EnemyHandler>().Initialize(data);
                enemy.GetComponentInChildren<EnemyHandler>().HealthHandler.HealthUpgrade(healthRate);

                spawnProgressSlider.value++;
            }
        }
    }
}