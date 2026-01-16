using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Enemy.Handler;

namespace Script.Creature.AttackHandler
{
    public class SpiderAttackHandler : BaseAttackHandler
    {
        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> ranges = new HashSet<CardZoneCoordinate>();
            ranges.Add(RootCoordinate.Right());
            ranges.Add(RootCoordinate.Right().Right());
            return ranges;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
                SoundManager.Instance.PlaySFX(46);
            }
        }
    }
}