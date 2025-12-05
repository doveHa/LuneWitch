using Script.BattleStyle.DataDefinitions.Data;
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
        [SerializeField] private TextMeshProUGUI descriptionText;

        public int Cost { get; private set; }
        public bool CanUseCard { get; private set; } = true;

        public CreatureHandler CreatureHandler { get; private set; }
        private Color originalColor;
        private Color cantUseColor;

        void Update()
        {
            UseCheck();
        }

        public void SetCard(CreatureHandler creature)
        {
            CreatureHandler = creature;
            VarInitialize();
            SetImage();
            UseCheck();
        }

        public void UseCard()
        {
            if (CanUseCard)
            {
                CostManager.Manager.UseCost(Cost);
                CantUseCard();
            }
        }

        public void UpgradeCard()
        {
            if (CanUseCard)
            {
                CostManager.Manager.UseCost(Cost);
                CardPoolManager.Manager.UpgradeCard(CreatureHandler);
                CantUseCard();
            }
        }

        public bool IsSummoned()
        {
            return CreatureHandler.IsOnSummoned;
        }

        private void SetImage()
        {
            originalImage.sprite = CreatureHandler.Card.CreatureImage;
            moveImage.sprite = CreatureHandler.Card.CreatureImage;
            Color moveSpriteColor = moveImage.color;
            moveSpriteColor.a = 0.6f;
            moveImage.color = moveSpriteColor;
        }

        private void UseCheck()
        {
            if (CostManager.Manager.Cost < Cost)
            {
                CanUseCard = false;
                originalImage.color = cantUseColor;
            }
            else
            {
                CanUseCard = true;
                GetComponent<PointerHandler>().CanDrag = true;
                originalImage.color = originalColor;
            }
        }

        private void CantUseCard()
        {
            CanUseCard = false;
            originalImage.color = cantUseColor;
            originalColor = cantUseColor;
        }

        private void VarInitialize()
        {
            descriptionText.text = CreatureHandler.Card.Description;
            Cost = CreatureHandler.Card.Cost;
            originalColor = Color.white;
            ColorUtility.TryParseHtmlString("#313131", out cantUseColor);
        }
    }
}