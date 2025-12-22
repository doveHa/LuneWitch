using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class ShadowAttackHandler : BaseAttackHandler
    {
        private float waitTime = 0.3f;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Right());
            return attackRange;
        }

        public override void StartAttacking()
        {
            CurrentCooldown = attackTerm;
            StartCoroutine(TimeDifferenceAttack(RootCoordinate));
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
            }
        }

        private IEnumerator TimeDifferenceAttack(CardZoneCoordinate coordinate)
        {
            CardZoneCoordinate coord = coordinate;
            for (int i = 0; i < 3; i++)
            {
                coord = coord.Right();
                CardZoneHandler zone = CardZoneManager.Manager.GetZone(coord);

                if (zone.IsOnEnemy())
                {
                    Attack(zone.Enemies);
                }

                yield return new WaitForSeconds(waitTime);
            }
        }
    }
}