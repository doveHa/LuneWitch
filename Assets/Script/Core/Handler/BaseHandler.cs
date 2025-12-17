using UnityEngine;

namespace Script.Core.Handler
{
    public class BaseHandler : MonoBehaviour
    {
        private int health;
        public bool IsDead { get; private set; } = false;

        public void Initialize(int health)
        {
            this.health = health;
        }
        
        public void Hit(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                IsDead = true;
            }
        }
    }
}