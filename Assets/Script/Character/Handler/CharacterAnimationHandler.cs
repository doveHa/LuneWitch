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
            animator.SetBool("SkillOn",true);
        }
        

        public void ActiveSkillAnimation()
        {
            animator.SetBool("SkillOn",false);
            animator.SetTrigger("ActiveSkill");
        }

        public void DeathAnimation()
        {
            animator.SetTrigger("Death");
        }
    }
}