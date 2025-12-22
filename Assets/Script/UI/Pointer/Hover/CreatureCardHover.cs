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
                cardHandler.CreatureHandler.CreatureSummonHandler.VisualizeCreature();
            }

            descriptionPanel.SetActive(true);
        }

        public void Exit()
        {
            CardHandler cardHandler = GetComponent<CardHandler>();

            if (cardHandler.IsSummoned())
            {
                cardHandler.CreatureHandler.CreatureSummonHandler.NormalizeCreature();
            }

            descriptionPanel.SetActive(false);
        }
    }
}