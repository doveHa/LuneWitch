using Script.Core.Handler;
using UnityEngine;

namespace Script.Enemy.Handler
{
    public class EnemyHealthHandler : HealthHandler
    {
        public EnemyHealthHandler(int maxHealth) : base(maxHealth){}
    }
}