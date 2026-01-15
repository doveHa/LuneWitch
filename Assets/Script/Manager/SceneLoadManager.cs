using System.Collections.Generic;
using Script.Core.Manager;
using Script.Stage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class SceneLoadManager : ManagerBase<SceneLoadManager>
    {
        public static bool isInfinityMode = false;
        public static int SelectedChapterNo;
        public static int SelectedRoundNo;

        public string NextDialogueDataID { get; private set; }

        protected override void Awake()
        {
            base.Awake();
        }

        public void LoadMainScene()
        {
            Time.timeScale = 1f;

            SceneManager.LoadScene(Constant.Scene.MAIN_SCENE);
        }

        public void LoadChapter()
        {
            string path = $"DialogueData/Story_{SelectedChapterNo}_0";
            // 시작 스토리가 2개 뿐이기 때문에 하드코딩 하였음

            DialogueData loadedData = Resources.Load<DialogueData>(path);

            if(loadedData != null)
            {
                StoryContext.storyToPlay = loadedData;
                SceneManager.LoadScene("StoryScene");
            }
            else
            {
                Debug.LogError($"DialogueData not found at path: {path}");
            }

/*            switch (SelectedChapterNo)
            {
                case 1:
                    SceneManager.LoadScene("Chapter 1 Story");
                    // SceneManager.LoadScene("StoryScene");
                    break;
                case 2:
                    SceneManager.LoadScene("Chapter 2 Story");
                    break;
            }*/
        }

        public void LoadRound()
        {
            SceneManager.LoadScene("BattleScene");
        }

        public void LoadStory(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void LoadStory(string sceneName, string dialogueDataID)
        {
            string path = $"DialogueData/Story_2_{dialogueDataID}";
            DialogueData loadedData = Resources.Load<DialogueData>(path);

            if (loadedData != null)
            {
                StoryContext.storyToPlay = loadedData;
                SceneManager.LoadScene("StoryScene");
            }
            else
            {
                Debug.LogError($"DialogueData not found at path: {path}");
            }
        }
    }
}