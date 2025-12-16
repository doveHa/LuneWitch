using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyAnimationHandler : MonoBehaviour
    {
        public Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        public void PlayAttackAnimation()
        {
            animator.SetBool("Attack", true);
        }

        public void StopAttackAnimation()
        {
            animator.SetBool("Attack", false);
        }

        public void PlayHitAnimation()
        {
            animator.SetTrigger("Hit");
        }

        public void PlayDeathAnimation()
        {
            animator.SetTrigger("Death");
        }

        public AnimatorStateInfo  GetAnimationStateInfo()
        {
            return animator.GetCurrentAnimatorStateInfo(0);
        }
    }
}