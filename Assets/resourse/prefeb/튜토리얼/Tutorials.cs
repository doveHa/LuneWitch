using UnityEngine;
using UnityEngine.UI;

public class Tutorials : MonoBehaviour
{
    public Sprite[] Sprite;
    public string[] TutorialsName;

    [Header("페이지 인디케이터")]
    public Image[] dotImages; // 점들 (Image 컴포넌트)
    public Sprite dotActiveSprite;
    public Sprite dotInactiveSprite;

    public void SetPageFromBolt(int index)
    {
        SetPageIndicator(index);
    }

    private void SetPageIndicator(int index)
    {
        for (int i = 0; i < dotImages.Length; i++)
        {
            dotImages[i].sprite = (i == index) ? dotActiveSprite : dotInactiveSprite;
        }
    }
}
