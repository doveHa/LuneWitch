using System.Collections.Generic;
using Script.Stage;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Manager
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private float RecoverCostPerSec;
        [SerializeField] private TextMeshProUGUI waveTitle, roundPanelRound, roundPanelTitle;
        [SerializeField] private SpriteRenderer backGroundImage;
        [SerializeField] private Transform cardSet;
        [SerializeField] private GameObject cardSlotPrefab;
        [SerializeField] private Text currentCostText;
        [SerializeField] private GameObject player;

        public List<GameObject> enemyPrefabs { get; private set; }

        public int SpawnCount { get; private set; }
        public float CurrentCost { get; private set; }

        public static StageManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }

            enemyPrefabs = new List<GameObject>();
            SetStage();
            SetPlayer();
            SetCards();
        }

        void FixedUpdate()
        {
            CurrentCost += Time.deltaTime * RecoverCostPerSec;
            currentCostText.text = ((int)CurrentCost).ToString();
        }

        public void Initialize()
        {
            CurrentCost = 0;
        }

        public void UseCost(int cost)
        {
            if (CurrentCost >= cost)
            {
                CurrentCost -= cost;
            }
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

            SetEnemyPrefab(data.enemyNames);
        }

        private void SetPlayer()
        {
            Debug.Log(PlayerManager.Manager.SelectedCharacter.ToString());
            Instantiate(ResourceManager.Load<GameObject>(
                Constant.ResourcePath.GAMEOBJECT_PATH_BY_CHARACTER_NAME(
                    PlayerManager.Manager.SelectedCharacter.ToString())
            ), player.transform).name = PlayerManager.Manager.SelectedCharacter.ToString();
        }

        private void SetCards()
        {
            foreach (CharacterData character in PlayerManager.Manager.SelectedCreatures)
            {
                GameObject obj = Instantiate(cardSlotPrefab, cardSet);
                obj.GetComponent<CardSlot>().InitializeCard(character);
            }
        }

        private void SetEnemyPrefab(string[] enemyNames)
        {
            foreach (string name in enemyNames)
            {
                enemyPrefabs.Add(
                    ResourceManager.Load<GameObject>(Constant.ResourcePath.GAMEOBJECT_PATH_BY_ENEMY_NAME(name)));
            }
        }
    }
}