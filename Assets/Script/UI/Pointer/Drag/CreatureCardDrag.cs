using Script.BattleStyle.Handler;
using Script.UI.Pointer.Hover;
using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public class CreatureCardDrag : MonoBehaviour, IDrag
    {
        public void Click()
        {
            if (TryGetComponent(out IHover hover))
            {
                hover.Exit();
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
                cardZoneHandler.Initialize(GetComponent<CardHandler>().CardData);
                GetComponent<CardHandler>().UseCard();
            }
        }
    }
}