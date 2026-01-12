using UnityEngine;

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
                Time.timeScale = 0f;
            }
        }
    }
}