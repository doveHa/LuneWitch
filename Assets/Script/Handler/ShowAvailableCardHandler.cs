using System.Collections.Generic;
using Script;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.DataDefinitions.ScriptableObjects;
using UnityEngine;
using Script.Manager;
using Script.UI;

public class ShowAvailableCardHandler : MonoBehaviour
{
    public Transform availableCardParent;
    public Transform selectedCardParent;
    public GameObject cardPrefab;

    private int maxSelectCount = 4;

    private List<CreatureData> displayedCharacters;

    void Awake()
    {
        displayedCharacters = new List<CreatureData>();
    }

    void Start()
    {
        UpdateDisplayedCharacters();
        RefreshCardsPool(availableCardParent, displayedCharacters);
        RefreshCardsPool(selectedCardParent, PlayerManager.Manager.SelectedCreatures);
    }

    void UpdateDisplayedCharacters()
    {
        foreach (var character in UnlockedCharacterManager.Manager.allCharacterData)
        {
            displayedCharacters.Add(character.Value);
        }
    }

    void OnCardClicked(CreatureData creature)
    {
        if (PlayerManager.Manager.SelectedCreatures.Contains(creature))
        {
            PlayerManager.Manager.SelectedCreatures.Remove(creature);
        }
        else
        {
            PlayerManager.Manager.AddCreature(creature);
        }

        RefreshCardsPool(selectedCardParent, PlayerManager.Manager.SelectedCreatures);
    }

    private void RefreshCardsPool(Transform parent, List<CreatureData> cards)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);

        foreach (var character in cards)
        {
            var card = Instantiate(cardPrefab, parent);
            card.GetComponent<CardSlot>().CardInitialize(character, OnCardClicked);
        }
    }
}