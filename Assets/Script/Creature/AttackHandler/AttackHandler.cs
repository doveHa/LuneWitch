using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using UnityEngine;

namespace Script.Creature.Handler
{
    public abstract class AttackHandler : MonoBehaviour
    {
        protected float attackSpeed;
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
            foreach (CardZoneCoordinate coordinate in AttackRanges())
            {
                if (CardZoneManager.Manager.GetZone(coordinate).Enemies.Count > 0)
                {
                }
            }

            return true;
        }

        protected abstract List<CardZoneCoordinate> AttackRanges();
    }
}