using UnityEngine;

namespace Script.Card
{
    public class DescriptionTooltip : TooltipTrigger
    {
        [TextArea] [SerializeField] private string description;

        protected override void SetTooltipText()
        {
            tooltipText = description;
        }
    }
}