using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public abstract class AttackHandler : MonoBehaviour
    {
        private float attackSpeed;
        protected CombatHandler CombatHandler;

        private float currentCooldown = 0;

        public CardZoneCoordinate RootCoordinate { protected get; set; }

        protected virtual void Start()
        {
            attackSpeed = GetComponent<CreatureHandler>().Card.attackTerm;
            CombatHandler = GetComponent<CombatHandler>();
        }

        protected virtual void Update()
        {
            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
                return;
            }

            if (TryAttackEnemy())
            {
                currentCooldown = attackSpeed;
            }
        }

        protected bool TryAttackEnemy()
        {
            bool isAttack = false;
            foreach (CardZoneCoordinate coordinate in AttackRanges())
            {
                if (CardZoneManager.Manager.GetZone(coordinate).IsOnEnemy())
                {
                    Attack(CardZoneManager.Manager.GetZone(coordinate).Enemies);
                    isAttack = true;
                }
            }

            return isAttack;
        }

        protected abstract List<CardZoneCoordinate> AttackRanges();

        protected abstract void Attack(List<EnemyHandler> enemies);
    }
}