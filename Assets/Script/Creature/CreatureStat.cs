using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Creature
{
    public class CreatureStat : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;

        private Animator animator;
        private bool isDead = false;

        public int MaxHealth { get; private set; }
        public int Health { get; private set; }
        public int Attack { get; private set; }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Initialize(CharacterData characterData)
        {
            MaxHealth = characterData.health;
            Health = MaxHealth;
            Attack = characterData.attack;
        }

        public void AttackMotion()
        {
            animator.SetTrigger("Attack");
        }

        public void Hit(int damage)
        {
            if (isDead)
            {
                return;
            }

            Health -= damage;
            if (Health <= 0)
            {
                Death();
            }
            else
            {
                animator.SetTrigger("Hit");
            }
        }

        private void Death()
        {
            isDead = true;

            if (animator != null)
            {
                animator.SetTrigger("Death");
            }
        }

        public void Destroy()
        {
            GetComponentInParent<DropSlot>().IsOnCreature = false;
            if (deathParticles != null)
            {
                StartCoroutine(DestroyCoroutine());
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void DestroyWithoutDeath()
        {
            GetComponentInParent<DropSlot>().IsOnCreature = false;
            Destroy(gameObject);
        }
        
        public void RangeShootAmmo()
        {
            GetComponentInChildren<RangedAttack>().ShootAmmo();
        }

        private IEnumerator DestroyCoroutine()
        {
            deathParticles.Play();
            yield return new WaitUntil(() => deathParticles.isStopped);
            Destroy(gameObject);
        }
    }
}