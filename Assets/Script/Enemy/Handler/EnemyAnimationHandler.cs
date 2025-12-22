using Script.Core.Handler;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyAnimationHandler : AnimationHandler
    {
        protected override void SetParameter()
        {
            SetAttackParameter("Attack");
            SetHitParameter("Hit");
            SetDeathParameter("Death");
        }
    }
}