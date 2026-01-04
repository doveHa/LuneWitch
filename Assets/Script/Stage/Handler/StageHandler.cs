using System.Collections;
using Script.BattleStyle.Manager;
using Script.Core.Manager;
using Script.Manager;
using Script.Stage.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Stage.Manager
{
    public class StageHandler : StageHandlerBase
    {
        void Start()
        {
            CardPoolManager.Manager.InitialCreature(PlayerManager.Manager.SelectedCreatures);
            CardPoolManager.Manager.ReRoll();
        }

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
            SetEnemyData(data.enemyDatas);
            SetPlayer();
        }

        public override IEnumerator StartGame()
        {
            GameFlowManager.Manager.SetTargetCount(SpawnCount);
            sceneReferences.spawner.SpawnStart(SpawnCount, EnemyData);
            yield return new WaitUntil(() => GameFlowManager.Manager.IsAllKill);
        }
    }
}