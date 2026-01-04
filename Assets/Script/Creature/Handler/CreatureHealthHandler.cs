using Script.Core.Handler;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureHealthHandler : HealthHandler
    {
        private int cost;

        public CreatureHealthHandler(int maxHealth, int cost) : base(maxHealth)
        {
            this.cost = cost;
        }

        public int SellCost()
        {
            if (MaxHealth <= 0)
            {
                return 0;
            }

            float ratio = (float)Health / MaxHealth;

            if (ratio >= 0.8f)
            {
                return cost;
            }
            else if (ratio >= 0.3f)
            {
                return Mathf.FloorToInt(cost * 0.5f);
            }
            else
            {
                return 0;
            }
        }
    }
}