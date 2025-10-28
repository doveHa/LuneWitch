namespace Script.Stage
{
    public class CardSlotTooltip : TooltipTrigger
    {
        protected override void SetTooltipText()
        {
            tooltipText = GetComponent<CardSlot>().characterData.description;
        }
    }
}