using System.Collections;
using TMPro;
using UnityEngine;

namespace Script.Manager
{
    public class GameFlowManager : MonoBehaviour
    {
        public static GameFlowManager Manager { get; private set; }

        private int TargetCount;
        private int killCount = 0;
        private float startTime;

        [SerializeField] private GameObject RoundPanel;
        [SerializeField] private EnemySpawner spawner;
        [SerializeField] private TextMeshProUGUI elapsedTime;

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