using Script.Stage.ButtonOnClick;
using UnityEngine;
using UnityEngine.UI;

public class EscController : MonoBehaviour
{
    [Header("ExitMenu")]
    public GameObject ExitMenu;
    public Button CancelBtn;

    public GameSpeedButton gameSpeedButton;

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
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        bool isActive = ExitMenu.activeSelf;
        ExitMenu.SetActive(!isActive);

        if (!isActive)
        {
            Time.timeScale = 0f; // 시간 정지
        }
        else // 패널을 끌 때
        {
            if (gameSpeedButton != null)
            {
                gameSpeedButton.ApplyCurrentSpeed();
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    public void CancelOnClicked()
    {
        if (gameSpeedButton != null)
        {
            gameSpeedButton.ApplyCurrentSpeed();
        }
        else
        {
            Time.timeScale = 1f;
        }
        ExitMenu.SetActive(false);
    }
}
