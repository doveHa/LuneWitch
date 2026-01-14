using Script.Stage.ButtonOnClick;
using UnityEngine;
using Script.Manager;

namespace Script.UI.ButtonOnClick
{
    public class ClosePanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject closePanel;

        protected override void OnClick()
        {
            closePanel.SetActive(false);

            if (TimeScaleManager.Manager != null)
            {
                TimeScaleManager.Manager.ResumeGame();
            }
        }
    }
}