using System.Collections;
using System.Collections.Generic;
using Script.Enemy;
using Script.Manager;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Slider spawnProgressSlider;
    [SerializeField] private Transform[] spawnPoints;

    private float minSpawnDelay = 5f;
    private float maxSpawnDelay = 10f;

    public void SpawnStart()
    {
        StartCoroutine(SpawnMonsters());
    }

    public Transform[] SpawnPoints()
    {
        return spawnPoints;
    }

    private IEnumerator SpawnMonsters()
    {
        spawnProgressSlider.maxValue = StageManager.Manager.SpawnCount;

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
            spawnProgressSlider.value++;
        }
    }
}