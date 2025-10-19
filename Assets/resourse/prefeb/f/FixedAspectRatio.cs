using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectRatio16by9 : MonoBehaviour
{
    // 원하는 목표 화면비 (16:9)
    [SerializeField] private float targetAspect = 16f / 9f;

    private Camera cam;
    private int lastScreenWidth;
    private int lastScreenHeight;

    // 전체화면 해상도 강제 적용 옵션 (원하면 활성화)
    [SerializeField] private bool forceResolution = false;
    [SerializeField] private int forcedWidth = 1920;
    [SerializeField] private int forcedHeight = 1080;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.backgroundColor = Color.black; // 배경은 검은색

        if (forceResolution)
        {
            Screen.SetResolution(forcedWidth, forcedHeight, Screen.fullScreenMode == FullScreenMode.FullScreenWindow);
        }

        lastScreenWidth = Screen.width;
        lastScreenHeight = Screen.height;

        UpdateAspect();
    }

    void Update()
    {
        if (Screen.width != lastScreenWidth || Screen.height != lastScreenHeight)
        {
            UpdateAspect();
            lastScreenWidth = Screen.width;
            lastScreenHeight = Screen.height;
        }
    }

    private void UpdateAspect()
    {
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // 위아래에 검은 여백 (letterbox)
            cam.rect = new Rect(0, (1f - scaleHeight) / 2f, 1, scaleHeight);
        }
        else
        {
            // 좌우에 검은 여백 (pillarbox)
            float scaleWidth = 1f / scaleHeight;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1);
        }
    }
}
