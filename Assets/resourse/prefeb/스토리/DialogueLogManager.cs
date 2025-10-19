using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogManager : MonoBehaviour
{
    public GameObject logPanel;                  // 대화 로그 전체 패널
    public Transform contentParent;              // 로그 항목들이 들어갈 Content
    public GameObject logItemPrefab;             // 로그 항목 프리팹
    public ScrollRect scrollRect;                // 스크롤뷰 컴포넌트

    public int maxLogCount = 35;                 // 최대 로그 수 제한

    private Queue<GameObject> logItems = new Queue<GameObject>(); // 로그 아이템 저장

    private string currentSpeakerName;
    private string currentDialogue;

    // 외부에서 이름 설정
    public void SetSpeakerName(string name)
    {
        currentSpeakerName = name;
    }

    // 외부에서 대사 텍스트 설정
    public void SetDialogue(string dialogue)
    {
        currentDialogue = dialogue;
    }

    // 대사 실제로 적용 (Bolt에서 호출)
    public void ApplyDialogue()
    {
        if (string.IsNullOrEmpty(currentDialogue))
        {
            Debug.LogWarning("대사가 비어있습니다.");
            return;
        }

        GameObject newItem = Instantiate(logItemPrefab, contentParent);
        DialogueLogItem logItem = newItem.GetComponent<DialogueLogItem>();
        logItem.SetData(currentSpeakerName, currentDialogue);

        logItems.Enqueue(newItem);

        // 최대 수 초과 시 오래된 로그 제거
        if (logItems.Count > maxLogCount)
        {
            GameObject oldItem = logItems.Dequeue();
            Destroy(oldItem);
        }

        StartCoroutine(ScrollToBottom());

        // 상태 초기화
        currentSpeakerName = null;
        currentDialogue = null;
    }

    // 스크롤을 가장 아래로 내림
    private IEnumerator ScrollToBottom()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();

        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
        else
        {
            Debug.LogWarning("ScrollRect가 연결되지 않았습니다.");
        }
    }

    // 대화 로그창 열고 닫기
    public void ToggleLog()
    {
        bool isActive = !logPanel.activeSelf;
        logPanel.SetActive(isActive);

        if (isActive)
        {
            StartCoroutine(ScrollToBottom());
        }
    }

    // 로그 초기화 (씬 전환 등에서 호출 가능)
    public void ClearLog()
    {
        foreach (var item in logItems)
        {
            Destroy(item);
        }
        logItems.Clear();
    }
}
