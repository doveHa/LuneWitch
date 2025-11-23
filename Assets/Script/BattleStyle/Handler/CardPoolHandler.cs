using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardPoolHandler : MonoBehaviour
    {
        private CardHandler[] cardHandlers;

        void Awake()
        {
        }

        void Start()
        {
            cardHandlers = GetComponentsInChildren<CardHandler>();
        }

        public void SetCards()
        {
            foreach (CardHandler cardHandler in cardHandlers)
            {
                CreatureSummonCard card = CardPoolManager.Manager.GetRandomCreature();
                cardHandler.SetCard(card);
                Debug.Log(card.CreatureName);
            }
        }
    }
}