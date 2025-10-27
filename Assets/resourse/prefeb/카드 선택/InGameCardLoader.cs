/*
using System.Collections.Generic;
using Script.Manager;
using UnityEngine;

public class InGameCardLoader : MonoBehaviour
{
    public List<GameObject> allCardObjects;

    private Dictionary<string, GameObject> cardDict = new Dictionary<string, GameObject>();

    void Start()
    {
        foreach (var card in allCardObjects)
        {
            card.SetActive(false);
            cardDict[card.name] = card;
        }

        var selectedCards = PlayerManager.Manager.SelectedCreatures;

        for (int i = 0; i < selectedCards.Count; i++)
        {
            string name = selectedCards[i].characterName;
            Debug.Log(name);
            if (cardDict.TryGetValue(name, out GameObject card))
            {
                card.SetActive(true);
                card.transform.SetSiblingIndex(i);
            }
            else
            {
                Debug.LogWarning("�ش� �̸��� ī�� ����: " + name);
            }
        }
    }
}*/