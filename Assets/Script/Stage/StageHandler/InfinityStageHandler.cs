using System.Collections;
using Script.Core.Manager;
using UnityEngine;
using Script.Stage.DataDefinitions.ScriptableObjects;
using Script.Stage.Abstaractions;
using Script.Stage.Handler;
using Script.Stage.Manager;
using UnityEngine.UI;

namespace Script.Stage.StageHandler
{
    public class InfinityStageHandler : StageHandlerBase
    {
        [SerializeField] private InfinityStageInfo stageInfo;
        private RoundPanelHandler roundPanel;

        private int currentWave = 1;

        public override void Setup(SceneReferences references)
        {
            base.Setup(references);
            stageInfo = ResourceManager.Load<InfinityStageInfo>(
                Constant.ResourcePath.INFINITY_STAGE_INFO_DATA_PATH);

            sceneReferences.waveTitle.text = WaveText();
            sceneReferences.roundPanelRound.text = WaveText();
            sceneReferences.roundPanelTitle.text = stageInfo.stageTitle;
            sceneReferences.backGroundImage.sprite = stageInfo.backGroundImage[0];
            
            roundPanel = sceneReferences.roundPanelRound.transform.GetComponentInParent<RoundPanelHandler>();
            SetEnemyData(stageInfo.normalEnemyPool);
            SetPlayer();
        }

        public override IEnumerator StartGame()
        {
            while (true)
            {
                int backgroundIndex = (currentWave / 5) % 4;
                sceneReferences.backGroundImage.sprite = stageInfo.backGroundImage[backgroundIndex];
                SpawnCount = stageInfo.enemyCount + currentWave * 3;
                
                CountEnemyKill(0);
                GameFlowManager.Manager.SetTargetCount(SpawnCount);
                GameFlowManager.Manager.StartWaveLogic();

                SpawnEnemies(SpawnCount);
                yield return new WaitUntil(() => GameFlowManager.Manager.IsAllKill);
                yield return new WaitForSeconds(3.0f);
                Debug.Log("Next Wave");
                NextWave();
                yield return new WaitForSeconds(3.0f);
            }
        }

        private void NextWave()
        {
            currentWave++;
            sceneReferences.waveTitle.text = WaveText();
            sceneReferences.roundPanelRound.text = WaveText();
            
            roundPanel.RoundPanelActive();  
        }

        private void SpawnEnemies(int count)
        {
            float healthRate = 1.0f + ((currentWave - 1) * stageInfo.hpMultiplierPerWave);
            sceneReferences.spawner.SetEnemyHealthRate(healthRate);
            sceneReferences.spawner.SpawnStart(count, EnemyData);
        }

        private string WaveText()
        {
            return $"Wave {currentWave}";
        }
    }
}