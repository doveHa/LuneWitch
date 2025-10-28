using System.Collections;
using Script.Manager;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;

    private float minSpawnDelay = 5f;
    private float maxSpawnDelay = 10f;

    public void SpawnStart()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < StageManager.Manager.SpawnCount; i++)
        {
            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSecondsRealtime(delay);

            int monsterIndex = Random.Range(0, StageManager.Manager.enemyPrefabs.Count);
            int positionIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(
                StageManager.Manager.enemyPrefabs[monsterIndex],
                spawnPoints[positionIndex].position,
                spawnPoints[positionIndex].rotation
            ).transform.parent = spawnPoints[positionIndex];
        }
    }
}