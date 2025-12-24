using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class RetryButton : Base.ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadRound();
        }
    }
}