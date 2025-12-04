using System;
using Script.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI
{
    public class CardSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI name;

        public void CardInitialize(CharacterData characterData, Action<CharacterData> callback)
        {
            image.sprite = characterData.characterImage;
            this.name.text = characterData.characterName_Kr;
            GetComponent<Button>().onClick.AddListener(delegate { callback(characterData); });
        }
    }
}