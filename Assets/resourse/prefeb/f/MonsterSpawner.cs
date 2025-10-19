using System.Collections;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public Transform[] spawnPoints;
    public GameObject stopConditionObject; // �� ������Ʈ�� ������ ����

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
            // Ư�� ������Ʈ�� �����ٸ� �ߴ�
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
