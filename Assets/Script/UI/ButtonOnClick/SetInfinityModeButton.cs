using Script.Manager;
using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class SetInfinityModeButton : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private bool isInfinityMode;
        protected override void OnClick()
        {
            SceneLoadManager.isInfinityMode = isInfinityMode;
        }
    }
}