using System.Collections.Generic;
using Script.Stage;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class SceneLoadManager : ManagerBase<SceneLoadManager>
    {
        public static int SelectedChapterNo;
        public static int SelectedRoundNo;

        protected override void Awake()
        {
            base.Awake();
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(Constant.Scene.MAIN_SCENE);
        }

        public void LoadChapter()
        {
            switch (SelectedChapterNo)
            {
                case 1:
                    SceneManager.LoadScene("Chapter 1 Story");
                    break;
                case 2:
                    SceneManager.LoadScene("Chapter 2 Story");
                    break;
            }
        }
    }
}