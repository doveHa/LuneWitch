using System.Collections.Generic;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Stage.DataDefinitions.ScriptableObjects
{
    [CreateAssetMenu(fileName = "StageInfo", menuName = "StageInfo/InfinityStageInfo")]
    public class InfinityStageInfo : ScriptableObject
    {
        public string stageTitle = "무한의 탑";
        public Sprite backGroundImage;

        public EnemyData[] normalEnemyPool;
        public List<EnemyData> bossEnemyPool;

        public int bossWaveInterval = 5;
        public float hpMultiplierPerWave = 0.1f;
        public float atkMultiplierPerWave = 0.05f;
        public int spawnCountIncreaseRate = 1;

        public int rewardCostPerWave = 50;
        public int enemyCount = 15;
    }
}