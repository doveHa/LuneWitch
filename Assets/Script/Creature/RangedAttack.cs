using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Creature
{
    public class RangedAttack : MonoBehaviour
    {
        [SerializeField] private GameObject ammoPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float attackSpeed = 1f, ammoSpeed = 1f;
        private int enemyCount = 0;
        private bool isEnemyOn, isAttacking;

        private CreatureStat stat;

        void Start()
        {
            stat = GetComponentInParent<CreatureStat>();
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
                StartCoroutine(ShootAmmo());
            }
        }

        private IEnumerator ShootAmmo()
        {
            isAttacking = true;

            while (isEnemyOn)
            {
                stat.AttackMotion();
                Ammo ammo = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity).GetComponent<Ammo>();
                ammo.SetStat(stat.Attack, ammoSpeed);
                ammo.AddForce();
                yield return new WaitForSeconds(attackSpeed);
            }

            isAttacking = false;
        }
    }
}