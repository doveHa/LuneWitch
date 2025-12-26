using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class MainSceneLoadButton : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadMainScene();
        }
    }
}