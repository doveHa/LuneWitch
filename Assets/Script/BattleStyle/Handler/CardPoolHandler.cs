using System.Collections.Generic;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardPoolHandler : MonoBehaviour
    {
        private CardHandler[] cardHandlers;

        void Start()
        {
            cardHandlers = GetComponentsInChildren<CardHandler>();
        }

        public void SetCards(List<CreatureSummonHandler> cards)
        {
            for (int i = 0; i < Constant.BattleSystem.MAX_CARDS - 1; i++)
            {
                cardHandlers[i].SetCard(cards[i].GetComponent<CreatureHandler>());
            }
        }

        public CardHandler GetCardSlot(CreatureSummonHandler summon)
        {
            foreach (CardHandler cardHandler in cardHandlers)
            {
                if (cardHandler.CreatureHandler.CreatureSummonHandler == summon)
                {
                    return cardHandler;
                }
            }

            return null;
        }
    }
}