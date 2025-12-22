using UnityEngine;

namespace Script.Core.Handler
{
    public abstract class AnimationHandler : MonoBehaviour
    {
        protected Animator Animator;
        protected int attackParameter, hitParameter, deathParameter;

        protected virtual void Awake()
        {
            Animator = GetComponent<Animator>();
            SetParameter();
        }

        public virtual void PlayAttackAnimation()
        {
            Animator.SetBool(attackParameter, true);
        }

        public void StopAttackAnimation()
        {
            Animator.SetBool(attackParameter, false);
        }

        public void PlayHitAnimation()
        {
            Animator.SetTrigger(hitParameter);
        }

        public virtual void PlayDeathAnimation()
        {
            Animator.SetTrigger(deathParameter);
        }

        protected void SetAttackParameter(string parameter)
        {
            attackParameter = Animator.StringToHash(parameter);
        }

        protected void SetHitParameter(string parameter)
        {
            hitParameter = Animator.StringToHash(parameter);
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