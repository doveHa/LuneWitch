using UnityEngine;

namespace Script.ButtonOnClick
{
    public class ShowPanel : ButtonOnClick
    {
        [SerializeField] private GameObject showPanel;
        protected override void OnClick()
        {
            showPanel.SetActive(true);
        }
    }
}