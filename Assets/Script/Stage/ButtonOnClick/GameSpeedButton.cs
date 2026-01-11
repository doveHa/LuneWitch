using TMPro;
using UnityEngine;

namespace Script.Stage.ButtonOnClick
{
    public class GameSpeedButton : Core.OnButtonClick.ButtonOnClick
    {
        private float initialSpeed;
        private float[] speedStep = { 1.0f, 2.0f, 3.0f };
        private string[] speedText = { "1X", "2X", "3X" };
        private int currentIndex = 0;

        void Start()
        {
            initialSpeed = Time.fixedDeltaTime;
        }

        protected override void OnClick()
        {
            currentIndex++;

            currentIndex %= speedStep.Length;
            Time.timeScale = speedStep[currentIndex];
            Time.fixedDeltaTime = initialSpeed * speedStep[currentIndex];
            GetComponentInChildren<TextMeshProUGUI>().text = speedText[currentIndex];
        }

        // H: 일시정지 해제 시 배속 유지하기 위한 메서드
        public void ApplyCurrentSpeed()
        {
            Time.timeScale = speedStep[currentIndex];
            Time.fixedDeltaTime = initialSpeed * speedStep[currentIndex];
        }
    }
}