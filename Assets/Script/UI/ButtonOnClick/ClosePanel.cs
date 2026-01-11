using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class ClosePanel : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject closePanel;
        protected override void OnClick()
        {
            closePanel.SetActive(false);
            Time.timeScale = 1f; // 시간 정상화 -> 배속 수정 필요
        }
    }
}