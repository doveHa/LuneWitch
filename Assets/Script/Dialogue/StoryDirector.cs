using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class StoryDirector : MonoBehaviour
{
    [Header("Background & CG")]
    public Image backgroundImage;
    public Image backlogImage;
    public Image illustrationImage; // CG용
    public Image overlayImage;      // 팝업 아이템용

    [Header("Characters")]
    public Image leftCharImage;
    public Image rightCharImage;

    [Header("Boss Animation")]
    public GameObject magotasParent;
    public Image magotasImage;
    public Image bossImage;
    public Sprite[] bossDeathSprites;
    public float animationSpeed = 0.5f;

    [Header("Settings")]
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float activeScale = 1.0f;
    public float inactiveScale = 0.85f;

    [Header("UI")]
    public GameObject dialoguePanel;
    public GameObject bubblePanel;
    public TextMeshProUGUI bubbleText;

    public bool isSequencePlaying { get; private set; } = false;
    private Coroutine currentSequence;
    private Coroutine bossAnimCoroutine;

    private DialogueLine currentLine;

    // 초기화
    public void Init()
    {
        if (illustrationImage) illustrationImage.gameObject.SetActive(false);
        if (overlayImage) overlayImage.gameObject.SetActive(false);
        if (leftCharImage) leftCharImage.gameObject.SetActive(false);
        if (rightCharImage) rightCharImage.gameObject.SetActive(false);

        if (magotasParent) magotasParent.SetActive(false);
        isSequencePlaying = false;
        if (currentSequence != null) StopCoroutine(currentSequence);
        if (bossAnimCoroutine != null) StopCoroutine(bossAnimCoroutine);

        if (dialoguePanel) dialoguePanel.SetActive(true);
        if (bubblePanel) bubblePanel.SetActive(false);
    }

    // 연출
    public void UpdateSceneVisuals(DialogueData storyData, DialogueLine line)
    {
        currentLine = line;

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

            if (!isSequencePlaying)
            {
                UpdateCharacterImages(line);
                HighlightSpeaker(line.speakerType);
            }
        }
    }

    // 배경 교체 (인트로용)
    public void SetBackground(Sprite bgSprite)
    {
        if (backgroundImage != null && bgSprite != null)
        {
            backgroundImage.sprite = bgSprite;
        }
        if (backlogImage != null && bgSprite != null)
        {
            backlogImage.sprite = bgSprite;
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
                if (value == "MagotasDie")
                {
                    if (currentSequence != null) StopCoroutine(currentSequence);
                    if (bossAnimCoroutine != null) StopCoroutine(bossAnimCoroutine);
                    StartCoroutine(PlayMagotasSequence());
                }
                else if (value == "BubbleOn")
                {
                    SetBubbleMode(true);
                }
                else if (value == "BubbleOff")
                {
                    SetBubbleMode(false);
                }
                break;
        }
    }

    public void SkipSequence()
    {
        if (!isSequencePlaying) return;

        if (currentSequence != null) StopCoroutine(currentSequence);
        if (bossAnimCoroutine != null) StopCoroutine(bossAnimCoroutine);

        if (magotasParent) magotasParent.SetActive(false);

        if (magotasImage)
        {
            magotasImage.color = new Color(magotasImage.color.r, magotasImage.color.g, magotasImage.color.b, 0f);
            RectTransform rect = magotasImage.GetComponent<RectTransform>();
        }

        isSequencePlaying = false;

        UpdateCharacterImages(currentLine);
        HighlightSpeaker(currentLine.speakerType);
    }

    IEnumerator PlayMagotasSequence()
    {
        isSequencePlaying = true;

        if (leftCharImage) leftCharImage.gameObject.SetActive(false);
        if (rightCharImage) rightCharImage.gameObject.SetActive(false);

        if (magotasParent) magotasParent.SetActive(true);

        // 영주 사망 애니메이션
        if (bossImage != null && bossDeathSprites != null && bossDeathSprites.Length > 0)
        {
            bossAnimCoroutine = StartCoroutine(RunBossSpriteAnimation());
        }

        // Magotas 페이드 아웃
        if (magotasImage)
        {
            RectTransform rect = magotasImage.GetComponent<RectTransform>();
            Vector2 startPos = rect.anchoredPosition;
            Vector2 targetPos = startPos + new Vector2(0, 600f);

            Color startColor = magotasImage.color;
            Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            float duration = 5.0f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;
                rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
                magotasImage.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }

            rect.anchoredPosition = targetPos;
            magotasImage.color = targetColor;
        }

        // 3. 종료 처리
        if (magotasParent) magotasParent.SetActive(false);

        SetBubbleMode(true);

        isSequencePlaying = false;

        UpdateCharacterImages(currentLine);
        //HighlightSpeaker(currentLine.speakerType);
    }

    IEnumerator RunBossSpriteAnimation()
    {
        bossImage.gameObject.SetActive(true);
        foreach (Sprite frame in bossDeathSprites)
        {
            bossImage.sprite = frame;
            yield return new WaitForSeconds(animationSpeed);
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

    public void SetBubbleMode(bool isOn)
    {
        if (isOn)
        {
            // 말풍선 켜고, 기존 창 끄기
            if (dialoguePanel) dialoguePanel.SetActive(false);
            if (bubblePanel) bubblePanel.SetActive(true);
            if (magotasParent) magotasParent.SetActive(true);

            // 말풍선 텍스트 초기화
            if (bubbleText) bubbleText.text = "";
        }
        else
        {
            // 말풍선 끄고, 기존 창 복구
            if (bubblePanel) bubblePanel.SetActive(false);
            if (dialoguePanel) dialoguePanel.SetActive(true);
            if (magotasParent) magotasParent.SetActive(false);
        }
    }

    // 리소스 로드 헬퍼
    Sprite GetSprite(string name) => Resources.Load<Sprite>("Images/" + name);
}