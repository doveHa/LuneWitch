using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;
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
                if (dropSlot.IsSummoned())
                {
                    isRecognized = true;
                    GetComponentInParent<EnemyHandler>().SetRecognize(dropSlot.Coordinate);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Creature"))
            {
                Debug.Log("OnTriggerExit");
                isRecognized = false;
                GetComponentInParent<EnemyHandler>().KillCreature();
            }
        }
    }
}