using UnityEngine;

namespace Script.Enemy
{
    public class RecognizeCreature : MonoBehaviour
    {
        private bool isRecognized;

        void OnTriggerStay2D(Collider2D other)
        {
            if (isRecognized)
            {
                return;
            }

            if (other.TryGetComponent(out DropSlot dropSlot))
            {
                if (dropSlot.IsOnCreature)
                {
                    isRecognized = true;
                    GetComponentInParent<EnemyStat>().SetCreature(true, dropSlot.Creature);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Creature"))
            {
                isRecognized = false;
                GetComponentInParent<EnemyStat>().SetCreature(false, null);
            }
        }
    }
}