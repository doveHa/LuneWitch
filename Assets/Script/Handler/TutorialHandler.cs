using System;
using Script;
using Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TutorialHandler : MonoBehaviour
{
    private Sprite[] pageSprites;
    private string[] tutorialDescription;

    [SerializeField] private Image[] dotSet;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    private int currentPage = 0;
    private Sprite onDotImage, offDotImage;

    public void Start()
    {
        pageSprites = ResourceManager.LoadAll<Sprite>(Constant.ResourcePath.TUTORIAL_IMAGES_PATH);
        Debug.Log(pageSprites.Length);
        onDotImage = ResourceManager.Load<Sprite>(Constant.ResourcePath.UI_PATH_BY_NAME("OnDot"));
        offDotImage = ResourceManager.Load<Sprite>(Constant.ResourcePath.UI_PATH_BY_NAME("OffDot"));
        Array.Sort(pageSprites, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));

        tutorialDescription = new[]
        {
            "<color=#EFCD5D>하트 아이콘</color>은 <color=#EFCD5D>마나</color>를 나타냅니다. \\n마나는<color=#EFCD5D> 4초마다 자동 생성</color>되며, <color=#EFCD5D>마력석</color> 설치 시 추가로 생성됩니다.",
            "하단의 카드는 <color=#EFCD5D>사용 가능한 소환수</color>와 <color=#EFCD5D>소모 마나</color>를 나타냅니다.\\n 각 카드는 <color=#EFCD5D>쿨타임</color>을 가지며, 쿨타임이 끝나고 <color=#EFCD5D>마나가 충분할 때</color> 사용할 수 있습니다.\n",
            "<color=#EFCD5D>소환수 카드를 타일로 드래그</color>하여 배치할 수 있습니다.\\n 배치된 캐릭터는 자동으로 적을 향해 공격을 시작합니다. \n",
            "마녀 캐릭터는 <color=#EFCD5D>궁극기</color>를 사용할 수 있으며, <color=#EFCD5D>60초마다 자동으로 충전</color>됩니다.\\n게이지가 모두 차면 <color=#EFCD5D>궁극기 버튼이 활성화</color>되어 사용할 수 있습니다."
        };
    }

    public void Open()
    {
        Initialize();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Close()
    {
        Debug.Log("Press Close Button");
        Initialize();
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void NextPage()
    {
        if (currentPage < dotSet.Length - 1)
        {
            dotSet[currentPage++].sprite = offDotImage;
            dotSet[currentPage].sprite = onDotImage;
            SetCurrentPageTutorial();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            dotSet[currentPage--].sprite = offDotImage;
            dotSet[currentPage].sprite = onDotImage;
            SetCurrentPageTutorial();
        }
    }

    private void Initialize()
    {
        currentPage = 0;
        SetCurrentPageTutorial();
    }

    private void SetCurrentPageTutorial()
    {
        tutorialImage.sprite = pageSprites[currentPage];
        tutorialText.text = tutorialDescription[currentPage];
    }
}