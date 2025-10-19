using UnityEngine;

public class Fixed2 : MonoBehaviour
{
    private void Start()
    {
        SetResolution();
    }

    /// <summary>
    /// 해상도 고정 함수
    /// </summary>
    public void SetResolution()
    {
        int setWidth = 1920; // 화면 너비
        int setHeight = 1080; // 화면 높이

        // 해상도를 설정값에 따라 변경 (전체화면 모드)
        Screen.SetResolution(setWidth, setHeight, true);
    }
}
