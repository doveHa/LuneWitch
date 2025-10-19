using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.Manager
{
    public class SceneLoadManager : ManagerBase<SceneLoadManager>
    {
        private const string MAIN_SCENE = "Main";

        protected override void Awake()
        {
            base.Awake();
        }

        public void LoadMainScene()
        {
            SceneManager.LoadScene(MAIN_SCENE);
        }
    }
}