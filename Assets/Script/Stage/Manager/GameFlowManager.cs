using System.Collections;
using Script.Core.Manager;
using Script.Manager;
using Script.Stage.Handler;
using TMPro;
using UnityEngine;

namespace Script.Stage.Manager
{
    public class GameFlowManager : ManagerBase<GameFlowManager>
    {

        public int CurrentEnemy { get; set; }
        private int targetCount, killCount = 0;
        private float startTime;

        [SerializeField] private GameObject RoundPanel;
        [SerializeField] private EnemySpawnHandler spawner;
        [SerializeField] private TextMeshProUGUI elapsedTime;

        [SerializeField] private GameObject EndGameScreen, GameOverScreen, GameWinScreen;


        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            StartCoroutine(StartGame());
        }

        void Update()
        {
            UpdateElapsedTime();
        }

        public void KillEnemy()
        {
            killCount++;
            if (killCount >= targetCount)
            {
                StartCoroutine(WaitTime());
            }
        }

        private IEnumerator WaitTime()
        {
            yield return new WaitForSeconds(5f);
            EndGame();
        }

        public void GameOver()
        {
            EndGameScreen.SetActive(true);
            GameOverScreen.SetActive(true);
        }

        public EnemySpawnHandler Spawner()
        {
            return spawner;
        }

        private IEnumerator StartGame()
        {
            yield return new WaitUntil(() => !RoundPanel.activeInHierarchy);

            Debug.Log("Starting Game");
            targetCount = StageManager.Manager.SpawnCount;
            spawner.SpawnStart();

            startTime = Time.time;
        }


        private void EndGame()
        {
            //EndGameScreen.SetActive(true);
            if (SceneLoadManager.SelectedChapterNo == 1)
            {
                GameWinScreen.SetActive(true);
            }
            else if (SceneLoadManager.SelectedChapterNo == 2)
            {
                EndGameScreen.SetActive(true);
                switch (SceneLoadManager.SelectedRoundNo)
                {
                    case 1:
                        SceneLoadManager.Manager.LoadStory("Chapter 2 Story 1");
                        break;
                    case 2:
                        SceneLoadManager.Manager.LoadStory("Chapter 2 Story 4");
                        break;
                    case 3:
                        SceneLoadManager.Manager.LoadStory("Chapter 2 Story 5");
                        break;
                }
            }

            Debug.Log("Ending Game");
        }

        private void UpdateElapsedTime()
        {
            if (elapsedTime == null) return;

            float elapsed = Time.time - startTime;
            int minutes = Mathf.FloorToInt(elapsed / 60f);
            int seconds = Mathf.FloorToInt(elapsed % 60f);

            elapsedTime.text = $"{minutes:00}:{seconds:00}";
        }
    }
}