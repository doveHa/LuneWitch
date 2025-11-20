using Script.BattleStyle.DataDefinitions.Data;
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

        public void SetCard(CreatureSummonCard card)
        {
            CardData = card;
            SetImage();
            SetDescription();
        }

        public void UseCard()
        {
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
    }
}