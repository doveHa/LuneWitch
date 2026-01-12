using UnityEngine;
using UnityEngine.UI;

public class StoryDirector : MonoBehaviour
{
    [Header("Background & CG")]
    public Image backgroundImage;
    public Image illustrationImage; // CG용
    public Image overlayImage;      // 팝업 아이템용

    [Header("Characters")]
    public Image leftCharImage;
    public Image rightCharImage;

    [Header("Effects")]
    public Animator effectAnimator;

    [Header("Settings")]
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float activeScale = 1.0f;
    public float inactiveScale = 0.85f;

    // 초기화
    public void Init()
    {
        if (illustrationImage) illustrationImage.gameObject.SetActive(false);
        if (overlayImage) overlayImage.gameObject.SetActive(false);
        if (leftCharImage) leftCharImage.gameObject.SetActive(false);
        if (rightCharImage) rightCharImage.gameObject.SetActive(false);
        if (effectAnimator) effectAnimator.gameObject.SetActive(false);
    }

    // 연출
    public void UpdateSceneVisuals(DialogueData storyData, DialogueLine line)
    {
        // 1. 이벤트 명령어 처리 (CG, Pop, Anim 등)
        ProcessEventCommand(line.eventCommand, storyData);

        // 2. 모드에 따른 이미지 갱신
        if (storyData.isIllustrationMode)
        {
            // CG 모드: 캐릭터 끔
            if (leftCharImage) leftCharImage.gameObject.SetActive(false);
            if (rightCharImage) rightCharImage.gameObject.SetActive(false);

            // CG 이미지는 ProcessEventCommand나 Intro에서 켜짐
            if (illustrationImage && storyData.illustrationSprite != null)
            {
                illustrationImage.sprite = storyData.illustrationSprite;
                illustrationImage.gameObject.SetActive(true);
            }
        }
        else
        {
            // 일반 모드: CG 끄고 캐릭터 켬
            if (illustrationImage) illustrationImage.gameObject.SetActive(false);

            UpdateCharacterImages(line);
            HighlightSpeaker(line.speakerType);
        }
    }

    // 배경 교체 (인트로용)
    public void SetBackground(Sprite bgSprite)
    {
        if (backgroundImage != null && bgSprite != null)
        {
            backgroundImage.sprite = bgSprite;
        }
    }

    // 명령어 해석기
    void ProcessEventCommand(string command, DialogueData storyData)
    {
        if (string.IsNullOrEmpty(command)) return;

        string[] parts = command.Split(':');
        string type = parts[0].ToUpper();
        string value = parts.Length > 1 ? parts[1] : "";

        switch (type)
        {
            case "POP":
                if (value == "OFF") overlayImage.gameObject.SetActive(false);
                else
                {
                    Sprite s = GetSprite(value);
                    if (s) { overlayImage.sprite = s; overlayImage.gameObject.SetActive(true); }
                }
                break;
            case "CG":
                if (value == "OFF")
                {
                    storyData.isIllustrationMode = false;
                    illustrationImage.gameObject.SetActive(false);
                }
                else
                {
                    storyData.isIllustrationMode = true;
                    Sprite s = GetSprite(value);
                    if (s) { illustrationImage.sprite = s; illustrationImage.gameObject.SetActive(true); }
                }
                break;
            case "ANIM":
                if (effectAnimator)
                {
                    effectAnimator.gameObject.SetActive(true);
                    effectAnimator.Play(value, -1, 0f);
                }
                break;
        }
    }

    // 캐릭터 이미지 업데이트
    void UpdateCharacterImages(DialogueLine line)
    {
        SetImage(leftCharImage, line.leftSprite);
        SetImage(rightCharImage, line.rightSprite);
    }

    void SetImage(Image target, Sprite sprite)
    {
        if (target != null && sprite != null)
        {
            target.sprite = sprite;
            target.preserveAspect = true;
            target.gameObject.SetActive(true);
        }
    }

    // 화자 강조 (밝기/크기 조절)
    void HighlightSpeaker(SpeakerType speaker)
    {
        if (leftCharImage.gameObject.activeSelf)
            SetVisualState(leftCharImage, speaker == SpeakerType.Left);

        if (rightCharImage.gameObject.activeSelf)
            SetVisualState(rightCharImage, speaker == SpeakerType.Right);
    }

    void SetVisualState(Image target, bool isActive)
    {
        RectTransform rect = target.GetComponent<RectTransform>();
        if (isActive)
        {
            rect.localScale = Vector3.one * activeScale;
            target.color = Color.white;
            rect.SetAsLastSibling();
        }
        else
        {
            rect.localScale = Vector3.one * inactiveScale;
            target.color = inactiveColor;
        }
    }

    // 리소스 로드 헬퍼
    Sprite GetSprite(string name) => Resources.Load<Sprite>("Images/" + name);
}