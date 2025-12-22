using UnityEngine;
using UnityEngine.UI; // Button 컴포넌트 사용을 위해 필수
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI Components")]
    public GameObject dialoguePanel;
    public Image leftCharImage;
    public Image rightCharImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Buttons")]
    public Button nextButton; // 다음 대사 버튼
    public Button skipButton; // 스킵 버튼

    [Header("Audio")]
    public ToonyVoices toonyVoices;

    [Header("Settings")]
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float activeScale = 1.0f;
    public float inactiveScale = 0.85f;

    [Range(0.01f, 0.2f)]
    public float typingSpeed = 0.15f;

    private DialogueData currentStory;
    private int currentLineIndex = 0;
    private bool isDialogueActive = false;

    // 타이핑 효과
    private bool isTyping = false;
    private string currentFullText = "";
    private Coroutine typingCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        dialoguePanel.SetActive(false);
    }

    private void Start()
    {
        // 버튼에 리스너(기능) 연결
        // 람다식을 사용하여 안전하게 연결하거나 함수 이름을 직접 넣습니다.
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextBtnClicked);

        if (skipButton != null)
            skipButton.onClick.AddListener(OnSkipBtnClicked);
    }

    public void StartDialogue(DialogueData storyData)
    {
        currentStory = storyData;
        currentLineIndex = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        DisplayNextLine();
    }

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

        // 이미지 설정
        if (line.leftSprite != null)
        {
            leftCharImage.sprite = line.leftSprite;
            leftCharImage.gameObject.SetActive(true);
        }
        if (line.rightSprite != null)
        {
            rightCharImage.sprite = line.rightSprite;
            rightCharImage.gameObject.SetActive(true);
        }

        UpdateVisuals(line.speakerType);

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

    void EndDialogue()
    {
        if (toonyVoices != null) toonyVoices.StopSpeech();

        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        Debug.Log("대화 종료");
    }
}