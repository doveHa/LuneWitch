using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public class DeleteCardDrag : MonoBehaviour, IDrag
    {
        void Start()
        {
            GetComponent<PointerHandler>().CanDrag = true;
        }

        public void Click(PointerHandler pointer, GameObject target)
        {
        }

        public void Drag(PointerHandler pointer, GameObject target)
        {
        }

        public void Drop(PointerHandler pointer, GameObject target)
        {
            if (target.TryGetComponent(out CardZoneHandler cardZoneHandler) && cardZoneHandler.IsSummoned())
            {
                CreatureHandler handler = cardZoneHandler.SummonedCreature.gameObject.GetComponent<CreatureHandler>();
                handler.Dead();
                int sellCost = ((CreatureHealthHandler)handler.HealthHandler).SellCost();
                CostManager.Manager.AddCost(sellCost);
                CardZoneManager.Manager.AttackRangeNormalize(handler.AttackHandler.AttackRanges());
            }
        }
    }
}