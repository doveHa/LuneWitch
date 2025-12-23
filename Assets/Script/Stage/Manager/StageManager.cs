using System.Collections.Generic;
using Script.Core.Manager;
using Script.DataDefinitions.ScriptableObjects;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Manager;
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

            EnemyData = new List<EnemyData>();
            SetStage();
            SetPlayer();
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

            SetEnemyData(data.enemyNames);
        }

        private void SetPlayer()
        {
            Debug.Log(PlayerManager.Manager.SelectedCharacter.ToString());
            Instantiate(ResourceManager.Load<GameObject>(
                Constant.ResourcePath.GAMEOBJECT_PATH_BY_CHARACTER_NAME(
                    PlayerManager.Manager.SelectedCharacter.ToString())
            ), player.transform).name = PlayerManager.Manager.SelectedCharacter.ToString();
        }

        private void SetEnemyData(string[] enemyNames)
        {
            foreach (string name in enemyNames)
            {
                EnemyData.Add(
                    ResourceManager.Load<EnemyData>(Constant.ResourcePath.ENEMY_PATH_BY_ENEMY_NAME(name)));

                Debug.Log($"Add {name}");
            }
        }
    }
}