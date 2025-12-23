using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Creature.Handler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class FrogAttackHandler : BaseAttackHandler
    {
        private EnemyHandler target;

        public override void Initialize(CreatureData data)
        {
            base.Initialize(data);
            if (RootCoordinate != null)
            {
                CreatureSummonHandler summonHandler = GetComponent<CreatureSummonHandler>();
                CardZoneManager.Manager.GetZone(RootCoordinate.Right()).SummonedCreature = summonHandler;
                CardZoneManager.Manager.GetZone(RootCoordinate.Up()).SummonedCreature = summonHandler;
                CardZoneManager.Manager.GetZone(RootCoordinate.Up().Right()).SummonedCreature = summonHandler;
            }
        }

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> result = new HashSet<CardZoneCoordinate>();
            result.Add(RootCoordinate.Right().Right());
            return result;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            target = enemies[0];
            StartCoroutine(WaitCoolDown());
        }

        public void KillTarget()
        {
            target.Dead();
        }

        private IEnumerator WaitCoolDown()
        {
            yield return new WaitForSeconds(attackTerm);
            GetComponent<CreatureAnimationHandler>().EndAdditionalAttackAnimation();
        }
    }
}