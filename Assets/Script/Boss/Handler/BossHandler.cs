using System.Collections;
using Script.BattleStyle.Manager;
using Script.Core.Handler;
using Script.Creature.Handler;
using Script.Enemy.Handler;
using Script.Manager;
using Script.Stage.Manager;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Script.Boss.Handler
{
    public class BossHandler : MonoBehaviour
    {
        private BossAnimationHandler animationHandler;
        private BossAttackHandler attackHandler;
        private HealthHandler healthHandler;

        private bool isDeadProsess = false;

        void Awake()
        {
            animationHandler = GetComponent<BossAnimationHandler>();
            attackHandler = GetComponent<BossAttackHandler>();
            healthHandler = new EnemyHealthHandler(Constant.Boss.HEALTH);
        }

        public void StartBossState()
        {
            StartCoroutine(StartRoutine());
        }

        public void Hit(int atk)
        {
            if (isDeadProsess)
            {
                return;
            }

            healthHandler.Hit(atk);
            if (healthHandler.IsDead)
            {
                isDeadProsess = true;
                StopAllCoroutines();

                animationHandler.DeathAnimation();
                EndBossStage();
            }
            else
            {
                animationHandler.HitAnimation();
            }
        }

        public bool IsDead()
        {
            return healthHandler.IsDead;
        }

        private void EndBossStage()
        {
            GameFlowManager.Manager.Spawner().StopSpawning();
            foreach (Transform point in GameFlowManager.Manager.Spawner().SpawnPoints())
            {
                foreach (EnemyHandler handler in point.GetComponentsInChildren<EnemyHandler>())
                {
                    handler.Dead();
                }
            }

            GameFlowManager.Manager.AllKill();
        }

        private IEnumerator StartRoutine()
        {
            while (!healthHandler.IsDead)
            {
                yield return new WaitForSeconds(Constant.Boss.ATTACKTERM);
                int creatureCount = CardZoneManager.Manager.GridRootTransform()
                    .GetComponentsInChildren<CreatureHandler>().Length;
                Debug.Log(creatureCount);
                yield return new WaitUntil(() => creatureCount > 0);
                
                int attackPattern = attackHandler.GetAttackPatternIndex();
                animationHandler.AttackAnimation(attackPattern);
                attackHandler.ActiveBossPattern(attackPattern).Invoke();
            }
        }
    }
}