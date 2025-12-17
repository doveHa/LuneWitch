using Script.Core.Handler;

namespace Script.Creature.Handler
{
    public class CreatureAnimationHandler : AnimationHandler
    {
        protected override void SetParameter()
        {
            SetAttackParameter("Attack");
            SetHitParameter("Hit");
            SetDeathParameter("Death");
        }
    }
}