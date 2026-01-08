using Script.Player;
using UnityEngine;

namespace Script.Character.Handler
{
    public class CharacterHandler : MonoBehaviour
    {
        private CharacterAnimationHandler animationHandler;
        private BaseSkillHandler skillHandler;

        void Start()
        {
            animationHandler = GetComponent<CharacterAnimationHandler>();
            skillHandler = GetComponent<BaseSkillHandler>();
        }

        public void SkillOn()
        {
            animationHandler.SkillOnAnimation();
        }

        public void UseSkill()
        {
            animationHandler.ActiveSkillAnimation();
            skillHandler.OnSkillUse();
        }
    }
}