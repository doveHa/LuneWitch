using System.Collections;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Core.DataDefinitions.ScriptableObjects;
using Script.Core.Handler;
using Script.Creature.Handler;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Stage.Manager;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyHandler : BaseHandler
    {
        private EnemyData enemyData;

        public EnemyMoveHandler MoveHandler { get; private set; }

        public bool IsRecognize { get; set; }
        private bool isAttack;

        private CardZoneCoordinate attackZone;

        void Awake()
        {
            isAttack = false;
        }

        void Update()
        {
            if (IsRecognize && !isAttack)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        public override void Initialize(BaseData data)
        {
            enemyData = data as EnemyData;
            AnimationHandler = GetComponent<EnemyAnimationHandler>();
            HealthHandler = new EnemyHealthHandler(enemyData.health);
            MoveHandler = GetComponent<EnemyMoveHandler>();
            MoveHandler.Initialize(enemyData.moveSpeed);
            Debug.Log("Initialize Walk");
            Walk();
        }

        private void Walk()
        {
            if (!isAttack)
            {
                (AnimationHandler as EnemyAnimationHandler).StartWalkAnimation();
                MoveHandler.StartWalk();
            }
        }

        public override void Hit(int damage)
        {
            base.Hit(damage);
            StartCoroutine(WaitDelayAndWalk());
        }

        private IEnumerator WaitDelayAndWalk()
        {
            MoveHandler.StopWalk();
            yield return new WaitForSeconds(Constant.BattleSystem.HIT_TIME);
            Walk();
        }

        public override void Dead()
        {
            GameFlowManager.Manager.KillEnemy();
            AnimationHandler.PlayDeathAnimation();
            StartCoroutine(DeathCoroutine());
        }

        public void DisSpeed(float disSpeedRate, float slowTime)
        {
            StartCoroutine(SpeedDebuff(disSpeedRate, slowTime));
        }

        public void SetRecognize(CardZoneCoordinate coordinate)
        {
            IsRecognize = true;
            attackZone = coordinate;
        }

        public void KillCreature()
        {
            Debug.Log("KillCreature");
            IsRecognize = false;
            isAttack = false;
            Walk();
        }

        public void Explore()
        {
            Destroy(gameObject);
        }

        public void AdjustCreatureDamage()
        {
            CardZoneManager.Manager.GetZone(attackZone).AttackCreature(enemyData.attack);
        }

        private IEnumerator SpeedDebuff(float disSpeedRate, float slowTime)
        {
            float originalSpeed = MoveHandler.Speed;
            MoveHandler.Speed *= disSpeedRate;
            yield return new WaitForSeconds(slowTime);
            MoveHandler.Speed = originalSpeed;
        }

        private IEnumerator AttackCoroutine()
        {
            isAttack = true;
            MoveHandler.StopWalk();

            while (IsRecognize)
            {
                AnimationHandler.PlayAttackAnimation();
                yield return new WaitForSeconds(enemyData.attackTerm);
            }

            Debug.Log("AttackCoroutine Walk");
            Walk();
            isAttack = false;
        }

        private IEnumerator DeathCoroutine()
        {
            AnimationHandler.PlayDeathAnimation();
            yield return new WaitUntil(() => AnimationHandler.GetAnimationStateInfo().normalizedTime >= 1f);
            Destroy(gameObject);
        }
    }
}