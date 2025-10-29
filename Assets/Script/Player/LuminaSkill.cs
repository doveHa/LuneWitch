using System.Collections;
using System.Collections.Generic;
using Script.Enemy;
using Script.Manager;
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
                EnemyStat[] stats = point.gameObject.GetComponentsInChildren<EnemyStat>();
                foreach (EnemyStat stat in stats)
                {
                    stat.Hit(damage);
                }
            }
        }
    }
}