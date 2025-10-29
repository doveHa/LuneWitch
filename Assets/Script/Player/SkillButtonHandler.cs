using System.Collections;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Player
{
    public class SkillButtonHandler : MonoBehaviour
    {
        [SerializeField] private Button skillButton;

        [SerializeField] private float maxGauge = 100f;
        [SerializeField] private float recoverPerSecond = 10f;

        private float currentGauge;
        private PlayerAnimationController animationController;


        void Start()
        {
            animationController = GetComponentInChildren<PlayerAnimationController>();
            currentGauge = 0f;
            UIUpdate();
            skillButton.onClick.AddListener(OnSkillUse);
        }

        void Update()
        {
            if (currentGauge < maxGauge)
            {
                RecoverGauge(Time.deltaTime);
                UIUpdate();
            }
            else
            {
                skillButton.interactable = true;
                animationController.SkillOnAnimation();
            }

            RecoverGauge(Time.deltaTime);
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

        private void OnSkillUse()
        {
            animationController.ActiveSkillAnimation();
            StartCoroutine(WaitSkillUse());
        }

        private IEnumerator WaitSkillUse()
        {
            StageManager.Manager.Player().GetComponentInChildren<IPlayerSkill>().OnSkillUse();
            yield return new WaitUntil(() => animationController.IsSkillEnd);
            animationController.ReturnIdle();
            currentGauge = 0f;
            UIUpdate();
        }
    }
}