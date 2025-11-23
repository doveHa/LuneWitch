using Script.Manager;
using TMPro;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CostManager : ManagerBase<CostManager>
    {
        [SerializeField] private TextMeshProUGUI costText;
        private int cps; //CostPerSec

        private float costTimer;
        public float Cost { get; private set; }

        protected override void Awake()
        {
            isDontDestroy = false;
            base.Awake();
            Initialize();
        }

        void FixedUpdate()
        {
            costText.text = Cost.ToString();

            costTimer += Time.deltaTime;
            if (costTimer >= 1)
            {
                Cost += cps;
                costTimer -= 1;
            }
        }

        public void AddCost(int cost)
        {
            Cost += cost;
        }

        public void UseCost(int cost)
        {
            Cost -= cost;
        }

        private void Initialize()
        {
            cps = 1;
            costTimer = 0f;
            Cost = 0;
        }
    }
}