using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.Manager
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Manager { get; private set; }

        public int CurrentEnemy { get; set; }
        private int TargetCount;
        private int killCount = 0;
        private float startTime;

        [SerializeField] private GameObject RoundPanel;
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private TextMeshProUGUI elapsedTime;

        [SerializeField] private GameObject EndGameScreen, GameOverScreen, GameWinScreen;


        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
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
            if (killCount >= TargetCount)
            {
                EndGame();
            }
        }

        public void GameOver()
        {
            EndGameScreen.SetActive(true);
            GameOverScreen.SetActive(true);
        }

        private IEnumerator StartGame()
        {
            yield return new WaitUntil(() => !RoundPanel.activeInHierarchy);

            Debug.Log("Starting Game");
            StageManager.Manager.Initialize();
            TargetCount = StageManager.Manager.SpawnCount;
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