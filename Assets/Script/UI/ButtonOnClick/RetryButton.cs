using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class RetryButton : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadRound();
        }
    }
}