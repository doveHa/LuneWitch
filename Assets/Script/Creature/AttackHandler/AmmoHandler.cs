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

        public void SetStat(int atk, float ammoSpeed)
        {
            this.atk = atk;
            this.ammoSpeed = ammoSpeed;
        }

        public void AddForce()
        {
            Debug.Log(ammoSpeed);
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.right * ammoSpeed;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                other.GetComponentInChildren<EnemyHandler>().Hit(atk);
                Destroy(gameObject);
            }
            else if (other.CompareTag("Boss"))
            {
                other.GetComponentInChildren<BossHandler>().Hit(atk);
                Destroy(gameObject);
            }
        }
    }
}