using UnityEngine;

namespace Script.Core.Handler
{
    public class HealthHandler
    {
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public bool IsDead { get; private set; } = false;

        public int HealthUpgradeCount { get; private set; } = 0;

        protected HealthHandler(int maxHealth)
        {
            MaxHealth = maxHealth;
            Health = maxHealth;
        }

        public void HealthRateUpgrade(float upgradeRate)
        {
            MaxHealth += (int)(MaxHealth * upgradeRate);
            Health += MaxHealth - Health;
        }

        public void HealthAddUpgrade(int health)
        {
            HealthUpgradeCount += health;

            MaxHealth += health;
            Health += MaxHealth - Health;
        }

        public void Hit(int damage)
        {
            Health -= damage;
            Debug.Log($"Health: {Health}/{MaxHealth}");
            if (Health <= 0)
            {
                Debug.Log("Dead");
                IsDead = true;
            }
        }
    }
}