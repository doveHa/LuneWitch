using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using UnityEngine;

namespace Script.Creature.Handler
{
    public abstract class AttackHandler : MonoBehaviour
    {
        protected bool isAttacking;
        protected List<Transform> enemies;

        protected float attackSpeed;
        protected CombatHandler CombatHandler;

        public CardZoneCoordinate RootCoordinate { protected get; set; }

        protected virtual void Awake()
        {
            isAttacking = false;
            enemies = new List<Transform>();
        }

        protected virtual void Start()
        {
            attackSpeed = GetComponent<CreatureHandler>().Card.attackTerm;
            CombatHandler = GetComponent<CombatHandler>();
        }

        protected virtual void Update()
        {
            if (isAttacking)
            {
                return;
            }

            if (enemies.Count > 0)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Add(other.transform);
            }
        }
        
        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemies.Remove(other.transform);
            }
        }
        
        protected abstract IEnumerator AttackCoroutine();
    }
}