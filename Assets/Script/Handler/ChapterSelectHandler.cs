using Script.Manager;
using UnityEngine;

public class ChapterSelectHandler : MonoBehaviour
{
    [SerializeField] private GameObject deckSelectPanel;

    public void ShowDeckSelection(int chapter)
    {
        SceneLoadManager.SelectedChapterNo = chapter;
        SceneLoadManager.SelectedRoundNo = 1;
        deckSelectPanel.SetActive(true);
    }
}