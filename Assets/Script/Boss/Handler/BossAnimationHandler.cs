using UnityEngine;

namespace Script.Boss.Handler
{
    public class BossAnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private int hitParameter, deathParameter, attackParameter, attackPatternParameter;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        void Start()
        {
            SetParameters();
        }

        private void SetParameters()
        {
            hitParameter = Animator.StringToHash("Hit");
            deathParameter = Animator.StringToHash("Death");
            attackParameter = Animator.StringToHash("Attack");
            attackPatternParameter = Animator.StringToHash("AttackPattern");
        }

        public void HitAnimation()
        {
            animator.SetTrigger(hitParameter);
        }

        public void DeathAnimation()
        {
            animator.SetTrigger(deathParameter);
        }

        public void AttackAnimation(int patternNumber)
        {
            animator.SetInteger(attackPatternParameter, patternNumber);
            animator.SetTrigger(attackParameter);
        }
    }
}