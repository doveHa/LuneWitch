using Script.Manager;
using UnityEngine.UI;

namespace Script.ButtonOnClick
{
    public class RetryButton : ButtonOnClick
    {
        protected override void OnClick()
        {
            SceneLoadManager.Manager.LoadRound();
        }
    }
}