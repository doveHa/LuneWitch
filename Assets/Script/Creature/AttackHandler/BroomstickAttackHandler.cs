using System;
using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class BroomstickAttackHandler : BaseAttackHandler
    {
        [SerializeField] private float knockBackDistance;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRanges = new HashSet<CardZoneCoordinate>();
            attackRanges.Add(RootCoordinate.Right());
            return attackRanges;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            enemies[0].Hit(Atk);
            enemies[0].MoveHandler.KnockBack(knockBackDistance);
        }
    }
}