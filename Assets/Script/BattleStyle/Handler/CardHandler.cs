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
        [SerializeField] private GameObject usedUI;
        [SerializeField] private TextMeshProUGUI descriptionText;
        
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
            usedUI.SetActive(false);
            VarInitialize();
            SetImage();
            UseCheck();
        }

        public void UseCard()
        {
            CostManager.Manager.UseCost(CreatureHandler.Card.cost);
            SetUsedUI();
        }

        public void UpgradeCard()
        {
            CostManager.Manager.UseCost(CreatureHandler.Card.cost);
            CardPoolManager.Manager.UpgradeCard(CreatureHandler);
            SetUsedUI();
        }

        public bool IsSummoned()
        {
            return CreatureHandler.IsOnSummoned;
        }

        private void SetImage()
        {
            originalImage.sprite = CreatureHandler.Card.characterImage;
            moveImage.sprite = CreatureHandler.Card.characterImage;
            Color moveSpriteColor = moveImage.color;
            moveSpriteColor.a = 0.6f;
            moveImage.color = moveSpriteColor;
        }

        private void UseCheck()
        {
            if (CostManager.Manager.Cost < CreatureHandler.Card.cost)
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
            usedUI.SetActive(true);
        }

        private void VarInitialize()
        {
            descriptionText.text = CreatureHandler.Card.description;
            originalColor = Color.white;
            ColorUtility.TryParseHtmlString("#313131", out cantUseColor);
        }
    }
}