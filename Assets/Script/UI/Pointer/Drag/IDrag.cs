using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public interface IDrag
    {
        public void Click(PointerHandler handler, GameObject target);
        public void Drag(PointerHandler handler, GameObject target);
        public void Drop(PointerHandler handler, GameObject target);
    }
}