using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public abstract class BaseAttackHandler : MonoBehaviour
    {
        protected float attackTerm;
        protected int Atk;

        public float CurrentCooldown { get; protected set; } = 0;

        public CardZoneCoordinate RootCoordinate { protected get; set; }

        protected virtual void Update()
        {
            if (CurrentCooldown > 0)
            {
                CurrentCooldown -= Time.deltaTime;
            }
        }

        public virtual void Initialize(CreatureData data)
        {
            attackTerm = data.attackTerm;
            Atk = data.attack;
        }

        public bool IsCoolOn()
        {
            return CurrentCooldown <= 0;
        }

        public virtual bool HasTarget()
        {
            foreach (CardZoneCoordinate coordinate in AttackRanges())
            {
                if (CardZoneManager.Manager.GetZone(coordinate).IsOnEnemy())
                {
                    return true;
                }
            }

            return false;
        }

        public virtual void StartAttacking()
        {
            CurrentCooldown = attackTerm;

            foreach (CardZoneCoordinate coordinate in AttackRanges())
            {
                var zone = CardZoneManager.Manager.GetZone(coordinate);
                if (zone.IsOnEnemy())
                {
                    Attack(zone.Enemies);
                }
            }
        }

        public abstract HashSet<CardZoneCoordinate> AttackRanges();

        protected abstract void Attack(List<EnemyHandler> enemies);
    }
}