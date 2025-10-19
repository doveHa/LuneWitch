using System.Collections.Generic;
using UnityEngine;

public class InGameCardLoader : MonoBehaviour
{
    public List<GameObject> allCardObjects; // 씬에 미리 배치된 카드들

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
                Debug.LogWarning("해당 이름의 카드 없음: " + name);
            }
        }
    }
}
