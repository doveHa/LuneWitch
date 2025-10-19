using System.Collections.Generic;
using UnityEngine;

public class InGameCardLoader : MonoBehaviour
{
    public List<GameObject> allCardObjects; // ���� �̸� ��ġ�� ī���

    private Dictionary<string, GameObject> cardDict = new Dictionary<string, GameObject>();

    void Start()
    {
        foreach (var card in allCardObjects)
        {
            card.SetActive(false);
            cardDict[card.name] = card;
        }

        var selectedNames = InGameManager2.Instance.selectedCharacterNames;

        for (int i = 0; i < selectedNames.Count; i++)
        {
            string name = selectedNames[i];

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
}
