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

        private bool nextAttack = false;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Right());
            attackRange.Add(RootCoordinate.Right().Right());
            attackRange.Add(RootCoordinate.Right().Right().Right());
            return attackRange;
        }

        public override void StartAttacking()
        {
            StartCoroutine(TimeDifferenceAttack());
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
            }
        }

        public override HashSet<CardZoneCoordinate> VisualizeAttackRange()
        {
            HashSet<CardZoneCoordinate> ranges = new HashSet<CardZoneCoordinate>(AttackRanges());
            ranges.Add(RootCoordinate.Right().Right());
            ranges.Add(RootCoordinate.Right().Right().Right());
            return ranges;
        }

        public void AdditionalAttack()
        {
            nextAttack = true;
        }

        private IEnumerator TimeDifferenceAttack()
        {
            CardZoneCoordinate coord = RootCoordinate;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitUntil(() => nextAttack);
                coord = coord.Right();
                CardZoneHandler zone = CardZoneManager.Manager.GetZone(coord);

                if (zone.IsOnEnemy())
                {
                    Attack(zone.Enemies);
                }

                nextAttack = false;
            }
        }
    }
}