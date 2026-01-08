using System;
using System.Collections;
using System.Collections.Generic;
using Script.Core.Handler;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureAnimationHandler : AnimationHandler
    {
        [SerializeField] private SpriteRenderer creatureSprite;
        [SerializeField] private GameObject hitEffectObject;
        [SerializeField] private ParticleSystem deathParticles;

        private int attackMotionParameter;
        private SpriteRenderer hitEffectSprite;

        void Start()
        {
            if (hitEffectObject != null)
            {
                hitEffectSprite = hitEffectObject.GetComponent<SpriteRenderer>();
            }
        }

        protected override void SetParameter()
        {
            SetAttackParameter("Attack");
            SetDeathParameter("Death");
            SetAttackMotionParameter("AttackMotion");
        }

        public override void PlayHitAnimation()
        {
            StartCoroutine(WaitDelayAndHit());
        }

        public override void PlayDeathAnimation()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator WaitDelayAndHit()
        {
            Animator.speed = 0f;
            hitEffectObject.SetActive(true);
            hitEffectSprite.sprite = creatureSprite.sprite;
            hitEffectObject.transform.position = creatureSprite.transform.position;
            yield return new WaitForSeconds(Constant.BattleSystem.HIT_TIME);
            hitEffectObject.SetActive(false);
            Animator.speed = 1f;
        }

        private IEnumerator DestroyCoroutine()
        {
            deathParticles.Play();
            yield return new WaitUntil(() => deathParticles.isStopped);
            Destroy(gameObject);
        }

        public void PlayAdditionalAttackAnimation()
        {
            Animator.SetBool(attackMotionParameter, true);
        }

        public void EndAdditionalAttackAnimation()
        {
            Animator.SetBool(attackMotionParameter, false);
        }

        private void SetAttackMotionParameter(string parameter)
        {
            attackMotionParameter = Animator.StringToHash(parameter);
        }
    }
}