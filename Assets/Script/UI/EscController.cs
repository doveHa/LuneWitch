using Script.Manager;
using Script.Stage.ButtonOnClick;
using UnityEngine;
using UnityEngine.UI;

public class EscController : MonoBehaviour
{
    [Header("ExitMenu")]
    public GameObject ExitMenu;
    public GameObject OptionMenu;
    public Button CancelBtn;

    //public GameSpeedButton gameSpeedButton;

    private void Start()
    {
        if(CancelBtn != null)
        {
           CancelBtn.onClick.AddListener(CancelOnClicked);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 만약 옵션 메뉴가 열려있다면 닫기
            if (OptionMenu != null && OptionMenu.activeSelf)
            {
                OptionMenu.SetActive(false);
                ExitMenu.SetActive(true);
                return;
            }

            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        bool isActive = ExitMenu.activeSelf;
        ExitMenu.SetActive(!isActive);

        if (!isActive)
        {
            if (TimeScaleManager.Manager != null)
                TimeScaleManager.Manager.PauseGame();
        }
        else
        {
            if (TimeScaleManager.Manager != null)
                TimeScaleManager.Manager.ResumeGame();
        }
    }

    public void CancelOnClicked()
    {
        if (TimeScaleManager.Manager != null)
            TimeScaleManager.Manager.ResumeGame();

        ExitMenu.SetActive(false);
    }
}
