using System;
using Script.Creature.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI name;

        public void CardInitialize(CreatureData creatureData, Action<CreatureData> callback)
        {
            image.sprite = creatureData.characterImage;
            name.text = creatureData.name_Kr;
            GetComponent<Button>().onClick.AddListener(delegate { callback(creatureData); });
        }
    }
}