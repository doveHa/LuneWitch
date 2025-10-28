using System.Collections;
using Script.Manager;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    private float minSpawnDelay = 1f;
    private float maxSpawnDelay = 4f;
    
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

            int monsterIndex = Random.Range(0, StageManager.Manager.monsterPrefabs.Length);
            int positionIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(
                StageManager.Manager.monsterPrefabs[monsterIndex],
                spawnPoints[positionIndex].position,
                spawnPoints[positionIndex].rotation
            );
        }
    }
}