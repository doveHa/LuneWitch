using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Script.Manager
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private float RecoverCostPerSec;
        [SerializeField] private string roundTitle;
        [SerializeField] private TextMeshProUGUI WaveText, smallWaveTitle;
        [SerializeField] private Sprite backGroundImage;
        [SerializeField] private Transform cardSet;
        [SerializeField] private GameObject cardSlotPrefab;
        [SerializeField] private Text currentCostText;

        private GameObject player;
        private GameObject dackList;
        public float CurrentCost { get; private set; }

        public static StageManager Manager { get; private set; }

        void Awake()
        {
            if (Manager == null)
            {
                Manager = this;
            }
        }

        void Start()
        {
            SetCards();
        }

        void FixedUpdate()
        {
            CurrentCost += Time.deltaTime * RecoverCostPerSec;
            currentCostText.text = ((int)CurrentCost).ToString();
        }

        private void SetCards()
        {
            foreach (CharacterData character in PlayerManager.Manager.SelectedCreatures)
            {
                GameObject obj = Instantiate(cardSlotPrefab, cardSet);
                obj.GetComponent<CardSlot>().InitializeCard(character);
            }
        }
    }
}