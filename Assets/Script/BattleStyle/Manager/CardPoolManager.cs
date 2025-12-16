using System.Collections.Generic;
using System.Linq;
using System.Text;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Creature.Handler;
using Script.DataDefinitions.ScriptableObjects;
using Script.Manager;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CardPoolManager : ManagerBase<CardPoolManager>
    {
        private float probabilitySum;
        private List<CreatureHandler> cardPool;

        protected override void Awake()
        {
            base.Awake();
            cardPool = new List<CreatureHandler>();
        }

        public void InitialCreature(List<CreatureData> initialCreatures)
        {
            foreach (CreatureData creature in initialCreatures)
            {
                if (creature.creaturePrefab != null)
                {
                    GameObject temp =
                        Instantiate(creature.creaturePrefab, transform.position, Quaternion.identity);
                    temp.transform.SetParent(transform);
                    temp.SetActive(false);
                    CreatureHandler creatureHandler = temp.GetComponent<CreatureHandler>();
                    creatureHandler.SetCreatureSummonCard(creature);
                    AddCardInPool(temp.GetComponent<CreatureHandler>());
                }
            }
        }

        public void AddCardInPool(CreatureHandler card)
        {
            AddTotalProbability(card);
            cardPool.Add(card);
        }

        public void UpgradeCard(CreatureHandler card)
        {
            cardPool.Remove(card);
            RemoveTotalProbability(card);
            card.SetNextProbability();
            AddCardInPool(card);
        }

        private void AddTotalProbability(CreatureHandler card)
        {
            probabilitySum += (int)card.Rarity * 0.01f;
            card.SummonChance = probabilitySum;
        }

        private void RemoveTotalProbability(CreatureHandler card)
        {
            probabilitySum -= (int)card.Rarity * 0.01f;
        }

        public CreatureHandler GetRandomCreature()
        {
            float summonChance = Random.Range(0, probabilitySum);
            foreach (CreatureHandler card in cardPool)
            {
                if (card.SummonChance > summonChance)
                {
                    return card;
                }
            }

            return cardPool.Last();
        }
    }
}