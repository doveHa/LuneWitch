namespace Script.Stage
{
    public class CardSlotTooltip : TooltipTrigger
    {
        protected override void SetTooltipText()
        {
            tooltipText = GetComponent<CardSlotTemp>().characterData.description;
        }
    }
}