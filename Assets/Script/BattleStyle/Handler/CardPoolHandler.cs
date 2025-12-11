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

        public void SetCards()
        {
            foreach (CardHandler cardHandler in cardHandlers)
            {
                CreatureHandler card = CardPoolManager.Manager.GetRandomCreature();
                cardHandler.SetCard(card);
            }
            CardPoolManager.Manager.DebugLog();
        }
    }
}