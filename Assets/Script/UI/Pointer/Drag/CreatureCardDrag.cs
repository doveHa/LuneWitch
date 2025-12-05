using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.UI.Pointer.Hover;
using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public class CreatureCardDrag : MonoBehaviour, IDrag
    {
        public void Click(GameObject target)
        {
            if (TryGetComponent(out IHover hover))
            {
                hover.Exit();
            }

            CardHandler cardHandler = target.GetComponentInParent<CardHandler>();
            if (cardHandler.IsSummoned())
            {
                GetComponentInParent<PointerHandler>().OnlyClick();
                cardHandler.UpgradeCard();
            }
        }

        public void Drag(RectTransform rectTransform, Vector3 mousePos)
        {
            rectTransform.localPosition = mousePos;
        }

        public void Drop(GameObject drop)
        {
            if (drop.TryGetComponent(out CardZoneHandler cardZoneHandler))
            {
                cardZoneHandler.SummonCreature(GetComponent<CardHandler>().CreatureHandler);
                GetComponent<CardHandler>().UseCard();
            }
        }
    }
}