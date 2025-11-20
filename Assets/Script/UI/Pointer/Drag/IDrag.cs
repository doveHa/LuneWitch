using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public interface IDrag
    {
        public void Click();
        public void Drag(RectTransform target, Vector3 mousePos);
        public void Drop(GameObject drop);
    }
}