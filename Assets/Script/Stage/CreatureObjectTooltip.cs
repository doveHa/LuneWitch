using Script.Creature;

namespace Script.Stage
{
    public class CreatureObjectTooltip : TooltipTrigger
    {
        protected override void SetTooltipText()
        {
            CreatureStat stat = GetComponent<CreatureStat>();
            string text = $"{stat.Health}/{stat.MaxHealth}";
            tooltipText = text;
        }
    }
}