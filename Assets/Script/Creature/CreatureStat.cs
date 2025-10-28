using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Creature
{
    public class CreatureStat : MonoBehaviour
    {
        [SerializeField] private ParticleSystem deathParticles;

        private Animator animator;
        private int health;

        public int Attack { get; private set; }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Initialize(CharacterData characterData)
        {
            health = characterData.health;
            Attack = characterData.attack;
        }

        public void AttackMotion()
        {
            animator.SetTrigger("Attack");
        }

        public void Hit(int damage)
        {
            health -= damage;
            if (health <= 0)
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
            if (animator != null)
            {
                animator.SetTrigger("Death");
            }
        }

        public void Destroy()
        {
            GetComponentInParent<DropSlot>().IsOnCreature = false;
            StartCoroutine(DestroyCoroutine());
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