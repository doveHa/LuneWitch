using Script.Creature;
using Script.Creature.Handler;

namespace Script.Stage
{
    public class CreatureObjectTooltip : TooltipTrigger
    {
        protected override void SetTooltipText()
        {
            CombatHandler stat = GetComponent<CombatHandler>();
            string text = $"{stat.Health}/{stat.MaxHealth}";
            tooltipText = text;
        }
    }
}