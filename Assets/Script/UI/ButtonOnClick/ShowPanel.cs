using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class ShowPanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject showPanel;
        protected override void OnClick()
        {
            showPanel.SetActive(true);
        }
    }
}