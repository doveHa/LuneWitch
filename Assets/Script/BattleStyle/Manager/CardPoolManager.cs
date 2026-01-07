using System.Collections.Generic;
using System.Linq;
using Script.BattleStyle.Handler;
using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CardPoolManager : ManagerBase<CardPoolManager>
    {
        [SerializeField] private CardPoolHandler cardPoolHandler;

        private float probabilitySum;
        private List<CreatureSummonHandler> cardPool;
        private HashSet<CreatureSummonHandler> originalCards;

        protected override void Awake()
        {
            isDontDestroy = false;
            base.Awake();
            cardPool = new List<CreatureSummonHandler>();
            originalCards = new HashSet<CreatureSummonHandler>();
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
                    originalCards.Add(creatureHandler.CreatureSummonHandler);
                }
            }
        }

        public void ReRoll()
        {
            List<CreatureSummonHandler> creatures = new List<CreatureSummonHandler>();

            while (creatures.Count < Constant.BattleSystem.MAX_CARDS)
            {
                CreatureSummonHandler pickedCard = GetRandomCreature();

                if (originalCards.Contains(pickedCard))
                {
                    creatures.Add(pickedCard);
                }
                else if (!creatures.Contains(pickedCard))
                {
                    creatures.Add(pickedCard);
                }
            }

            cardPoolHandler.SetCards(creatures);
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

        public void AddCardInPool(CreatureSummonHandler card)
        {
            AddTotalProbability(card);
            cardPool.Add(card);
        }

        public void RemoveCardInPool(CreatureSummonHandler card)
        {
            if (cardPoolHandler.GetCardSlot(card) != null)
            {
                cardPoolHandler.GetCardSlot(card).SetUsedUI();
            }
            RemoveTotalProbability(card);
            cardPool.Remove(card);
        }

        public void UpgradeCard(CreatureSummonHandler card)
        {
            RemoveCardInPool(card);
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
    }
}