using Script.Core.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Core.Handler
{
    public abstract class BaseHandler : MonoBehaviour
    {
        protected AnimationHandler AnimationHandler;
        public HealthHandler HealthHandler;
        
        public void Hit(int damage)
        {
            AnimationHandler.PlayHitAnimation();
            HealthHandler.Hit(damage);

            if (HealthHandler.IsDead)
            {
                StopAllCoroutines();
                Dead();
            }
        }

        public abstract void Dead();
        public abstract void Initialize(BaseData data);
    }
}