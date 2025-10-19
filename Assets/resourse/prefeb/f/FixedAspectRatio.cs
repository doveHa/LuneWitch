using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FixedAspectRatio16by9 : MonoBehaviour
{
    // ���ϴ� ��ǥ ȭ��� (16:9)
    [SerializeField] private float targetAspect = 16f / 9f;

    private Camera cam;
    private int lastScreenWidth;
    private int lastScreenHeight;

    // ��üȭ�� �ػ� ���� ���� �ɼ� (���ϸ� Ȱ��ȭ)
    [SerializeField] private bool forceResolution = false;
    [SerializeField] private int forcedWidth = 1920;
    [SerializeField] private int forcedHeight = 1080;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.backgroundColor = Color.black; // ����� ������

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
            // ���Ʒ��� ���� ���� (letterbox)
            cam.rect = new Rect(0, (1f - scaleHeight) / 2f, 1, scaleHeight);
        }
        else
        {
            // �¿쿡 ���� ���� (pillarbox)
            float scaleWidth = 1f / scaleHeight;
            cam.rect = new Rect((1f - scaleWidth) / 2f, 0, scaleWidth, 1);
        }
    }
}
