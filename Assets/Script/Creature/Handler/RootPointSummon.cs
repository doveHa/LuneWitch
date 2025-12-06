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
                StartCoroutine(EnemyDisSpeed(other.GetComponentInChildren<EnemyStat>()));
                Destroy(GetComponent<Collider2D>());
            }
        }

        private IEnumerator EnemyDisSpeed(EnemyStat stat)
        {
            stat.Hit(damage);
            float originalSpeed = stat.Speed;
            stat.Speed *= disSpeedRate;
            yield return new WaitForSeconds(slowTime);
            stat.Speed = originalSpeed;
            Destroy(gameObject);
        }
    }
}