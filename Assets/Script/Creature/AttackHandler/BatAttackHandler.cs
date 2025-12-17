using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.Enemy.Handler;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class BatAttackHandler : AttackHandler
    {
        protected override List<CardZoneCoordinate> AttackRanges()
        {
            List<CardZoneCoordinate> attackRange = new List<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Right());
            attackRange.Add(RootCoordinate.Right().Right());
            attackRange.Add(RootCoordinate.Right().Up());
            attackRange.Add(RootCoordinate.Right().Down());

            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(GetComponent<CreatureHandler>().Card.attack);
            }
        }
    }
}