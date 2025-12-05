using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Creature.Handler
{
    public class RangedAttackHandler : MonoBehaviour
    {
        [SerializeField] private GameObject ammoPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float attackSpeed = 1f, ammoSpeed = 1f;
        private int enemyCount = 0;
        private bool isEnemyOn, isAttacking;

        private HitHandler stat;

        void Start()
        {
            stat = GetComponentInParent<HitHandler>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyCount++;
                isEnemyOn = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyCount--;
                if (enemyCount <= 0)
                {
                    isEnemyOn = false;
                }
            }
        }

        void Update()
        {
            if (isEnemyOn && !isAttacking)
            {
                StartCoroutine(AttackMotionCoroutine());
            }
        }

        public void ShootAmmo()
        {
            AmmoHandler ammoHandler = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity).GetComponent<AmmoHandler>();
            ammoHandler.SetStat(stat.Attack, ammoSpeed);
            ammoHandler.AddForce();
        }

        private IEnumerator AttackMotionCoroutine()
        {
            isAttacking = true;

            while (isEnemyOn)
            {
                stat.AttackMotion();
                yield return new WaitForSeconds(attackSpeed);
            }

            isAttacking = false;
        }
    }
}