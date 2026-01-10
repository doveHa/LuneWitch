using System.Collections;
using Script.Character.Handler;
using Script.Enemy.Handler;
using Script.Player;
using Script.Stage.Manager;
using UnityEngine;

namespace Script.Character.SkillHandler
{
    public class LuminaSkillHandler : BaseSkillHandler
    {
        public override void OnSkillUse()
        {
            particle.Play();
            StartCoroutine(AdjustDamage());
        }

        private IEnumerator AdjustDamage()
        {
            yield return new WaitUntil(() => particle.time / particle.totalTime > 0.7);
            Damage(GameFlowManager.Manager.Spawner().SpawnPoints());
        }

        private void Damage(Transform[] points)
        {
            foreach (Transform point in points)
            {
                EnemyHandler[] stats = point.gameObject.GetComponentsInChildren<EnemyHandler>();
                foreach (EnemyHandler enemyHandler in stats)
                {
                    enemyHandler.Hit(damage);
                }
            }
        }
    }
}