using UnityEngine;

namespace Script.Enemy
{
    public class RecognizeCreature : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DropSlot dropSlot))
            {
                if (dropSlot.IsOnCreature)
                {
                    GetComponentInParent<EnemyStat>().SetCreature(true, dropSlot.Creature);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Creature"))
            {
                GetComponentInParent<EnemyStat>().SetCreature(false, null);
            }
        }
    }
}