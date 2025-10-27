using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class SceneLoadManager : ManagerBase<SceneLoadManager>
    {
        private const string MAIN_SCENE = "Main";
        public static int SelectedChapterNo;

        protected override void Awake()
        {
            base.Awake();
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(MAIN_SCENE);
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