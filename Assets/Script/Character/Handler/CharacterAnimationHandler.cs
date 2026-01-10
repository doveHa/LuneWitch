using UnityEngine;

namespace Script.Character.Handler
{
    public class CharacterAnimationHandler : MonoBehaviour
    {
        private Animator animator;

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SkillOnAnimation()
        {
            animator.SetTrigger("SkillOn");
        }

        public void ActiveSkillAnimation()
        {
            animator.SetTrigger("ActiveSkill");
        }

        public void DeathAnimation()
        {
            animator.SetTrigger("Death");
        }
    }
}