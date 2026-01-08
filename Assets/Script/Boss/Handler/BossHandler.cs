using System.Collections;
using UnityEngine;

namespace Script.Boss.Handler
{
    public class BossHandler : MonoBehaviour
    {
        private BossAnimationHandler animationHandler;
        private BossAttackHandler attackHandler;

        void Awake()
        {
            animationHandler = GetComponent<BossAnimationHandler>();
            attackHandler = GetComponent<BossAttackHandler>();
        }

        public void StartBossState()
        {
            StartCoroutine(StartRoutine());
        }

        private IEnumerator StartRoutine()
        {
            yield return new WaitForSeconds(Constant.Boss.ATTACKTERM);

            int attackPattern = attackHandler.GetAttackPatternIndex();
            animationHandler.AttackAnimation(attackPattern);
            attackHandler.ActiveBossPattern(attackPattern).Invoke();
        }
    }
}