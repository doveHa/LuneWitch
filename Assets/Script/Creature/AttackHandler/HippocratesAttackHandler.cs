using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;

namespace Script.Creature.AttackHandler
{
    public class HippocratesAttackHandler : BaseAttackHandler
    {
        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> ranges = new HashSet<CardZoneCoordinate>();
            ranges.Add(RootCoordinate.Right().Right().Up());
            ranges.Add(RootCoordinate.Right().Right().Right().Up());
            ranges.Add(RootCoordinate.Right().Right().Down());
            ranges.Add(RootCoordinate.Right().Right().Right().Down());
            return ranges;
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
            HashSet<CardZoneCoordinate> additionalRange = new HashSet<CardZoneCoordinate>();
            additionalRange.Add(RootCoordinate.Right().Right());
            additionalRange.Add(RootCoordinate.Right().Right().Right());

            foreach (CardZoneCoordinate range in additionalRange)
            {
                CardZoneHandler zone = CardZoneManager.Manager.GetZone(range);
                if (zone.IsOnEnemy())
                {
                    Attack(zone.Enemies);
                }
            }
        }
    }
}