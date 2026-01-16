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
    public class BatAttackHandler : BaseAttackHandler
    {
        private SoundManager soundManager;

        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(RootCoordinate.Right());
            attackRange.Add(RootCoordinate.Right().Right());
            attackRange.Add(RootCoordinate.Right().Right().Up());
            attackRange.Add(RootCoordinate.Right().Right().Down());
            
            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
                soundManager.PlaySFX(40);
            }
        }
    }
}