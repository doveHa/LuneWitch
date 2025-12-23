using Script.BattleStyle.Manager;
using Script.Core.Handler;
using Script.Creature;
using Script.Creature.AttackHandler;
using Script.Creature.Handler;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Stage
{
    public class CreatureObjectHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject tooltipPanel;

        private string tooltipText;
        private bool isTooltipActive;

        private void Awake()
        {
            tooltipPanel.SetActive(false);
        }

        void Update()
        {
            if (isTooltipActive)
            {
                AdjustTooltipText();
            }
        }

        private void AdjustTooltipText()
        {
            HealthHandler handler = GetComponent<CreatureHandler>().HealthHandler;
            tooltipPanel.transform.GetChild(0).GetComponent<TextMeshPro>().text =
                $"{handler.Health}/{handler.MaxHealth}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isTooltipActive = true;
            AdjustTooltipText();
            tooltipPanel.SetActive(true);
            CardZoneManager.Manager.AttackRangeVisuals(GetComponent<BaseAttackHandler>().VisualizeAttackRange());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isTooltipActive = false;
            tooltipPanel.SetActive(false);
            CardZoneManager.Manager.AttackRangeNormalize(GetComponent<BaseAttackHandler>().VisualizeAttackRange());
        }
    }
}