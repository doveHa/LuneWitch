using System.Collections;
using System.Collections.Generic;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Stage.Handler
{
    public class EnemySpawnHandler : MonoBehaviour
    {
        private EnemySpawnPointHandler[] spawnPoints;
        private float healthRate = 0f;

        void Start()
        {
            spawnPoints = GetComponentsInChildren<EnemySpawnPointHandler>();
        }

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
            Transform[] spawnPoints = new Transform[this.spawnPoints.Length];
            for (int i = 0; i < this.spawnPoints.Length; i++)
            {
                spawnPoints[i] = this.spawnPoints[i].transform;
            }

            return spawnPoints;
        }

        public void StopSpawning()
        {
            StopCoroutine("SpawnEnemies");
        }

        private IEnumerator SpawnEnemies(int spawnCount, List<EnemyData> enemies)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                float randomInterval = Random.Range(Constant.BattleSystem.MIN_SPAWN_TERM, Constant.BattleSystem.MAX_SPAWN_TERM);

                yield return new WaitForSeconds(randomInterval);

                int enemyIndex = Random.Range(0, enemies.Count);
                int positionIndex = Random.Range(0, spawnPoints.Length);

                EnemyData data = enemies[enemyIndex];

                spawnPoints[positionIndex].ShowWarningStep();
                yield return new WaitUntil(() => !spawnPoints[positionIndex].IsPlayingParticle());

                GameObject enemy = Instantiate(
                    data.prefab,
                    spawnPoints[positionIndex].transform.position,
                    spawnPoints[positionIndex].transform.rotation
                );
                enemy.transform.parent = spawnPoints[positionIndex].transform;

                var handler = enemy.GetComponentInChildren<EnemyHandler>();
                handler.Initialize(data);
                handler.HealthHandler.HealthRateUpgrade(healthRate);
            }
        }
    }
}