using System.Net.Mime;
using UnityEditor;
using UnityEngine.Device;

namespace Script.UI.ButtonOnClick
{
    public class QuitButton : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            //Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}