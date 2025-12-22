using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class GrootAttackHandler : BaseAttackHandler
    {
        [SerializeField] private float slowTime = 2;
        [SerializeField] private float disSpeedRate = 0.5f;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(new CardZoneCoordinate(RootCoordinate.Row, CardZoneCoordinate.MAXCOL));
            return attackRange;
        }   

        protected override void Attack(List<EnemyHandler> enemies)
        {
            enemies[0].DisSpeed(disSpeedRate, slowTime);
            enemies[0].Hit(Atk);
        }
    }
}