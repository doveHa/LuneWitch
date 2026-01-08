using System.Collections;
using Script.Character.Handler;
using Script.Core.Manager;
using Script.Manager;
using Script.Stage.Handler;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Stage.Manager
{
    public class GameFlowManager : ManagerBase<GameFlowManager>
    {
        private int targetCount, killCount = 0;
        private float startTime;
        public bool IsAllKill { get; private set; }

        [SerializeField] private GameObject RoundPanel;
        [SerializeField] private TextMeshProUGUI elapsedTime;
        [SerializeField] private GameObject EndGameScreen, GameOverScreen, GameWinScreen;

        private StageHandlerBase currentStage;
        public CharacterHandler ChHandler { get; private set; }

        protected override void Awake()
        {
            isDontDestroy = false;
            IsAllKill = false;
            base.Awake();
        }

        void Start()
        {
            currentStage = FindFirstObjectByType<StageHandlerBase>();
            StartCoroutine(GameLoop());
            ChHandler = currentStage.Player().GetComponentInChildren<CharacterHandler>();
        }

        void Update()
        {
            UpdateElapsedTime();
        }

        public EnemySpawnHandler Spawner()
        {
            return currentStage.Spawner();
        }

        public void SetTargetCount(int count)
        {
            targetCount = count;
            killCount = 0;
            IsAllKill = false;
        }

        public void KillEnemy()
        {
            killCount++;
            if (killCount >= targetCount)
            {
                IsAllKill = true;
            }
        }

        public void GameOver()
        {
            EndGameScreen.SetActive(true);
            GameOverScreen.SetActive(true);
        }


        private IEnumerator GameLoop()
        {
            yield return !RoundPanel.activeInHierarchy;
            startTime = Time.time;

            yield return StartCoroutine(currentStage.StartGame());

            HandlerGameClear();
        }

        private void HandlerGameClear()
        {
            StartCoroutine(GameEndRoutine());
        }

        private IEnumerator GameEndRoutine()
        {
            yield return new WaitForSeconds(5f);

            if (SceneLoadManager.SelectedChapterNo == 1)
            {
                GameWinScreen.SetActive(true);
            }
            else if (SceneLoadManager.SelectedChapterNo == 2)
            {
                EndGameScreen.SetActive(true);
                LoadNextStory();
            }
        }

        private void LoadNextStory()
        {
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