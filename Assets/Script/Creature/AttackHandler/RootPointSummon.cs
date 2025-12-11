using System.Collections;
using Script.Enemy;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class RootPointSummon : MonoBehaviour
    {
        [SerializeField] private float slowTime = 2;
        private int damage;
        [SerializeField] private float disSpeedRate = 0.5f;

        void Start()
        {
        }

        public void SetStat(int damage)
        {
            this.damage = damage;
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHandler enemyHandler = other.GetComponentInChildren<EnemyHandler>();
                enemyHandler.Hit(damage);
                enemyHandler.DisSpeed(disSpeedRate, slowTime);
                Destroy(gameObject);
            }
        }
    }
}