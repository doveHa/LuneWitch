using NUnit.Framework;
using Script.Manager;
using Script.Stage.ButtonOnClick;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EscController : MonoBehaviour
{
    [Header("ExitMenu")]
    public GameObject ExitMenu;
    public GameObject OptionMenu;
    public Button CancelBtn;
    public Button ExitBtn;
    public TextMeshProUGUI ExitText;

    public List<string> activeScenes = new List<string>()
    {
        "BattleScene",
        "StoryScene"
    };

    //public GameSpeedButton gameSpeedButton;


    private void Start()
    {
        if(CancelBtn != null)
        {
           CancelBtn.onClick.AddListener(CancelOnClicked);
        }

        CheckButtonState(SceneManager.GetActiveScene().name);
        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
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

    private void CheckButtonState(string currentScene)
    {
        if (ExitBtn != null && ExitText != null)
        {
            if (activeScenes.Contains(currentScene))
            {
                ExitBtn.onClick.AddListener(OnClick_LoadMainScene);
                ExitText.text = "메인으로 돌아가시겠습니까?";
                Debug.Log("LoadMainScene");
            }
            else
            {
                ExitBtn.onClick.AddListener(OnClick_ExitGame);
                ExitText.text = "게임을 종료하시겠습니까?";
            }
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

    // 버튼
    public void OnClick_ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void OnClick_LoadMainScene()
    {
        if (SceneLoadManager.Manager != null)
        {
            SceneLoadManager.Manager.LoadMainScene();
        }
    }
}
