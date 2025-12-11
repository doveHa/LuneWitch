using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class RecognizePointAttackHandler : MonoBehaviour
    {
        [SerializeField] private GameObject pointSummonPrefab;
        [SerializeField] private float attackSpeed = 1f;
        //private int enemyCount = 0;
        private bool isAttacking;

        private CombatHandler stat;
        private List<Transform> enemies;
        
        void Start()
        {
            stat = GetComponentInParent<CombatHandler>();
            enemies = new List<Transform>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Add(other.transform);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Remove(other.transform);
            }
        }

        void Update()
        {
            if (isAttacking)
            {
                return;
            }
            
            if (enemies.Count > 0)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            isAttacking = true;

            while (enemies.Count > 0)
            {
                stat.AttackMotion();
                Transform nearestTransform = enemies[0];
                foreach (Transform enemyTransform in enemies)
                {
                    float nearestDistance = Vector3.Distance(transform.position, nearestTransform.position);
                    float targetDistance = Vector3.Distance(transform.position, enemyTransform.position);
                    if (nearestDistance > targetDistance){
                        nearestTransform = enemyTransform;
                    }
                }
                
                Instantiate(pointSummonPrefab, nearestTransform.position, Quaternion.identity).GetComponent<RootPointSummon>().SetStat(stat.Attack);
                yield return new WaitForSeconds(attackSpeed);
            }

            isAttacking = false;
        }
    }
}