using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Creature
{
    public class CreatureStat : MonoBehaviour
    {
        private Animator animator;
        public int Health { get; private set; }
        public int Attack { get; private set; }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void Initialize(CharacterData characterData)
        {
            Health = characterData.health;
            Attack = characterData.attack;
        }
        public void AttackMotion()
        {
            animator.SetTrigger("Attak");
        }

        public void Hit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Death();
            }
            animator.SetTrigger("Hit");
        }
        
        private void Death()
        {
            animator.SetTrigger("Death");
        }
    }
}