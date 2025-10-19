using System.Collections;
using TMPro;
using UnityEngine;
using Unity.VisualScripting;


public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI textUI;
    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private bool isTyping = false;

    void Update()
    {
        // 스페이스바 입력 체크
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandleInput();
        }
    }

    // Bolt에서도 버튼 OnClick에 이 메서드 연결
    public void OnClickButton()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (isTyping)
        {
            // 타이핑 중 → 즉시 글 다 보이게
            ShowAllTextImmediately();
        }
        else
        {
            // 이미 다 나옴 → Bolt에 이벤트 전달
            BoltEventTrigger();
        }
    }

    public void StartTypingFromBolt(string dialogue)
    {
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText(dialogue));
    }

    private IEnumerator TypeText(string fullText)
    {
        isTyping = true;
        textUI.text = fullText;
        textUI.maxVisibleCharacters = 0;

        int totalCharacters = fullText.Length;
        int visibleCount = 0;

        while (visibleCount < totalCharacters)
        {
            visibleCount++;
            textUI.maxVisibleCharacters = visibleCount;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false; // 다 끝났음
    }

    public void ShowAllTextImmediately()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        textUI.maxVisibleCharacters = textUI.text.Length;
        isTyping = false;
    }

    private void BoltEventTrigger()
    {
        // Bolt로 "NextDialogue" 커스텀 이벤트 전달
        CustomEvent.Trigger(gameObject, "NextDialogue");

    }
}
