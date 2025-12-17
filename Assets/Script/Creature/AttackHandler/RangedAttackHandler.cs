using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.AttackHandler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class RangedAttackHandler : AttackHandler.AttackHandler
    {
        [SerializeField] private GameObject ammoPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float attackSpeed = 1f, ammoSpeed = 1f;
        private int enemyCount = 0;
        private bool isEnemyOn, isAttacking;

        private CombatHandler stat;

        void Start()
        {
            stat = GetComponentInParent<CombatHandler>();
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

        protected override List<CardZoneCoordinate> AttackRanges()
        {
            var attackRange = new List<CardZoneCoordinate>();
            CardZoneCoordinate coordinate = RootCoordinate;

            for (int i = RootCoordinate.Row; i < CardZoneManager.ROW; i++)
            {
                coordinate = coordinate.Right();
                attackRange.Add(coordinate);
            }

            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            throw new System.NotImplementedException();
        }

        public void ShootAmmo()
        {
            AmmoHandler ammoHandler = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity)
                .GetComponent<AmmoHandler>();
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