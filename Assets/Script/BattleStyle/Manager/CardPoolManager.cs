using System.Collections.Generic;
using System.Linq;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Manager;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CardPoolManager : ManagerBase<CardPoolManager>
    {
        private float probabilitySum;
        private List<CreatureSummonCard> cardPool;

        protected override void Awake()
        {
            base.Awake();
            cardPool = new List<CreatureSummonCard>();
        }

        public void InitialCreature(List<CreatureSummonCard> initialCreatures)
        {
            foreach (CreatureSummonCard creature in initialCreatures)
            {
                AddCardInPool(creature);
            }
        }

        public void AddCardInPool(CreatureSummonCard card)
        {
            AddTotalProbability(card);
            cardPool.Add(card);
        }

        private void AddTotalProbability(CreatureSummonCard card)
        {
            probabilitySum += (int)card.Rarity * 0.01f;
            card.SummonChance = probabilitySum;
        }

        public CreatureSummonCard GetRandomCreature()
        {
            float summonChance = Random.Range(0, probabilitySum);
            foreach (CreatureSummonCard card in cardPool)
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