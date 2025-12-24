using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class ShowPanel : Base.ButtonOnClick
    {
        [SerializeField] private GameObject showPanel;
        protected override void OnClick()
        {
            showPanel.SetActive(true);
        }
    }
}