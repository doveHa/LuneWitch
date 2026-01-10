using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("Intro Canvas")]
    public GameObject introPanel;
    public TextMeshProUGUI introTitleText;
    public TextMeshProUGUI introDescText;
    public float introDuration = 2.0f; // 인트로 패널 표시 시간
    public float fadeDuration = 1.5f;  // 페이드 아웃 시간

    [Header("Main Dialogue")]
    public GameObject dialogueCanvas;
    public Image backgroundImage;
    public Image leftCharImage;
    public Image rightCharImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Reward Panel")]
    public GameObject rewardPanel;

    [Header("Buttons")]
    public Button nextButton; // 다음 대사 버튼
    public Button skipButton; // 스킵 버튼

    [Header("Settings")]
    public ToonyVoices toonyVoices;
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float activeScale = 1.0f;
    public float inactiveScale = 0.85f;
    [Range(0.01f, 0.5f)]
    public float typingSpeed = 0.08f; // 타이핑 속도 조절 필요


    private DialogueData currentStory;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private string currentFullText = "";
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // 초기화
        if (introPanel) introPanel.SetActive(false);
        if (dialogueCanvas) dialogueCanvas.SetActive(false);
        if (rewardPanel) rewardPanel.SetActive(false);

        if (leftCharImage) leftCharImage.gameObject.SetActive(false);
        if (rightCharImage) rightCharImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextBtnClicked);

        if (skipButton != null)
            skipButton.onClick.AddListener(OnSkipBtnClicked);
    }

    // 외부 호출 시작 함수
    public void PlayStory(DialogueData storyData)
    {
        currentStory = storyData;
        currentLineIndex = 0;
        isDialogueActive = true;

        // 1. 인트로 시작
        StartCoroutine(PlayIntroSequence());
    }

    // 인트로 연출 코루틴
    /*    IEnumerator PlayIntroSequence()
        {
            // UI 초기 설정
            dialogueCanvas.SetActive(false); // 대화창 숨김
            if (rewardPanel) rewardPanel.SetActive(false);

            // 배경 설정
            if (backgroundImage != null && currentStory.backgroundSprite != null)
                backgroundImage.sprite = currentStory.backgroundSprite;

            // 인트로 패널 활성화 및 텍스트 설정
            if (introPanel != null)
            {
                introPanel.SetActive(true);
                CanvasGroup canvasGroup = introPanel.GetComponent<CanvasGroup>();
                if (canvasGroup == null) canvasGroup = introPanel.AddComponent<CanvasGroup>();

                canvasGroup.alpha = 1f; // 완전 불투명
                if (introTitleText) introTitleText.text = currentStory.introTitle;
                if (introDescText) introDescText.text = currentStory.introDescription;

                // 대기
                yield return new WaitForSeconds(introDuration);

                // 페이드 아웃 (검은 화면이 서서히 사라짐)
                float timer = 0f;
                while (timer < fadeDuration)
                {
                    timer += Time.deltaTime;
                    canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                    yield return null;
                }

                introPanel.SetActive(false); // 인트로 끄기
            }

            // 2. 대화창 켜고 대화 시작
            dialogueCanvas.SetActive(true);
            DisplayNextLine();
        }*/

    IEnumerator PlayIntroSequence()
    {
        // 1. 인트로 패널(검은 화면) 세팅
        if (introPanel != null)
        {
            introPanel.SetActive(true);
            CanvasGroup canvasGroup = introPanel.GetComponent<CanvasGroup>();
            if (canvasGroup == null) canvasGroup = introPanel.AddComponent<CanvasGroup>();

            canvasGroup.alpha = 1f; // 완전 불투명 (뒤에 뭐가 있든 가림)
            canvasGroup.blocksRaycasts = true; // 클릭 방지

            if (introTitleText) introTitleText.text = currentStory.introTitle;
            if (introDescText) introDescText.text = currentStory.introDescription;
        }

        // [핵심 변경] 대화창을 '미리' 켭니다.
        // 인트로 패널이 불투명(alpha 1)해서 맨 위에 덮여 있으므로, 
        // 대화창을 지금 켜도 유저 눈에는 안 보입니다. (뒤에 숨어있음)
        dialogueCanvas.SetActive(true);

        // 아직 대사는 출력하지 말고 UI만 준비 (이미지 등)
        // 필요하다면 첫 대사의 배경이나 캐릭터 이미지를 여기서 미리 세팅해도 좋습니다.
        DialogueLine firstLine = currentStory.lines[0];
        if (!currentStory.isIllustrationMode)
        {
            UpdateCharacterImages(firstLine);
            UpdateVisuals(firstLine.speakerType);
        }

        // 2. 인트로 대기 시간 (필요 없으면 주석 처리하거나 0으로 설정)
        // yield return new WaitForSeconds(introDuration); 

        // 3. 페이드 아웃 (검은 화면이 서서히 투명해짐 -> 뒤에 있던 대화창이 자연스럽게 드러남)
        if (introPanel != null)
        {
            CanvasGroup canvasGroup = introPanel.GetComponent<CanvasGroup>();
            float timer = 0f;

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                yield return null;
            }

            // 다 투명해졌으면 인트로 끄기
            introPanel.SetActive(false);
        }

        // 4. 대사 타이핑 시작
        DisplayNextLine();
    }

    /*    public void StartDialogue(DialogueData storyData)
        {
            currentStory = storyData;
            currentLineIndex = 0;
            isDialogueActive = true;
            dialoguePanel.SetActive(true);

            DisplayNextLine();
        }*/

    // Next 버튼이 클릭되었을 때 실행
    public void OnNextBtnClicked()
    {
        if (!isDialogueActive) return;

        if (isTyping)
        {
            CompleteTextImmediately();
        }
        // 2. 타이핑이 끝난 상태라면? -> 다음 대사로 넘어가기
        else
        {
            DisplayNextLine();
        }
    }

    // Skip 버튼이 클릭되었을 때 실행
    public void OnSkipBtnClicked()
    {
        if (!isDialogueActive) return;

        StopAllCoroutines(); // 타이핑 등 모든 코루틴 중지
        if (toonyVoices != null) toonyVoices.StopSpeech();

        isTyping = false;

        // 마지막 대사로 점프
        currentLineIndex = currentStory.lines.Count - 1;
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (currentLineIndex >= currentStory.lines.Count)
        {
            EndDialogue();
            return;
        }

        DialogueLine line = currentStory.lines[currentLineIndex];

        // UI 설정
        nameText.text = line.speakerName;

        // 텍스트는 바로 보여주지 않고 비워둠 (타이핑 코루틴이 채울 예정)
        dialogueText.text = "";
        currentFullText = line.text; // 전체 문장 저장해두기

        // 일러스트 모드 체크 후 배경 업데이트
        if (currentStory.isIllustrationMode)
        {
            // 일러스트 모드면 캐릭터 둘 다 숨김
            if (leftCharImage) leftCharImage.gameObject.SetActive(false);
            if (rightCharImage) rightCharImage.gameObject.SetActive(false);
        }
        else
        {
            // 일반 모드면 캐릭터 이미지 업데이트
            UpdateCharacterImages(line);
            UpdateVisuals(line.speakerType);
        }

        //UpdateVisuals(line.speakerType);

        // 백로그 추가 (이전 대화 내용이 있다면)
        if (BacklogManager.Instance != null)
        {
            BacklogManager.Instance.AddLog(line.speakerName, line.text);
        }

        // --- 소리 재생 시작 ---
        if (toonyVoices != null)
        {
            toonyVoices.StopSpeech(); // 이전 소리 끄고
            toonyVoices.Speak(line.text); // 새 소리 재생
        }

        // --- 타이핑 효과 시작 ---
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(line.text));

        currentLineIndex++;
    }

    void UpdateCharacterImages(DialogueLine line)
    {
        // 왼쪽 이미지 업데이트 (비어있으면 기존 것 유지)
        if (line.leftSprite != null && leftCharImage != null)
        {
            leftCharImage.sprite = line.leftSprite;
            //코드로 비율 유지
            leftCharImage.preserveAspect = true;
            leftCharImage.gameObject.SetActive(true);
        }

        // 오른쪽 이미지 업데이트
        if (line.rightSprite != null && rightCharImage != null)
        {
            rightCharImage.sprite = line.rightSprite;
            //코드로 비율 유지
            rightCharImage.preserveAspect = true;
            rightCharImage.gameObject.SetActive(true);
        }
    }

    void UpdateVisuals(SpeakerType speaker)
    {
        if (leftCharImage.gameObject.activeSelf)
        {
            SetCharacterVisual(leftCharImage, speaker == SpeakerType.Left);
        }
        if (rightCharImage.gameObject.activeSelf)
        {
            SetCharacterVisual(rightCharImage, speaker == SpeakerType.Right);
        }
    }

    void SetCharacterVisual(Image targetImage, bool isActive)
    {
        RectTransform rect = targetImage.GetComponent<RectTransform>();
        if (isActive)
        {
            rect.localScale = Vector3.one * activeScale;
            targetImage.color = Color.white;
            rect.SetAsLastSibling();
        }
        else
        {
            rect.localScale = Vector3.one * inactiveScale;
            targetImage.color = inactiveColor;
        }
    }

    IEnumerator TypeText(string textToType)
    {
        isTyping = true;
        dialogueText.text = ""; // 시작 전 초기화

        foreach (char letter in textToType.ToCharArray())
        {
            dialogueText.text += letter; // 한 글자 추가
            yield return new WaitForSeconds(typingSpeed); // 대기
        }

        isTyping = false;
    }

    void CompleteTextImmediately()
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);

        dialogueText.text = currentFullText; // 완성된 문장 바로 출력
        isTyping = false;

        // 텍스트 완성 시 소리도 멈추기
        if (toonyVoices != null) toonyVoices.StopSpeech();
    }

    void EndDialogue()
    {
        if (toonyVoices != null) toonyVoices.StopSpeech();

        isDialogueActive = false;
        dialogueCanvas.SetActive(false);
        Debug.Log("대화 종료");

        if (currentStory.isChapterEnd)
        {
            // 1장 2장 종료 -> 보상 패널 활성화
            Debug.Log("보상 패널 활성화");
            if (rewardPanel != null) rewardPanel.SetActive(true);
        }
        else
        {
            // 2. 소챕터 종료 -> 전투 씬으로 이동
            Debug.Log($"소챕터 종료: {currentStory.nextSceneName} 씬으로 이동");
            if (!string.IsNullOrEmpty(currentStory.nextSceneName))
            {
                SceneManager.LoadScene(currentStory.nextSceneName);
            }
        }
    }
}