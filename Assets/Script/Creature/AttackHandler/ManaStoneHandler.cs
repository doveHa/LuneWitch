using Script.BattleStyle.Manager;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class ManaStoneHandler : MonoBehaviour
    {
        public int bonusCostAmount = 25;
        public float productionTime = 5.0f;

        public GameObject readyIcon;

        
        private float currentTimer;
        private bool isReady = false;

        private void Start()
        {
            currentTimer = 0f;
        }

        private void Update()
        {
            if (isReady) return;

            currentTimer += Time.deltaTime;

            if (currentTimer >= productionTime)
            {
                SetReadyState(true);
            }
        }

        private void SetReadyState(bool ready)
        {
            isReady = ready;

            if (readyIcon != null)
                readyIcon.SetActive(ready);
        }

        private void OnMouseDown()
        {
            if (isReady)
            {
                CollectResource();
            }
        }

        // 코스트 획득 로직
        private void CollectResource()
        {
            CostManager.Manager.AddCost(bonusCostAmount);
            currentTimer = 0f;
            SetReadyState(false);
        }
    }
}