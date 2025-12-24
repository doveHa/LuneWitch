using Script.Manager;
using UnityEngine;

namespace Script.UI.MainScene
{
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
}