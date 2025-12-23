using System.Collections;
using Script.Enemy.Handler;
using Script.Stage.Manager;
using UnityEngine;

namespace Script.Player
{
    public class LuminaSkill : IPlayerSkill
    {
        [SerializeField] private int damage;
        [SerializeField] ParticleSystem particle;

        public override void OnSkillUse()
        {
            particle.Play();
            StartCoroutine(AdjustDamage());
        }

        private IEnumerator AdjustDamage()
        {
            yield return new WaitUntil(() => particle.time / particle.totalTime > 0.5);
            Damage(GameFlowManager.Manager.Spawner().SpawnPoints());
            GetComponent<PlayerAnimationController>().SkillEnd();
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