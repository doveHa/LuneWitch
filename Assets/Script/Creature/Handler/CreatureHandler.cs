using System;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureHandler : MonoBehaviour
    {
        public bool IsOnSummoned { get; private set; } = false;
        public float SummonChance { get; set; }
        public Probability Rarity { get; private set; } = Probability.Common;
        public CardZoneHandler CardZone { get; set; }
        public CreatureData Card { get; private set; }

        public void SetCreatureSummonCard(CreatureData card)
        {
            Card = card;
        }

        public void SetNextProbability()
        {
            Probability[] probabilities = Enum.GetValues(typeof(Probability)) as Probability[];
            Array.Reverse(probabilities);

            foreach (Probability probability in probabilities)
            {
                if ((int)Rarity > (int)probability)
                {
                    Rarity = probability;
                    return;
                }
            }
        }

        public CreatureHandler SummonCreature(CardZoneCoordinate coordinate)
        {
            Transform summonTransform = CardZoneManager.Manager.GetZone(coordinate).gameObject.transform;

            GameObject creatureObject =
                Instantiate(Card.creaturePrefab, summonTransform.position, Quaternion.identity);
            creatureObject.name = Card.characterName;
            creatureObject.transform.SetParent(summonTransform);
            creatureObject.GetComponent<CreatureHandler>().Card = Card;
            creatureObject.GetComponent<AttackHandler.AttackHandler>().RootCoordinate = coordinate;
            
            return creatureObject.GetComponent<CreatureHandler>();
        }
    }
}