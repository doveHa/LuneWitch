using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.UI.Pointer.Hover
{
    public class CreatureCardHover : MonoBehaviour, IHover
    {
        [SerializeField] private GameObject descriptionPanel;

        public void Enter()
        {
            CardHandler cardHandler = GetComponent<CardHandler>();

            if (cardHandler.IsSummoned())
            {
                cardHandler.CreatureHandler.CardZone.Visualization();
            }

            descriptionPanel.SetActive(true);
        }

        public void Exit()
        {
            CardHandler cardHandler = GetComponent<CardHandler>();

            if (cardHandler.IsSummoned())
            {
                cardHandler.CreatureHandler.CardZone.Normalization();
            }

            descriptionPanel.SetActive(false);
        }
    }
}