/*
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Card
{
    public class CardCoolTimeHandler : MonoBehaviour
    {
        private Image frameImage, spriteImage;
        private float maxGauge;

        void Start()
        {
            maxGauge = GetComponent<CardSlot>().characterData.cost;
            frameImage = GetComponent<CardSlot>().sI
            spriteImage = GetComponentInChildren<Image>();
            UpdateUI();
        }

        void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            float ratio = StageManager.Manager.CurrentCost / maxGauge;

            Color startColor = new Color(70f / 255f, 70f / 255f, 70f / 255f);
            Color endColor = Color.white;
            frameImage.color = Color.Lerp(startColor, endColor, Mathf.Clamp01(ratio));
            spriteImage.color = Color.Lerp(startColor, endColor, Mathf.Clamp01(ratio));
        }
    }
}*/