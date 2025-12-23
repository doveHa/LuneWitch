using System.Collections;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using Script.Stage.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Stage.Handler
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        [SerializeField] private Slider spawnProgressSlider;
        [SerializeField] private Transform[] spawnPoints;

        private float minSpawnDelay = 5f;
        private float maxSpawnDelay = 10f;

        public void SpawnStart()
        {
            StartCoroutine(SpawnEnemies());
        }

        public Transform[] SpawnPoints()
        {
            return spawnPoints;
        }

        private IEnumerator SpawnEnemies()
        {
            spawnProgressSlider.maxValue = StageManager.Manager.SpawnCount;

            for (int i = 0; i < StageManager.Manager.SpawnCount; i++)
            {
                float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
                yield return new WaitForSecondsRealtime(delay);

                int enemyIndex = Random.Range(0, StageManager.Manager.EnemyData.Count);
                int positionIndex = Random.Range(0, spawnPoints.Length);

                EnemyData data = StageManager.Manager.EnemyData[enemyIndex];

                GameObject enemy = Instantiate(
                    data.prefab,
                    spawnPoints[positionIndex].position,
                    spawnPoints[positionIndex].rotation
                );
                enemy.transform.parent = spawnPoints[positionIndex];
                enemy.GetComponentInChildren<EnemyHandler>().Initialize(data);

                spawnProgressSlider.value++;
            }
        }
    }
}