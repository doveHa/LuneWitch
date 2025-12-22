using System;
using System.Collections;
using System.Collections.Generic;
using Script.Core.Handler;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureAnimationHandler : AnimationHandler
    {
        [SerializeField] private ParticleSystem deathParticles;

        protected override void SetParameter()
        {
            SetAttackParameter("Attack");
            SetHitParameter("Hit");
            SetDeathParameter("Death");
        }

        public override void PlayAttackAnimation()
        {
            Animator.SetTrigger(attackParameter);

        }
        
        public override void PlayDeathAnimation()
        {
            StartCoroutine(DestroyCoroutine());
        }

        private IEnumerator DestroyCoroutine()
        {
            deathParticles.Play();
            yield return new WaitUntil(() => deathParticles.isStopped);
            Destroy(gameObject);
        }
    }
}