using NUnit.Framework;
using Script.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public struct CharacterVoiceProfile
{
    public string characterName;
    [UnityEngine.Range(0.5f, 8.0f)]
    public float pitch;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public StoryDirector director;

    [Header("Intro Canvas")]
    public GameObject introPanel;
    public TextMeshProUGUI introTitleText;
    public TextMeshProUGUI introDescText;

    public float introDuration = 2.0f; // 인트로 패널 표시 시간
    public float fadeDuration = 1.5f;  // 페이드 아웃 시간

    [Header("Main Dialogue")]
    public GameObject dialogueCanvas;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    [Header("Reward Panel")]
    public GameObject rewardPanel;
    public TextMeshProUGUI rewardText;

    [Header("Buttons")]
    public Button nextButton; // 다음 대사 버튼
    public Button nextButton2;
    public Button skipButton; // 스킵 버튼
    
    [Header("Settings")]
    public ToonyVoices toonyVoices;
    public List<CharacterVoiceProfile> characterVoices;
    public float defaultPitch = 4.0f;
    public Color inactiveColor = new Color(0.5f, 0.5f, 0.5f, 1f);
    public float activeScale = 1.0f;
    public float inactiveScale = 0.85f;
    [UnityEngine.Range(0.01f, 0.5f)]
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

        if(director) director.Init();
    }

    private void Start()
    {
        if (nextButton != null)
            nextButton.onClick.AddListener(OnNextBtnClicked);

        if(nextButton2 != null)
            nextButton2.onClick.AddListener(OnNextBtnClicked);

        if (skipButton != null)
            skipButton.onClick.AddListener(OnSkipBtnClicked);

        if(StoryContext.storyToPlay != null)
        {
            PlayStory(StoryContext.storyToPlay);
            StoryContext.storyToPlay = null; // 재사용 방지
        }
        else
        {
            Debug.Log("다이얼로그 데이터 없음");
        }
    }

    // 외부 호출 시작 함수
    public void PlayStory(DialogueData storyData)
    {
        // 기존 코루틴 중지
        StopAllCoroutines();
        if (director) director.Init();
        if (dialogueCanvas) dialogueCanvas.SetActive(false);

        currentStory = storyData;
        currentLineIndex = 0;
        isDialogueActive = true;

        // 1. 인트로 시작
        StartCoroutine(PlayIntroSequence());
    }

    IEnumerator PlayIntroSequence()
    {
        // 1. 인트로 패널 켜기 (UI 제어는 매니저가 함)
        if (introPanel)
        {
            introPanel.SetActive(true);
            CanvasGroup cg = introPanel.GetComponent<CanvasGroup>();
            if (!cg) cg = introPanel.AddComponent<CanvasGroup>();
            cg.alpha = 1f;
            cg.blocksRaycasts = true;

            if (introTitleText) introTitleText.text = currentStory.introTitle;
            if (introDescText) introDescText.text = currentStory.introDescription;
        }

        // 2. 대화창 켜기
        //dialogueCanvas.SetActive(true);

        if (director) director.SetBackground(currentStory.backgroundSprite);

        if (currentStory.lines.Count > 0 && director)
        {
            director.UpdateSceneVisuals(currentStory, currentStory.lines[0]);
        }

        yield return null;
        // 3. 페이드 아웃
        yield return new WaitForSeconds(0.5f); // 살짝 대기 (배경 전환 시간 벌기)
        
        if (dialogueCanvas) dialogueCanvas.SetActive(true);

        if (introPanel)
        {
            CanvasGroup cg = introPanel.GetComponent<CanvasGroup>();
            float timer = 0f;
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                cg.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
                yield return null;
            }
            introPanel.SetActive(false);
        }

        // 4. 대사 시작
        DisplayNextLine();
    }

    // Next 버튼이 클릭되었을 때 실행
    public void OnNextBtnClicked()
    {
        if (!isDialogueActive) return;

        if (director != null && director.isSequencePlaying)
        {
            director.SkipSequence();
            return; // 텍스트는 넘어가지 않음
        }

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

        if (director != null && director.isSequencePlaying)
        {
            director.SkipSequence();
        }

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

        dialogueText.text = "";
        currentFullText = line.text;

        // 화면 연출
        if (director)
        {
            director.UpdateSceneVisuals(currentStory, line);
        }

        //UpdateVisuals(line.speakerType);

        // 백로그 추가
        if (BacklogManager.Instance != null)
        {
            BacklogManager.Instance.AddLog(line.speakerName, line.text);
        }

        // 소리 재생
        if (toonyVoices != null)
        {
            toonyVoices.StopSpeech(); // 이전 소리 끄고

            // 캐릭터에 맞는 피치 설정
            float pitchToUse = GetPitchByName(line.speakerName);
            toonyVoices.Speak(line.text, pitchToUse);
        }

        // --- 타이핑 효과 시작 ---
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(line.text));

        currentLineIndex++;
    }

    private float GetPitchByName(string name)
    {
        // 등록된 리스트를 뒤져서 이름이 같은 게 있는지 확인
        foreach (var profile in characterVoices)
        {
            if (profile.characterName == name)
            {
                return profile.pitch;
            }
        }
        // 없으면 기본값 반환
        return defaultPitch;
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
        //dialogueCanvas.SetActive(false);
        Debug.Log("대화 종료");

        // 다음 소챕터가 있을 경우 진행
        if (currentStory.nextStory != null)
        {
            Debug.Log($"다음 소챕터({currentStory.nextStory.name}) 진행");
            PlayStory(currentStory.nextStory);
            return;
        }

        if (currentStory.isChapterEnd)
        {
            // 1장 2장 종료 -> 보상 패널 활성화
            Debug.Log("보상 패널 활성화");

            if (rewardText != null && !string.IsNullOrEmpty(currentStory.chapterClearMessage))
            {
                rewardText.text = currentStory.chapterClearMessage;
            }

            if (rewardPanel != null)
                rewardPanel.SetActive(true);
        }
        else
        {
            // 2. 소챕터 종료 -> 전투 씬으로 이동
            Debug.Log($"소챕터 종료: {currentStory.nextSceneName} 씬으로 이동");
            if (!string.IsNullOrEmpty(currentStory.nextSceneName))
            {
                if (currentStory.nextBattleRoundNo > 0)
                {
                    Debug.Log($"라운드 정보 갱신: {SceneLoadManager.SelectedRoundNo} -> {currentStory.nextBattleRoundNo}");
                    SceneLoadManager.SelectedRoundNo = currentStory.nextBattleRoundNo;
                }

                if (currentStory.nextStoryAfterBattle != null)
                {
                    StoryContext.storyAfterBattle = currentStory.nextStoryAfterBattle;
                }

                SceneManager.LoadScene(currentStory.nextSceneName);
            }
        }
    }
}