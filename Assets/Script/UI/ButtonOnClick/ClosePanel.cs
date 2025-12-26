using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class ClosePanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject closePanel;
        protected override void OnClick()
        {
            closePanel.SetActive(false);
        }
    }
}