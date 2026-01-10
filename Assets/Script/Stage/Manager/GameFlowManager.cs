using System.Collections;
using Script.Character.Handler;
using Script.Core.Manager;
using Script.Manager;
using Script.Stage.Abstaractions;
using Script.Stage.Handler;
using Script.Stage.StageHandler;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Stage.Manager
{
    public class GameFlowManager : ManagerBase<GameFlowManager>
    {
        [SerializeField] private Slider waveProgressSlider;
        [SerializeField] private GameObject RoundPanel;
        [SerializeField] private TextMeshProUGUI elapsedTime;
        [SerializeField] private GameObject EndGameScreen, GameOverScreen, GameWinScreen;

        private int targetCount, killCount = 0;
        private float startTime;
        public bool IsAllKill { get; private set; }

        private float sliderSpeed;
        private bool isWaveInProgress;

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
            ChHandler = currentStage.Player().GetComponentInChildren<CharacterHandler>();
            StartCoroutine(GameLoop());
        }

        void Update()
        {
            UpdateElapsedTime();
            UpdateWaveProgress();
        }

        public void StartWaveLogic()
        {
            waveProgressSlider.maxValue = targetCount;
            waveProgressSlider.value = 0f;

            float avgInterval = (Constant.BattleSystem.MIN_SPAWN_TERM + Constant.BattleSystem.MAX_SPAWN_TERM) / 2;

            sliderSpeed = 1.0f / avgInterval;
            isWaveInProgress = true;
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
            if (currentStage.GetType() == typeof(BossStageHandler))
            {
                var bossHandler = (currentStage as BossStageHandler).BossHandler;

                if (bossHandler != null && !bossHandler.IsDead())
                {
                    bossHandler.Hit(Constant.Boss.ENEMY_KILL_DMG);
                }
            }

            if (killCount >= targetCount)
            {
                IsAllKill = true;
            }
        }

        public void AllKill()
        {
            IsAllKill = true;
        }

        public void GameOver()
        {
            EndGameScreen.SetActive(true);
            GameOverScreen.SetActive(true);
            ChHandler.GameOver();
        }

        private void UpdateWaveProgress()
        {
            if (!isWaveInProgress)
            {
                return;
            }

            float visualLimit = killCount + 1;

            if (waveProgressSlider.value < visualLimit)
            {
                waveProgressSlider.value += sliderSpeed * Time.deltaTime * 0.5f;
            }
            else
            {
                waveProgressSlider.value = visualLimit;
            }
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

        //Story가 하나의 Scene으로 통합되면 아래 함수를 수정하여 연결
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