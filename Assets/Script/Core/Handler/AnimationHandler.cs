using UnityEngine;

namespace Script.Core.Handler
{
    public abstract class AnimationHandler : MonoBehaviour
    {
        protected Animator Animator;
        protected int attackParameter, deathParameter;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            SetParameter();
        }

        public void PlayAttackAnimation()
        {
            Animator.SetTrigger(attackParameter);
        }

        public abstract void PlayHitAnimation();

        public virtual void PlayDeathAnimation()
        {
            Animator.SetTrigger(deathParameter);
        }

        protected void SetAttackParameter(string parameter)
        {
            attackParameter = Animator.StringToHash(parameter);
        }


        protected void SetDeathParameter(string parameter)
        {
            deathParameter = Animator.StringToHash(parameter);
        }

        public AnimatorStateInfo GetAnimationStateInfo()
        {
            return Animator.GetCurrentAnimatorStateInfo(0);
        }

        protected abstract void SetParameter();
    }
}