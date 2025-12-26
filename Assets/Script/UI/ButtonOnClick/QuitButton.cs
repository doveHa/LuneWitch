using System.Net.Mime;
using UnityEngine.Device;

namespace Script.UI.ButtonOnClick
{
    public class QuitButton : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}