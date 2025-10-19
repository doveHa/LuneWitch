using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform[] spawnPoints;
    public GameObject stopConditionObject; // 이 오브젝트가 켜지면 멈춤

    public int spawnCount = 10;
    public float minSpawnDelay = 1f;
    public float maxSpawnDelay = 4f;

    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 특정 오브젝트가 켜졌다면 중단
            if (stopConditionObject.activeSelf)
                yield break;

            float delay = Random.Range(minSpawnDelay, maxSpawnDelay);
            yield return new WaitForSecondsRealtime(delay);

            int monsterIndex = Random.Range(0, monsterPrefabs.Length);
            int positionIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(
                monsterPrefabs[monsterIndex],
                spawnPoints[positionIndex].position,
                spawnPoints[positionIndex].rotation
            );
        }
    }
}
