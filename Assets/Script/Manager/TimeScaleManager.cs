using Script.Core.Manager;
using UnityEngine;

namespace Script.Manager
{
    public class TimeScaleManager : ManagerBase<TimeScaleManager>
    {
        public float CurrentGameSpeed { get; private set; } = 1.0f;
        private float initialFixedDeltaTime;

        private float saveTimeSclae = 1;

        protected override void Awake()
        {
            isDontDestroy = false; // 씬 전환 시 파괴
            base.Awake();
            initialFixedDeltaTime = Time.fixedDeltaTime;
        }

        void Start()
        {
            SetSpeed(1.0f);
        }

        public void SetSpeed(float speed)
        {
            CurrentGameSpeed = speed;

            if (Time.timeScale != 0)
            {
                ApplyTimeScale();
            }
        }

        private void ApplyTimeScale()
        {
            Time.timeScale = CurrentGameSpeed;
            Time.fixedDeltaTime = initialFixedDeltaTime * CurrentGameSpeed;
        }

        public void PauseGame()
        {
            //saveTimeSclae = Time.timeScale;
            Time.timeScale = 0f;
            
        }

        public void ResumeGame()
        {
            //Time.timeScale = saveTimeSclae;
            ApplyTimeScale();
        }
    }
}