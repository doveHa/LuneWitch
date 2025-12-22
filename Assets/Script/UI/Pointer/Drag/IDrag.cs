using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public interface IDrag
    {
        public void Click(GameObject target);
        public void Drag(GameObject target);
        public void Drop(GameObject target);
    }
}