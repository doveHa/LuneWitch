using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Enemy.Handler;

namespace Script.Creature.AttackHandler
{
    public class BalloonAttackHandler : BaseAttackHandler
    {
        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            var attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Right());
            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
            }
        }
    }
}