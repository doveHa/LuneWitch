using UnityEngine;

public class ChapterSelectManager : MonoBehaviour
{
    public void SelectChapter1()
    {
        PlayerPrefs.SetString("SelectedChapter", "Chapter1");
        PlayerPrefs.Save();
    }

    public void SelectChapter2()
    {
        PlayerPrefs.SetString("SelectedChapter", "Chapter2");
        PlayerPrefs.Save();
    }
}
