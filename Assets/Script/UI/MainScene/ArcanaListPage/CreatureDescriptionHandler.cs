using Script.Creature.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.MainScene.ArcanaListPage
{
    public class CreatureDescriptionHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI creatureName, hpText, atkText, costText, descriptionText;
        [SerializeField] private Image creatureIcon;

        private Vector3 initialPosition;

        public void Start()
        {
            initialPosition = transform.position;
        }

        public void SetDescription(CreatureData creature)
        {
            creatureName.text = creature.name;
            hpText.text = creature.health.ToString();
            atkText.text = creature.attack.ToString();
            costText.text = creature.cost.ToString();
            descriptionText.text = creature.description;

            creatureIcon.sprite = creature.characterImage;
            creatureIcon.SetNativeSize();
        }

        public void PositionInitialize()
        {
            gameObject.GetComponent<RectTransform>().position = initialPosition;
        }
    }
}