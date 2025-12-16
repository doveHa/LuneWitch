using System.Collections;
using System.Collections.Generic;
using Script.Enemy;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
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
                data.creaturePrefab,
                spawnPoints[positionIndex].position,
                spawnPoints[positionIndex].rotation
            );
            enemy.transform.parent = spawnPoints[positionIndex];
            enemy.GetComponent<EnemyHandler>().Initialize(data);
            
            spawnProgressSlider.value++;
        }
    }
}