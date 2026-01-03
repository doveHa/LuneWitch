using UnityEngine;

public class EscController : MonoBehaviour
{
    [Header("ExitMenu")]
    public GameObject ExitMenu;

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
            Time.timeScale = 1f; // 시간 정상화
        }
    }
}
