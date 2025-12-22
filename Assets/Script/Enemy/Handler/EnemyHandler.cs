using System.Collections;
using Script.Core.DataDefinitions.ScriptableObjects;
using Script.Core.Handler;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Manager;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyHandler : BaseHandler
    {
        private EnemyData enemyData;

        public EnemyMoveHandler MoveHandler { get; private set; }

        public bool IsRecognize { get; set; }
        private bool isAttak;

        void Awake()
        {
            isAttak = false;
        }

        void Update()
        {
            if (IsRecognize && !isAttak)
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
        }

        protected override void Dead()
        {
            //GameFlowManager.Manager.KillEnemy();
            AnimationHandler.PlayDeathAnimation();
            StartCoroutine(DeathCoroutine());
        }

        public void DisSpeed(float disSpeedRate, float slowTime)
        {
            StartCoroutine(SpeedDebuff(disSpeedRate, slowTime));
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
            isAttak = true;
            MoveHandler.StopWalk();

            while (IsRecognize)
            {
                AnimationHandler.PlayAttackAnimation();
                yield return new WaitForSeconds(enemyData.attackTerm);
            }

            AnimationHandler.StopAttackAnimation();
            MoveHandler.Walk();
            isAttak = false;
        }

        private IEnumerator DeathCoroutine()
        {
            AnimationHandler.PlayDeathAnimation();
            yield return new WaitUntil(() => AnimationHandler.GetAnimationStateInfo().normalizedTime >= 1f);
            Destroy(gameObject);
        }
    }
}