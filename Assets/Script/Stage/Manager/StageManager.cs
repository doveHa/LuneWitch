using System.Collections.Generic;
using Script.BattleStyle.Manager;
using Script.Core.Manager;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Manager;
using Script.Stage.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;

namespace Script.Stage.Manager
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private float RecoverCostPerSec;
        [SerializeField] private TextMeshProUGUI waveTitle, roundPanelRound, roundPanelTitle;
        [SerializeField] private SpriteRenderer backGroundImage;
        [SerializeField] private GameObject player;

        public List<EnemyData> EnemyData { get; private set; }

        public int SpawnCount { get; private set; }

        public static StageManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }

            SetStage();
            SetPlayer();
        }

        void Start()
        {
            CardPoolManager.Manager.InitialCreature(PlayerManager.Manager.SelectedCreatures);
        }

        public GameObject Player()
        {
            return player;
        }

        private void SetStage()
        {
            StageInfoData data = ResourceManager.Load<StageInfoData>(
                Constant.ResourcePath.STAGE_INFO_DATA_PATH_BY_CHAPTER_ROUND(
                    SceneLoadManager.SelectedChapterNo, SceneLoadManager.SelectedRoundNo));
            string chapter_round = data.chapter + "-" + data.round;
            waveTitle.text = chapter_round;
            roundPanelRound.text = "Round " + data.round;
            roundPanelTitle.text = data.roundTitle;
            backGroundImage.sprite = data.backGroundImage;
            SpawnCount = data.enemyCount;

            SetEnemyData(data.enemyDatas);
        }

        private void SetPlayer()
        {
            Instantiate(PlayerManager.Manager.SelectedCharacter.prefab, player.transform).name =
                PlayerManager.Manager.SelectedCharacter.name;
        }

        private void SetEnemyData(EnemyData[] enemies)
        {
            EnemyData = new List<EnemyData>(enemies);
        }
    }
}