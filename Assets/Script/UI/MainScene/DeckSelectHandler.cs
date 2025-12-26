using System.Collections.Generic;
using Script.Creature.DataDefinitions.ScriptableObjects;
using UnityEngine;
using Script.Manager;


namespace Script.UI.MainScene
{
    public class DeckSelectHandler : MonoBehaviour
    {
        public Transform availableCardParent;
        public Transform selectedCardParent;
        public GameObject cardPrefab;

        private Dictionary<CreatureData, GameObject> cards;

        void Awake()
        {
            cards = new Dictionary<CreatureData, GameObject>();
        }

        void Start()
        {
            SetDisplayedCreature();
            SelectedCardRefresh();
        }

        void SetDisplayedCreature()
        {
            foreach (var creature in PlayerManager.Manager.AllCreatureData)
            {
                var card = Instantiate(cardPrefab, availableCardParent);
                card.GetComponent<CardSlot>().CardInitialize(creature.Value, OnCardClicked);
                cards.Add(creature.Value, card);
            }
        }

        void OnCardClicked(CreatureData creature)
        {
            if (PlayerManager.Manager.SelectedCreatures.Contains(creature))
            {
                PlayerManager.Manager.SelectedCreatures.Remove(creature);
                cards[creature].SetActive(true);
            }
            else if (PlayerManager.Manager.AddCreature(creature))
            {
                cards[creature].SetActive(false);
            }

            SelectedCardRefresh();
        }

        private void SelectedCardRefresh()
        {
            foreach (Transform child in selectedCardParent)
                Destroy(child.gameObject);

            foreach (var character in PlayerManager.Manager.SelectedCreatures)
            {
                var card = Instantiate(cardPrefab, selectedCardParent);
                card.GetComponent<CardSlot>().CardInitialize(character, OnCardClicked);
            }
        }
    }
}