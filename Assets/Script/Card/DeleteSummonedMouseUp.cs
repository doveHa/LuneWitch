using UnityEngine;

namespace Script.Card
{
    public class DeleteSummonedMouseUp : IMouseUp
    {
        void Start()
        {
            GetComponentInParent<DraggableObject>().SetDraggable(true);
        }

        public override void MouseUp(Collider2D[] hits)
        {
            if (hits != null)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.TryGetComponent<DropSlot>(out DropSlot slot))
                    {
                        if (slot.IsOnCreature)
                        {
                            Delete(slot);
                            return;
                        }
                    }
                }
            }
        }

        private void Delete(DropSlot slot)
        {
            slot.Creature.DestroyWithoutDeath();
        }
    }
}