using System.Text;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.UI.Pointer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.BattleStyle.Handler
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private Image originalImage;
        [SerializeField] private Image moveImage;
        [SerializeField] private GameObject usedUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI costText;
        [SerializeField] private GameObject upgradePanel;
        public bool IsUsed { get; private set; }

        public CreatureHandler CreatureHandler { get; private set; }
        private Color originalColor;
        private Color cantUseColor;


        void Update()
        {
            UseCheck();
        }

        public void SetCard(CreatureHandler creatureHandler)
        {
            CreatureHandler = creatureHandler;
            usedUI.SetActive(false);
            IsUsed = false;
            VarInitialize();
            SetImage();
            SetCost();
            SetDescription();
            UseCheck();
        }

        public void UseCard()
        {
            CostManager.Manager.UseCost(CreatureHandler.CreatureSummonHandler.Cost);
            SetUsedUI();
        }

        public void UpgradeCard(UpgradeType upgradeType)
        {
            CostManager.Manager.UseCost(CreatureHandler.CreatureSummonHandler.Cost);
            CreatureHandler.UpgradeCreature(upgradeType);
            SetUsedUI();
        }

        public bool IsSummoned()
        {
            return CreatureHandler.CreatureSummonHandler.IsOnSummoned;
        }

        private void UseCheck()
        {
            if (CostManager.Manager.Cost < CreatureHandler.CreatureData.cost)
            {
                GetComponent<PointerHandler>().CanDrag = false;
                originalImage.color = cantUseColor;
            }
            else
            {
                GetComponent<PointerHandler>().CanDrag = true;
                originalImage.color = originalColor;
            }
        }

        public void SetUsedUI()
        {
            IsUsed = true;
            upgradePanel.SetActive(false);
            usedUI.SetActive(true);
        }

        private void VarInitialize()
        {
            descriptionText.text = CreatureHandler.CreatureData.description;
            originalColor = Color.white;
            ColorUtility.TryParseHtmlString("#313131", out cantUseColor);
        }

        private void SetImage()
        {
            originalImage.sprite = CreatureHandler.CreatureData.characterImage;
            moveImage.sprite = CreatureHandler.CreatureData.characterImage;
            Color moveSpriteColor = moveImage.color;
            moveSpriteColor.a = 0.6f;
            moveImage.color = moveSpriteColor;
        }

        private void SetCost()
        {
            string costString = CreatureHandler.CreatureData.cost.ToString();
            if (CreatureHandler.CreatureSummonHandler.IsOnSummoned)
            {
                costString = CreatureHandler.CreatureSummonHandler.Cost.ToString();
            }

            costText.text = costString;
        }

        private void SetDescription()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(CreatureHandler.CreatureData.description + "\n");
            if (CreatureHandler.CreatureSummonHandler.IsOnSummoned)
            {
                stringBuilder
                    .Append(
                        $"공격력({CreatureHandler.AttackHandler.AtkUpgradeCount}) : {CreatureHandler.AttackHandler.Atk}\n공격속도({CreatureHandler.AttackHandler.AttackTermUpgradeCount}) : {CreatureHandler.AttackHandler.AttackTerm}\n")
                    .Append(
                        $"체력({CreatureHandler.HealthHandler.HealthUpgradeCount}) : {CreatureHandler.HealthHandler.MaxHealth}\n");
            }
            else
            {
                stringBuilder
                    .Append(
                        $"공격력 : {CreatureHandler.CreatureData.attack}\n공격속도 : {CreatureHandler.CreatureData.attackTerm}\n")
                    .Append($"체력 : {CreatureHandler.CreatureData.health}\n");
            }

            descriptionText.text = stringBuilder.ToString();
        }
    }
}