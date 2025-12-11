using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
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
                    CardZoneManager.Manager.GetZone(dropSlot.Coordinate.Right()).InEnemy(GetComponentInParent<EnemyHandler>());
                    GetComponentInParent<EnemyHandler>().IsRecognize = true;
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Creature"))
            {
                isRecognized = false;
                GetComponentInParent<EnemyHandler>().IsRecognize = false;
            }
        }
    }
}