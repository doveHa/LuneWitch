using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using UnityEngine;

namespace Script.UI.Pointer.Hover
{
    public class CreatureCardHover : MonoBehaviour, IHover
    {
        [SerializeField] private GameObject descriptionPanel;

        public void Enter()
        {
            CreatureSummonCard card = GetComponent<CardHandler>().CardData;

            if (card.IsOnSummoned)
            {
                card.CardZone.Visualization();
            }

            descriptionPanel.SetActive(true);
        }

        public void Exit()
        {
            CreatureSummonCard card = GetComponent<CardHandler>().CardData;

            if (card.IsOnSummoned)
            {
                card.CardZone.Normalization();
            }
            
            descriptionPanel.SetActive(false);
        }
    }
}