using Script.BattleStyle.Handler;
using Script.Creature.Handler;
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

            if (other.TryGetComponent(out CardZoneHandler dropSlot))
            {
                if (dropSlot.IsSpawned())
                {
                    isRecognized = true;
                    GetComponentInParent<EnemyStat>().SetCreature(dropSlot.GetComponentInChildren<HitHandler>());
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Creature"))
            {
                isRecognized = false;
                GetComponentInParent<EnemyStat>().UnSetCreature();
            }
        }
    }
}