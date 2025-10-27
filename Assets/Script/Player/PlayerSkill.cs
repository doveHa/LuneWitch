using UnityEngine;
using UnityEngine.UI;

namespace Script.Player
{
    public class PlayerSkill : MonoBehaviour
    {
        [SerializeField] private Button skillButton;

        [SerializeField] private float maxGauge = 100f;
        [SerializeField] private float recoverPerSecond = 10f;

        private float currentGauge;

        void Start()
        {
            currentGauge = 0f;
            UpdateUI();
        }

        void Update()
        {
            RecoverGauge(Time.deltaTime);
            UpdateUI();
        }

        private void RecoverGauge(float deltaTime)
        {
            if (currentGauge < maxGauge)
            {
                currentGauge += recoverPerSecond * deltaTime;
                currentGauge = Mathf.Min(currentGauge, maxGauge);
            }
        }

        private void UpdateUI()
        {
            skillButton.image.fillAmount = currentGauge / maxGauge;
            skillButton.interactable = (currentGauge >= maxGauge); // 최대 도달 시 활성화
        }

        public void OnSkillUse()
        {
            if (currentGauge >= maxGauge)
            {
                // 스킬 발동
                Debug.Log("스킬 사용!");
                currentGauge = 0f;
                UpdateUI();
            }
        }
    }
}