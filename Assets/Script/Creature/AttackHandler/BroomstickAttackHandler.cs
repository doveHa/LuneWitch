using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;

namespace Script.Creature.AttackHandler
{
    public class BroomstickAttackHandler : BaseAttackHandler
    {
        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRanges = new HashSet<CardZoneCoordinate>();
            attackRanges.Add(RootCoordinate.Right());
            return attackRanges;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
                enemy.MoveHandler.KnockBack(CardZoneManager.Manager.GetZone(RootCoordinate.Right().Right()).transform.position);
            }
        }
    }
}