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
        protected float AttackTerm;
        protected int Atk;

        public int AttackTermUpgradeCount { get; private set; } = 0;
        public int AtkUpgradeCount { get; private set; } = 0;

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
            AttackTerm = data.attackTerm;
            Atk = data.attack;
        }

        public bool IsCoolOn()
        {
            return CurrentCooldown <= 0;
        }

        public void Cooldown()
        {
            CurrentCooldown = AttackTerm;
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
            foreach (CardZoneCoordinate coordinate in AttackRanges())
            {
                var zone = CardZoneManager.Manager.GetZone(coordinate);
                if (zone.IsOnEnemy())
                {
                    Attack(zone.Enemies);
                }
            }
        }

        public void UpgradeAttack(int atk)
        {
            AtkUpgradeCount++;

            Atk += atk;
        }

        public void UpgradeAttackTerm()
        {
            AttackTermUpgradeCount++;

            AttackTerm *= 1f - Constant.Upgrade.AttackTerm.ATTACKTERM_RATE;
            AttackTerm = Mathf.Max(AttackTerm, Constant.Upgrade.AttackTerm.MIN_ATTACKTERM);
        }

        public abstract HashSet<CardZoneCoordinate> AttackRanges();

        protected abstract void Attack(List<EnemyHandler> enemies);

        public virtual HashSet<CardZoneCoordinate> VisualizeAttackRange()
        {
            return AttackRanges();
        }
    }
}