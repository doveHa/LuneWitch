using UnityEngine;
using UnityEngine.UI;

namespace Script.Boss.Handler
{
    public class BossHealthBarHandler : MonoBehaviour
    {
        [SerializeField] private Image healthValue;

        private float maxHp;

        void Start()
        {
            GetComponentInParent<Canvas>().worldCamera = Camera.main;
        }

        public void Init(float maxHp)
        {
            this.maxHp = maxHp;
            healthValue.fillAmount = 1.0f;
        }

        public void UpdateHealthBar(float currentHp)
        {
            if (currentHp <= 0)
            {
                return;
            }

            healthValue.fillAmount = currentHp / maxHp;
        }
    }
}