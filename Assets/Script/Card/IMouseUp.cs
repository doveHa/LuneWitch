using UnityEngine;

namespace Script.Card
{
    public abstract class IMouseUp : MonoBehaviour
    {
        public abstract void MouseUp(Collider2D[] hits);
    }
}