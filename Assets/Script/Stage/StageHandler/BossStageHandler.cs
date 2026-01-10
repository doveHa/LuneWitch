using System.Collections;
using Script.Boss.Handler;
using Script.Core.Manager;
using Script.Manager;
using Script.Stage.Abstaractions;
using Script.Stage.DataDefinitions.ScriptableObjects;
using Script.Stage.Manager;
using UnityEngine;

namespace Script.Stage.StageHandler
{
    public class BossStageHandler : StageHandlerBase
    {
        public BossHandler BossHandler { get; private set; }

        public override void Setup(SceneReferences references)
        {
            base.Setup(references);

            StageInfoData data = ResourceManager.Load<StageInfoData>(
                Constant.ResourcePath.STAGE_INFO_DATA_PATH_BY_CHAPTER_ROUND(
                    SceneLoadManager.SelectedChapterNo, SceneLoadManager.SelectedRoundNo));

            sceneReferences.waveTitle.text = data.chapter + "-" + data.round;
            sceneReferences.roundPanelRound.text = "Round " + data.round;
            sceneReferences.roundPanelTitle.text = data.roundTitle;
            sceneReferences.backGroundImage.sprite = data.backGroundImage;

            SpawnCount = data.enemyCount;
            SetEnemyData(data.enemyData);
            SetPlayer();
            SummonBoss();
        }

        public override IEnumerator StartGame()
        {
            while (!BossHandler.IsDead())
            {
                BossHandler.StartBossState();
                GameFlowManager.Manager.SetTargetCount(SpawnCount);
                GameFlowManager.Manager.StartWaveLogic();

                sceneReferences.spawner.SpawnStart(SpawnCount, EnemyData);
                yield return new WaitUntil(() => GameFlowManager.Manager.IsAllKill);
            }
        }

        private void SummonBoss()
        {
            GameObject bossObject = Instantiate(ResourceManager.Load<GameObject>
                (Constant.ResourcePath.BOSS_PREFAB), transform);
            bossObject.name = "Boss";
            BossHandler = bossObject.GetComponentInChildren<BossHandler>();
        }
    }
}