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
            UseCheck();
        }

        public void UseCard()
        {
            CostManager.Manager.UseCost(CreatureHandler.CreatureData.cost);
            SetUsedUI();
        }

        public void UpgradeCard(UpgradeType upgradeType)
        {
            CostManager.Manager.UseCost(CreatureHandler.CreatureData.cost);
            CreatureHandler.UpgradeCreature(upgradeType);
            SetUsedUI();
        }

        public bool IsSummoned()
        {
            return CreatureHandler.CreatureSummonHandler.IsOnSummoned;
        }

        private void SetImage()
        {
            originalImage.sprite = CreatureHandler.CreatureData.characterImage;
            moveImage.sprite = CreatureHandler.CreatureData.characterImage;
            Color moveSpriteColor = moveImage.color;
            moveSpriteColor.a = 0.6f;
            moveImage.color = moveSpriteColor;
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

        private void SetUsedUI()
        {
            IsUsed = true;
            usedUI.SetActive(true);
        }

        private void VarInitialize()
        {
            descriptionText.text = CreatureHandler.CreatureData.description;
            originalColor = Color.white;
            ColorUtility.TryParseHtmlString("#313131", out cantUseColor);
        }
    }
}