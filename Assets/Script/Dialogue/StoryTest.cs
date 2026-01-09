using UnityEngine;

public class StoryTest : MonoBehaviour
{
    public DialogueData storyToPlay; // 에디터에서 Story 데이터 할당

    private void Start()
    {
        // 자동으로 스토리 시작 (테스트용)
        TriggerStory();
    }

    // 예: 버튼 클릭 시 혹은 게임 시작 시
    public void TriggerStory()
    {
        if (storyToPlay == null)
        {
            Debug.LogError("테스트할 Dialogue Data가 연결되지 않았습니다! 인스펙터를 확인해주세요.");
            return;
        }

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("씬에 DialogueManager가 없습니다! UI 프리팹을 씬에 배치했는지 확인해주세요.");
            return;
        }

        Debug.Log($"[Test] 스토리 '{storyToPlay.name}' 재생을 시작합니다.");
        DialogueManager.Instance.PlayStory(storyToPlay);
    }
}
