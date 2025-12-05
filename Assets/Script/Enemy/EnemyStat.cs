using System.Collections;
using Script.Creature;
using Script.Creature.Handler;
using Script.Manager;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

namespace Script.Enemy
{
    public class EnemyStat : MonoBehaviour
    {
        [SerializeField] private int health, attack;
        [SerializeField] private float attackSpeed, speed, hitTerm;
        private Animator animator;
        private Rigidbody2D rigidbody;

        private bool isRecognize, isDead = false;
        private HitHandler creature;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            Walk();
        }

        void Update()
        {
            if (isRecognize)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        private IEnumerator AttackCoroutine()
        {
            StopWalk();

            while (isRecognize)
            {
                animator.SetBool("Attack", true);
                yield return new WaitForSeconds(attackSpeed);
            }

            animator.SetBool("Attack", false);
            Walk();
        }

        public void CreatureHit()
        {
            if (creature != null)
            {
                creature.Hit(attack);
            }
        }

        public void Hit(int damage)
        {
            if (isDead)
            {
                return;
            }

            health -= damage;
            if (health <= 0)
            {
                Death();
            }

            animator.SetTrigger("Hit");
        }

        public void SetCreature(HitHandler creature)
        {
            isRecognize = true;
            this.creature = creature;
        }

        public void UnSetCreature()
        {
            isRecognize = false;
            creature = null;
        }

        public void Walk()
        {
            rigidbody.linearVelocity = Vector2.left * speed;
        }

        public void StopWalk()
        {
            rigidbody.linearVelocity = Vector2.zero;
        }

        private void Death()
        {
            isDead = true;
            if (animator != null)
            {
                GameFlowManager.Manager.KillEnemy();
                animator.SetTrigger("Death");
            }
        }

        public void Destroy()
        {
            Destroy(transform.parent.gameObject);
        }
    }
}