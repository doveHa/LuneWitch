using Script.Core.Manager;
using UnityEngine;

namespace Script.Manager
{
    public class TimeScaleManager : ManagerBase<TimeScaleManager>
    {
        private float saveTimeSclae = 1;
        protected override void Awake()
        {
            isDontDestroy = false;
            base.Awake();
        }

        void Start()
        {
            Time.timeScale = 1;
        }

        public void PauseGame()
        {
            saveTimeSclae = Time.timeScale;
            Time.timeScale = 0;
            
        }

        public void ResumeGame()
        {
            Time.timeScale = saveTimeSclae;
        }
    }
}