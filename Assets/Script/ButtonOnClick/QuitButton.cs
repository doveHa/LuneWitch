using System.Net.Mime;
using UnityEngine.Device;

namespace Script.ButtonOnClick
{
    public class QuitButton : ButtonOnClick
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}