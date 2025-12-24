using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class MainSceneLoadButton : Base.ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadMainScene();
        }
    }
}