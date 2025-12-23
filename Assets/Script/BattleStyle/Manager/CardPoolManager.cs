using System.Collections.Generic;
using System.Linq;
using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CardPoolManager : ManagerBase<CardPoolManager>
    {
        private float probabilitySum;
        private List<CreatureSummonHandler> cardPool;

        protected override void Awake()
        {
            base.Awake();
            cardPool = new List<CreatureSummonHandler>();
        }

        public void InitialCreature(List<CreatureData> initialCreatures)
        {
            foreach (CreatureData creature in initialCreatures)
            {
                if (creature.prefab != null)
                {
                    GameObject temp =
                        Instantiate(creature.prefab, transform.position, Quaternion.identity);
                    temp.transform.SetParent(transform);
                    temp.SetActive(false);
                    CreatureHandler creatureHandler = temp.GetComponent<CreatureHandler>();
                    creatureHandler.Initialize(creature);
                    AddCardInPool(creatureHandler.CreatureSummonHandler);
                }
            }
        }

        public void AddCardInPool(CreatureSummonHandler card)
        {
            AddTotalProbability(card);
            cardPool.Add(card);
        }

        public void UpgradeCard(CreatureSummonHandler card)
        {
            cardPool.Remove(card);
            RemoveTotalProbability(card);
            card.SetNextProbability();
            AddCardInPool(card);
        }

        private void AddTotalProbability(CreatureSummonHandler card)
        {
            probabilitySum += (int)card.Rarity * 0.01f;
            card.SummonChance = probabilitySum;
        }

        private void RemoveTotalProbability(CreatureSummonHandler card)
        {
            probabilitySum -= (int)card.Rarity * 0.01f;
        }

        public CreatureSummonHandler GetRandomCreature()
        {
            float summonChance = Random.Range(0, probabilitySum);
            foreach (CreatureSummonHandler card in cardPool)
            {
                Debug.Log(card);
                if (card.SummonChance > summonChance)
                {
                    return card;
                }
            }

            return cardPool.Last();
        }
    }
}