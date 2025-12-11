using System;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.Enemy;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.BattleStyle.Handler
{
    public class CardZoneHandler : MonoBehaviour
    {
        public CardZoneCoordinate Coordinate { get; set; }
        public CreatureHandler creatureHandler { get; set; }
        public List<EnemyHandler> Enemies { get; private set; }

        void Awake()
        {
            Enemies = new List<EnemyHandler>();
        }

        public void SummonCreature(CreatureHandler creature)
        {
            GameObject creatureObject =
                Instantiate(creature.Card.creaturePrefab, transform.position, Quaternion.identity);
            creatureObject.name = creature.Card.characterName;
            creatureObject.transform.SetParent(transform);

            creatureHandler = creatureObject.GetComponent<CreatureHandler>();
            creatureHandler.CardZone = this;
            creatureHandler.SummonCreatureSetting(creature.Card);

            CardPoolManager.Manager.AddCardInPool(creatureHandler);
        }

        public void InEnemy(EnemyHandler enemyHandler)
        {
            Enemies.Add(enemyHandler);
        }

        public void OutEnemy(EnemyHandler enemyHandler)
        {
            Enemies.Remove(enemyHandler);
        }

        public void DeathCreature()
        {
            creatureHandler = null;
        }

        public void Visualization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        public void Normalization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        public bool IsSpawned()
        {
            if (creatureHandler == null)
            {
                return false;
            }

            return creatureHandler.IsOnSummoned;
        }
    }
}