using System.Collections;
using System.Collections.Generic;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class GrootAmmoHandler : MonoBehaviour
    {
        private float duration = 1.0f;
        private float height = 2.0f;

        private int atk;
        private float disSpeedRate, slowTime;
        
        public void SetStat(int atk, float disSpeedRate, float slowTime)
        {
            this.atk = atk;
            this.disSpeedRate = disSpeedRate;
            this.slowTime = slowTime;
        }

        public void ShootAmmo(Transform startPosition, Transform target)
        {
            StartCoroutine(CurveRoutine(startPosition.position, target.position));
        }

        private IEnumerator CurveRoutine(Vector3 start, Vector3 end)
        {
            float timePassed = 0f;

            while (timePassed < duration)
            {
                timePassed += Time.deltaTime;
                float linearT = timePassed / duration;

                float heightT = 4 * height * (linearT - (linearT * linearT));

                transform.position = Vector3.Lerp(start, end, linearT) + new Vector3(0, heightT, 0);

                yield return null;
            }

            transform.position = end;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                EnemyHandler handler = other.GetComponentInChildren<EnemyHandler>();
                handler.Hit(atk);
                handler.DisSpeed(disSpeedRate, slowTime);
                Destroy(gameObject);
            }
        }
    }
}