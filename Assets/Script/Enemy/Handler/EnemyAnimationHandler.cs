using Script.Core.Handler;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyAnimationHandler : AnimationHandler
    {
        private int walkParameter, hitParameter;

        public void StartWalkAnimation()
        {
            Animator.SetTrigger(walkParameter);
        }

        public override void PlayHitAnimation()
        {
            Animator.SetTrigger(hitParameter);
        }

        protected override void SetParameter()
        {
            SetAttackParameter("Attack");
            SetDeathParameter("Death");
            SetWalkParameter();
            SetHitParameter();
        }

        private void SetWalkParameter()
        {
            walkParameter = Animator.StringToHash("Walk");
        }
        
        protected void SetHitParameter()
        {
            hitParameter = Animator.StringToHash("Hit");
        }
    }
}