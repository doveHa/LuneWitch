using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeckSelectManager : MonoBehaviour
{
    public Transform availableCardParent;
    public Transform selectedCardParent;
    public GameObject cardPrefab;

    public int maxSelectCount = 6;

    private List<CharacterData> displayedCharacters = new List<CharacterData>();
    private List<CharacterData> selectedCharacters = new List<CharacterData>();

    public Button confirmButton;
    public Button resetButton;

    private readonly string[] defaultCharacters = new string[] { "마력석", "펌피", "실룸" };

    void Start()
    {
        UpdateDisplayedCharacters();
        RenderAvailableCards();
        RefreshSelectedCards();

        confirmButton.onClick.AddListener(ConfirmDeck);
        if (resetButton != null)
            resetButton.onClick.AddListener(ResetToDefaultCharacters);
    }

    void UpdateDisplayedCharacters()
    {
        displayedCharacters.Clear();

        if (UnlockedCharacterManager.Instance != null)
        {
            List<CharacterData> unlocked = UnlockedCharacterManager.Instance.GetUnlockedCharacters();
            displayedCharacters.AddRange(unlocked);
        }
        else
        {
            foreach (var name in defaultCharacters)
            {
                var character = FindCharacterByName(name);
                if (character != null)
                    displayedCharacters.Add(character);
            }
        }
    }

    void RenderAvailableCards()
    {
        foreach (Transform child in availableCardParent)
            Destroy(child.gameObject);

        foreach (var character in displayedCharacters)
        {
            var cardGO = Instantiate(cardPrefab, availableCardParent);
            cardGO.GetComponent<CardUI>().Setup(character, OnCardClicked, true);
        }
    }

    void OnCardClicked(CharacterData character)
    {
        if (selectedCharacters.Contains(character))
        {
            selectedCharacters.Remove(character);
        }
        else if (selectedCharacters.Count < maxSelectCount)
        {
            selectedCharacters.Add(character);
        }
        RefreshSelectedCards();
    }

    void RefreshSelectedCards()
    {
        foreach (Transform child in selectedCardParent)
            Destroy(child.gameObject);

        foreach (var character in selectedCharacters)
        {
            var cardGO = Instantiate(cardPrefab, selectedCardParent);
            cardGO.GetComponent<CardUI>().Setup(character, OnCardClicked, false);
        }
    }

    public bool AreAllSlotsFilled()
    {
        return selectedCharacters.Count == maxSelectCount;
    }

    void ConfirmDeck()
    {
        if (!AreAllSlotsFilled())
        {
            return;
        }

        InGameManager2.Instance.selectedCharacterNames.Clear();
        foreach (var character in selectedCharacters)
        {
            InGameManager2.Instance.selectedCharacterNames.Add(character.characterName);
        }

        StartCoroutine(HandleSceneTransition());
    }

    private IEnumerator HandleSceneTransition()
    {
        yield return new WaitForSeconds(0.5f);

        string chapter = PlayerPrefs.GetString("SelectedChapter", "Chapter1");
        if (chapter == "Chapter1")
        {
            SceneManager.LoadScene("Chapter 1 Story");
        }
        else if (chapter == "Chapter2")
        {
            SceneManager.LoadScene("Chapter 2 Story");
        }
    }

    void ResetToDefaultCharacters()
    {
        if (UnlockedCharacterManager.Instance == null)
            return;

        List<string> toRemove = new List<string>();
        foreach (var c in UnlockedCharacterManager.Instance.GetUnlockedCharacters())
        {
            if (System.Array.IndexOf(defaultCharacters, c.characterName) < 0)
                toRemove.Add(c.characterName);
        }

        foreach (var name in toRemove)
        {
            var field = typeof(UnlockedCharacterManager).GetField("unlockedCharacterNames", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                var set = (HashSet<string>)field.GetValue(UnlockedCharacterManager.Instance);
                if (set.Contains(name))
                {
                    set.Remove(name);
                }
            }
        }

        UnlockedCharacterManager.Instance.SaveUnlockedCharacters();

        selectedCharacters.Clear();
        UpdateDisplayedCharacters();
        RenderAvailableCards();
        RefreshSelectedCards();
    }

    private CharacterData FindCharacterByName(string name)
    {
        if (UnlockedCharacterManager.Instance == null)
            return null;

        foreach (var c in UnlockedCharacterManager.Instance.allCharacters)
        {
            if (c.characterName == name)
                return c;
        }
        return null;
    }
}
