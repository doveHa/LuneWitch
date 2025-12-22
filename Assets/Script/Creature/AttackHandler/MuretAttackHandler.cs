using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class MuretAttackHandler : BaseAttackHandler
    {
        [SerializeField] private ParticleSystem explodeParticles;

        public override void Initialize(CreatureData data)
        {
            base.Initialize(data);
            CurrentCooldown = attackTerm;
        }

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRanges = new HashSet<CardZoneCoordinate>();
            attackRanges.Add(RootCoordinate);
            attackRanges.Add(RootCoordinate.Left());
            attackRanges.Add(RootCoordinate.Right());
            attackRanges.Add(RootCoordinate.Up());
            attackRanges.Add(RootCoordinate.Down());
            attackRanges.Add(RootCoordinate.Up().Left());
            attackRanges.Add(RootCoordinate.Up().Right());
            attackRanges.Add(RootCoordinate.Down().Left());
            attackRanges.Add(RootCoordinate.Down().Right());
            return attackRanges;
        }

        public override bool HasTarget()
        {
            return true;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler enemy in enemies)
            {
                enemy.Hit(Atk);
            }
        }

        public void AttackAnimationTrigger()
        {
            StartCoroutine(DestroyCoroutine());
        }
        
        private IEnumerator DestroyCoroutine()
        {
            explodeParticles.Play();
            yield return new WaitUntil(() => explodeParticles.isStopped);
            Destroy(gameObject);
        }
    }
}