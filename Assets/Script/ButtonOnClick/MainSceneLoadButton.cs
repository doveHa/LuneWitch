using Script.Manager;

namespace Script.ButtonOnClick
{
    public class MainSceneLoadButton : ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadMainScene();
        }
    }
}