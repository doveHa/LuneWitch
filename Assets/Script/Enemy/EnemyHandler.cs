using System.Collections;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Manager;
using UnityEngine;

namespace Script.Enemy
{
    public class EnemyHandler : MonoBehaviour
    {
        private EnemyData enemyData;

        private EnemyAnimationHandler animationHandler;
        private EnemyHealthHandler healthHandler;
        private EnemyMoveHandler moveHandler;

        public bool IsRecognize { get; set; }
        private bool isAttak;

        void Awake()
        {
            isAttak = false;
        }
        void Start()
        {
            animationHandler = GetComponent<EnemyAnimationHandler>();
            healthHandler = GetComponent<EnemyHealthHandler>();
            healthHandler.Initialize(enemyData.health);
            moveHandler = GetComponent<EnemyMoveHandler>();
            moveHandler.Initialize(enemyData.speed);
        }

        void Update()
        {
            if (IsRecognize && !isAttak)
            {
                StartCoroutine(AttackCoroutine());
            }
        }

        public void Hit(int damage)
        {
            animationHandler.PlayHitAnimation();
            healthHandler.Hit(damage);
         
            if (healthHandler.IsDead)
            {
                GameFlowManager.Manager.KillEnemy();
                animationHandler.PlayDeathAnimation();
                StartCoroutine(DeathCoroutine());
            }
        }

        public void DisSpeed(float disSpeedRate, float slowTime)
        {
            StartCoroutine(SpeedDebuff(disSpeedRate, slowTime));
        }

        private IEnumerator SpeedDebuff(float disSpeedRate, float slowTime)
        {
            float originalSpeed = moveHandler.Speed;
            moveHandler.Speed *= disSpeedRate;
            yield return new WaitForSeconds(slowTime);
            moveHandler.Speed = originalSpeed; 
        }

        private IEnumerator AttackCoroutine()
        {
            isAttak = true;
            moveHandler.StopWalk();

            while (IsRecognize)
            {
                animationHandler.PlayAttackAnimation();
                yield return new WaitForSeconds(enemyData.attackTerm);
            }
            
            animationHandler.StopAttackAnimation();
            moveHandler.Walk();
            isAttak = false;
        }

        private IEnumerator DeathCoroutine()
        {
            animationHandler.PlayDeathAnimation();
            yield return new WaitUntil(() => animationHandler.GetAnimationStateInfo().normalizedTime >= 1f);
            Destroy(gameObject);
        }
    }
}