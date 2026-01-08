using System.Collections;
using Script.Core.Manager;
using UnityEngine;
using Script.Stage.DataDefinitions.ScriptableObjects;
using Script.Stage.Abstaractions;
using Script.Stage.Manager;

namespace Script.Stage.StageHandler
{
    public class InfinityStageHandler : StageHandlerBase
    {
        [SerializeField] private InfinityStageInfo stageInfo;
        private int currentWave = 1;

        public override void Setup(SceneReferences references)
        {
            base.Setup(references);
            stageInfo = ResourceManager.Load<InfinityStageInfo>(
                Constant.ResourcePath.INFINITY_STAGE_INFO_DATA_PATH);

            sceneReferences.waveTitle.text = stageInfo.stageTitle;
            sceneReferences.roundPanelRound.text = "";
            sceneReferences.roundPanelTitle.text = stageInfo.stageTitle;
            sceneReferences.backGroundImage.sprite = stageInfo.backGroundImage;

            SetEnemyData(stageInfo.normalEnemyPool);
            SetPlayer();
        }

        public override IEnumerator StartGame()
        {
            while (true)
            {
                sceneReferences.waveTitle.text = $"Wave {currentWave}";

                int waveEnemyCount = stageInfo.enemyCount + currentWave * 3;
                
                GameFlowManager.Manager.SetTargetCount(waveEnemyCount);
                GameFlowManager.Manager.StartWaveTimer();
                
                SpawnEnemies(waveEnemyCount);
                yield return new WaitUntil(() => GameFlowManager.Manager.IsAllKill);
                yield return new WaitForSeconds(3.0f);
                currentWave++;
            }
        }

        private void SpawnEnemies(int count)
        {
            float healthRate = 1.0f + ((currentWave - 1) * stageInfo.hpMultiplierPerWave);
            sceneReferences.spawner.SetEnemyHealthRate(healthRate);
            sceneReferences.spawner.SpawnStart(count, EnemyData);
        }
    }
}