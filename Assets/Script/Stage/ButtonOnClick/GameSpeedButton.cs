using TMPro;
using UnityEngine;

namespace Script.Stage.ButtonOnClick
{
    public class GameSpeedButton : Core.OnButtonClick.ButtonOnClick
    {
        private float initialSpeed;
        private float[] speedStep = { 1.0f, 2.0f, 3.0f };

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
            GetComponentInChildren<TextMeshProUGUI>().text = currentIndex.ToString();
        }
    }
}