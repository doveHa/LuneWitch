using Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStory : MonoBehaviour
{
    //���� �� ���丮 �� �ε�
    public void LoadNextAfterBattle()
    {
        Debug.Log(StoryContext.storyAfterBattle);
        Debug.Log(StoryContext.storyToPlay);

        
    }
}
