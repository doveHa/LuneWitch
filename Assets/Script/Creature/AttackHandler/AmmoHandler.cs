using Script.Boss.Handler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class AmmoHandler : MonoBehaviour
    {
        private int atk;
        private float ammoSpeed;
        private bool isFired = false;

        public void SetStat(int atk, float ammoSpeed)
        {
            this.atk = atk;
            this.ammoSpeed = ammoSpeed;
        }

        public void Fire()
        {
            isFired = true;
        }

        void Update()
        {
            if (isFired)
            {
                transform.Translate(Vector2.right * ammoSpeed * Time.deltaTime);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                var enemy = other.GetComponentInChildren<EnemyHandler>();
                if (enemy != null)
                {
                    enemy.Hit(atk);
                }

                Destroy(gameObject);
            }
            else if (other.CompareTag("Boss"))
            {
                var boss = other.GetComponentInChildren<BossHandler>();
                if (boss != null)
                {
                    boss.Hit(atk);
                }
                
                Destroy(gameObject);
            }
        }
    }
}