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
        DialogueManager.Instance.StartDialogue(storyToPlay);
    }
}
