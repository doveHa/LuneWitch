using System;
using Script;
using Script.Core.Manager;
using Script.Stage.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Script.Manager;
using System.Linq;

public class TutorialHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Image backGroundImage;
    [SerializeField] private Image[] dotSet;

    [Header("Data")]
    [SerializeField] private Sprite[] pageSprites;

    [SerializeField] private Sprite onDotImage;
    [SerializeField] private Sprite offDotImage;

    private string[] tutorialDescription;
    private int currentPage = 0;

    public void Start()
    {
        tutorialDescription = new[]
        {
            "<color=#EFCD5D>하트 아이콘</color>은 <color=#EFCD5D>마나</color>를 나타냅니다. \\n마나는<color=#EFCD5D> 4초마다 자동 생성</color>되며, <color=#EFCD5D>마력석</color> 설치 시 추가로 생성됩니다.",
            "하단의 카드는 <color=#EFCD5D>사용 가능한 소환수</color>와 <color=#EFCD5D>소모 마나</color>를 나타냅니다.\\n 각 카드는 <color=#EFCD5D>쿨타임</color>을 가지며, 쿨타임이 끝나고 <color=#EFCD5D>마나가 충분할 때</color> 사용할 수 있습니다.\n",
            "<color=#EFCD5D>소환수 카드를 타일로 드래그</color>하여 배치할 수 있습니다.\\n 배치된 캐릭터는 자동으로 적을 향해 공격을 시작합니다.\\n오른쪽 버튼을 눌러 사용할 소환수 카드를 다시 뽑을 수 있습니다.",
            "카드 오른쪽의 <color=#EFCD5D>마도서</color>를 필드의 소환수에게 가져다 놓으면\\n소환수를 <color=#EFCD5D>판매</color>할 수 있습니다.",
            "마녀 캐릭터는 <color=#EFCD5D>궁극기</color>를 사용할 수 있으며, <color=#EFCD5D>60초마다 자동으로 충전</color>됩니다.\\n게이지가 모두 차면 <color=#EFCD5D>궁극기 버튼이 활성화</color>되어 사용할 수 있습니다."
        };

        if (pageSprites.Length != tutorialDescription.Length)
        {
            Debug.LogWarning($"이미지({pageSprites.Length}장)와 설명({tutorialDescription.Length}개)의 개수가 다릅니다!");
        }

        Initialize();
    }

    public void Open()
    {
        Initialize();
        backGroundImage.sprite = GameFlowManager.Manager.CurrentStage.StageBackGround();
        transform.GetChild(0).gameObject.SetActive(true);

        if (TimeScaleManager.Manager != null)
            TimeScaleManager.Manager.PauseGame();
    }

    public void Close()
    {
        Debug.Log("Press Close Button");
        Initialize();
        transform.GetChild(0).gameObject.SetActive(false);

        if (TimeScaleManager.Manager != null)
            TimeScaleManager.Manager.ResumeGame();
    }

    public void NextPage()
    {
        /*        if (currentPage < dotSet.Length - 1)
                {
                    dotSet[currentPage++].sprite = offDotImage;
                    dotSet[currentPage].sprite = onDotImage;
                    SetCurrentPageTutorial();
                }*/
        if (currentPage < pageSprites.Length - 1)
        {
            currentPage++;
            UpdateUI();
        }
    }

    public void PrevPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateUI();
        }
    }

    private void Initialize()
    {
        currentPage = 0;
        //SetCurrentPageTutorial();
        UpdateUI();
    }

    private void UpdateUI()
    {
        // 1. 이미지 및 텍스트 갱신
        // (인덱스 범위 체크로 에러 방지)
        if (currentPage < pageSprites.Length)
            tutorialImage.sprite = pageSprites[currentPage];

        if (currentPage < tutorialDescription.Length)
            tutorialText.text = tutorialDescription[currentPage];

        // 2. 점(Dot) 상태 갱신
        for (int i = 0; i < dotSet.Length; i++)
        {
            // 점이 이미지 페이지 수보다 많을 경우를 대비해 숨기거나 처리할 수도 있음
            if (i >= pageSprites.Length)
            {
                dotSet[i].gameObject.SetActive(false); // 페이지보다 많은 점은 숨김
                continue;
            }

            dotSet[i].gameObject.SetActive(true);

            // 현재 페이지면 On, 아니면 Off
            if (i == currentPage)
            {
                dotSet[i].sprite = onDotImage;
            }
            else
            {
                dotSet[i].sprite = offDotImage;
            }
        }
    }

    private void SetCurrentPageTutorial()
    {
        tutorialImage.sprite = pageSprites[currentPage];
        tutorialText.text = tutorialDescription[currentPage];
    }
}