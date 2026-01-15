using Script.Stage.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Character.ButtonOnClick
{
    public class CharacterSkillButton : Core.OnButtonClick.ButtonOnClick
    {
        private float maxGauge = 60f;
        private float recoverPerSecond = 1f;

        private float currentGauge;
        private Button skillButton;

        void Start()
        {
            currentGauge = 0f;
            skillButton = GetComponent<Button>();
            UIUpdate();
        }

        void Update()
        {
            if (currentGauge < maxGauge)
            {
                skillButton.interactable = false;
                RecoverGauge(Time.deltaTime);
                UIUpdate();
            }
            else
            {
                skillButton.interactable = true;
                GameFlowManager.Manager.ChHandler.SkillOn();
            }

            RecoverGauge(Time.deltaTime);
        }

        protected override void OnClick()
        {
            currentGauge = 0f;
            GameFlowManager.Manager.ChHandler.UseSkill();
        }

        private void RecoverGauge(float deltaTime)
        {
            currentGauge += recoverPerSecond * deltaTime;
            currentGauge = Mathf.Min(currentGauge, maxGauge);
        }

        private void UIUpdate()
        {
            skillButton.image.fillAmount = currentGauge / maxGauge;
        }
    }
}