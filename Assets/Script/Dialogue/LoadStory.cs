using Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStory : MonoBehaviour
{
    //전투 후 스토리 씬 로드
    public void LoadNextAfterBattle()
    {
        Debug.Log(StoryContext.storyAfterBattle);
        Debug.Log(StoryContext.storyToPlay);

        if (StoryContext.storyAfterBattle != null)
        {
            StoryContext.storyToPlay = StoryContext.storyAfterBattle;
            StoryContext.storyAfterBattle = null;

            SceneManager.LoadScene("StoryScene"); // 통합된 스토리 씬 이름
        }
        else
        {
            //SceneManager.LoadScene("Main");
            Debug.Log("storyAfterBattle is null");
        }
    }
}
