using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
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

        public CreatureSummonCard CardData { get; private set; }
        private Color originalColor;
        private Color cantUseColor;

        void Update()
        {
            UseCheck();
        }

        public void SetCard(CreatureSummonCard card)
        {
            VarInitialize(card);
            SetImage();
            SetDescription();
            UseCheck();
        }

        public void UseCard()
        {
            CostManager.Manager.UseCost(CardData.Cost);
            CantUseCard();
        }

        private void SetImage()
        {
            originalImage.sprite = CardData.CreatureImage;
            moveImage.sprite = CardData.CreatureImage;
            Color moveSpriteColor = moveImage.color;
            moveSpriteColor.a = 0.6f;
            moveImage.color = moveSpriteColor;
        }

        private void SetDescription()
        {
            descriptionText.text = CardData.Description;
        }

        private void UseCheck()
        {
            if (CostManager.Manager.Cost < CardData.Cost)
            {
                originalImage.color = cantUseColor;
            }
            else
            {
                GetComponent<PointerHandler>().CanDrag = true;
                originalImage.color = originalColor;
            }
        }

        private void CantUseCard()
        {
            originalImage.color = cantUseColor;
            originalColor = cantUseColor;
        }

        private void VarInitialize(CreatureSummonCard card)
        {
            CardData = card;
            originalColor = Color.white;
            ColorUtility.TryParseHtmlString("#313131", out cantUseColor);
        }
    }
}