using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class SacaboomAttackHandler : BaseAttackHandler
    {
        [SerializeField] private float speed;

        private bool isEndLine = false, isMove;
        private bool hasTarget = false;
        private EnemyHandler target;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            return new HashSet<CardZoneCoordinate>();
        }

        public override void Initialize(CreatureData data)
        {
            base.Initialize(data);
            if (RootCoordinate != null)
            {
                isMove = true;
            }
        }

        public override bool HasTarget()
        {
            return hasTarget;
        }

        protected override void Update()
        {
            if (isMove)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
        }

        public override void StartAttacking()
        {
            Attack(null);
        }

        public void Destroy()
        {
            Destroy(gameObject);
            SoundManager.Instance.PlaySFX(45);
        }
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                isMove = false;
                hasTarget = true;
                target = other.GetComponentInChildren<EnemyHandler>();
            }
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            target.Hit(Atk);
        }
    }
}