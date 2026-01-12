using Script.Stage.ButtonOnClick;
using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class ClosePanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject closePanel;
        [SerializeField] private GameSpeedButton gameSpeedButton;

        protected override void OnClick()
        {
            closePanel.SetActive(false);

            if (gameSpeedButton != null)
            {
                gameSpeedButton.ApplyCurrentSpeed();
            }
            else
            {
                // 연결 안 되어 있으면(메인 메뉴 등) 그냥 1배속
                Time.timeScale = 1f;
            }
        }
    }
}