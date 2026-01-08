using System.Collections;
using Script.Core.Handler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Boss.Handler
{
    public class BossHandler : MonoBehaviour
    {
        private BossAnimationHandler animationHandler;
        private BossAttackHandler attackHandler;
        private HealthHandler healthHandler;

        void Awake()
        {
            animationHandler = GetComponent<BossAnimationHandler>();
            attackHandler = GetComponent<BossAttackHandler>();
            healthHandler = new EnemyHealthHandler(Constant.Boss.HEALTH);
        }

        public void StartBossState()
        {
            StartCoroutine(StartRoutine());
        }

        public void Hit(int atk)
        {
            animationHandler.HitAnimation();
            healthHandler.Hit(atk);

            if (healthHandler.IsDead)
            {
                animationHandler.DeathAnimation();
                StopAllCoroutines();
                Time.timeScale = 0;
            }
        }

        public bool IsDead()
        {
            return healthHandler.IsDead;
        }

        private IEnumerator StartRoutine()
        {
            while (!healthHandler.IsDead)
            {
                yield return new WaitForSeconds(Constant.Boss.ATTACKTERM);

                int attackPattern = attackHandler.GetAttackPatternIndex();
                animationHandler.AttackAnimation(attackPattern);
                attackHandler.ActiveBossPattern(attackPattern).Invoke();
            }
        }
    }
}