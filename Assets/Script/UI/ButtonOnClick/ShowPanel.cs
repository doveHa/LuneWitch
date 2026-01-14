using UnityEngine;
using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class ShowPanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject showPanel;
        [SerializeField] private bool pauseGame = false;

        protected override void OnClick()
        {
            showPanel.SetActive(true);

            if (pauseGame)
            {
                if (TimeScaleManager.Manager != null)
                    TimeScaleManager.Manager.PauseGame();
            }
        }
    }
}