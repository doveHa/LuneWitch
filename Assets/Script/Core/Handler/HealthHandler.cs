namespace Script.Core.Handler
{
    public class HealthHandler
    {
        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public bool IsDead { get; private set; } = false;

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
            MaxHealth += health;
            Health += MaxHealth - Health;
        }

        public void Hit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                IsDead = true;
            }
        }
    }
}