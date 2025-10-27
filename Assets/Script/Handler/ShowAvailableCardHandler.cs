using System.Collections.Generic;
using Script;
using UnityEngine;
using Script.Manager;

public class ShowAvailableCardHandler : MonoBehaviour
{
    public Transform availableCardParent;
    public Transform selectedCardParent;
    public GameObject cardPrefab;

    private int maxSelectCount = 4;

    private List<CharacterData> displayedCharacters;

    void Awake()
    {
        displayedCharacters = new List<CharacterData>();
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
            if (character.Value.isUnlocked)
            {
                displayedCharacters.Add(character.Value);
            }
        }
    }

    void OnCardClicked(CharacterData character)
    {
        if (PlayerManager.Manager.SelectedCreatures.Contains(character))
        {
            PlayerManager.Manager.SelectedCreatures.Remove(character);
        }
        else
        {
            PlayerManager.Manager.AddCreature(character);
        }

        RefreshCardsPool(selectedCardParent, PlayerManager.Manager.SelectedCreatures);
    }

    private void RefreshCardsPool(Transform parent, List<CharacterData> cards)
    {
        foreach (Transform child in parent)
            Destroy(child.gameObject);

        foreach (var character in cards)
        {
            var card = Instantiate(cardPrefab, parent);
            card.GetComponent<CardUI>().Setup(character, OnCardClicked, false);
        }
    }
}