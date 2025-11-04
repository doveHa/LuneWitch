using UnityEngine;

namespace Script.ButtonOnClick
{
    public class ClosePanel : ButtonOnClick
    {
        [SerializeField] private GameObject closePanel;
        protected override void OnClick()
        {
            closePanel.SetActive(false);
        }
    }
}