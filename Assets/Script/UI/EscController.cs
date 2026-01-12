using Script.Stage.ButtonOnClick;
using UnityEngine;
using UnityEngine.UI;

public class EscController : MonoBehaviour
{
    [Header("ExitMenu")]
    public GameObject ExitMenu;
    public GameObject OptionMenu;
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
                if (gameSpeedButton != null)
                {
                    Debug.Log($"[EscController] 배속 버튼이 연결되어 있음! ({gameSpeedButton.name}) -> 배속 복구 시도");
                    gameSpeedButton.ApplyCurrentSpeed();
                }
                else
                {
                    Debug.Log("[EscController] 배속 버튼이 연결 안 됨 (null) -> 1배속으로 강제 초기화");
                    Time.timeScale = 1f;
                }
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
            if (gameSpeedButton != null)
            {
                Debug.Log($"CancelBtn: [EscController] 배속 버튼이 연결되어 있음! ({gameSpeedButton.name}) -> 배속 복구 시도");
                gameSpeedButton.ApplyCurrentSpeed();
            }
            else
            {
                Debug.Log("CanselBtn: [EscController] 배속 버튼이 연결 안 됨 (null) -> 1배속으로 강제 초기화");
                Time.timeScale = 1f;
            }
        }
        ExitMenu.SetActive(false);
    }
}
