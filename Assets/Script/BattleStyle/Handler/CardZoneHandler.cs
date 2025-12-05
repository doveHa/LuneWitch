using System;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardZoneHandler : MonoBehaviour
    {
        public CreatureHandler creatureHandler { get; set; }

        public void SummonCreature(CreatureHandler creature)
        {
            GameObject creatureObject =
                Instantiate(creature.Card.CreaturePrefab, transform.position, Quaternion.identity);
            creatureObject.name = creature.Card.CreatureName;
            creatureObject.transform.SetParent(transform);
            
            creatureHandler = creatureObject.GetComponent<CreatureHandler>();
            creatureHandler.CardZone = this;
            creatureHandler.SummonCreatureSetting(creature.Card);

            CardPoolManager.Manager.AddCardInPool(creatureHandler);
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