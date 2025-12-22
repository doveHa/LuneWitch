using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BacklogManager : MonoBehaviour
{
    public static BacklogManager Instance;

    [Header("UI Components")]
    public GameObject backlogPanel; // 백로그 전체 패널
    public Transform logContent;    // Scroll View의 Content 오브젝트 (여기에 자식으로 쌓임)
    public GameObject logItemPrefab; // 위에서 만든 프리팹

    [Header("Buttons")]
    public Button openButton;  // 게임 화면의 'Log' 버튼
    public Button closeButton; // 백로그 창의 'X' 버튼

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // 시작 시 백로그 꺼두기
        backlogPanel.SetActive(false);
    }

    private void Start()
    {
        if (openButton != null) openButton.onClick.AddListener(OpenBacklog);
        if (closeButton != null) closeButton.onClick.AddListener(CloseBacklog);
    }

    // 대화 추가 함수 (DialogueManager에서 호출)
    public void AddLog(string name, string text)
    {
        // 프리팹 생성
        GameObject newLog = Instantiate(logItemPrefab, logContent);

        // 텍스트 설정
        LogItem itemScript = newLog.GetComponent<LogItem>();
        if (itemScript != null)
        {
            itemScript.SetLog(name, text);
        }

        // (선택사항) 스크롤을 맨 아래로 내리기 위한 로직이 필요할 수 있음
    }

    public void OpenBacklog()
    {
        backlogPanel.SetActive(true);
    }

    public void CloseBacklog()
    {
        backlogPanel.SetActive(false);
    }
}