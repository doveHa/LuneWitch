using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Enemy;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class BatAttackHandler : AttackHandler
    {
        protected override IEnumerator AttackCoroutine()
        {
            isAttacking = true;

            List<CardZoneCoordinate> attackRange = new List<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Left());
            attackRange.Add(RootCoordinate.Left().Left());
            attackRange.Add(RootCoordinate.Left().Up());
            attackRange.Add(RootCoordinate.Left().Down());
            
            while (enemies.Count > 0)
            {
                CombatHandler.AttackMotion();
                CardZoneManager.Manager.HitDamage(attackRange, CombatHandler.Attack);
                foreach (Transform enemyTransform in enemies)
                {
                    enemyTransform.GetComponent<EnemyHandler>().Hit(CombatHandler.Attack);
                }

                yield return new WaitForSeconds(attackSpeed);
            }

            isAttacking = false;
        }
    }
}