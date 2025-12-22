using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Enemy.Handler;

namespace Script.Creature.AttackHandler
{
    public class NoAttackHandler : BaseAttackHandler
    {
        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            return new HashSet<CardZoneCoordinate>();
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            
        }
    }
}