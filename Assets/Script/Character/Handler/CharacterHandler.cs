using Script.Player;
using UnityEngine;

namespace Script.Character.Handler
{
    public class CharacterHandler : MonoBehaviour
    {
        private CharacterAnimationHandler animationHandler;
        private BaseSkillHandler skillHandler;

        //H: Add Sound
        private SoundManager soundManager;

        void Start()
        {
            animationHandler = GetComponent<CharacterAnimationHandler>();
            skillHandler = GetComponent<BaseSkillHandler>();

            soundManager = FindObjectOfType<SoundManager>();
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

        public void GameOver()
        {
            transform.parent.position = new Vector2(0, 0);
            animationHandler.DeathAnimation();

            soundManager.StopAllSFX();
            soundManager.PlaySFX(17);
        }
    }
}