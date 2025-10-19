using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;
    public float fillSpeed = 0.5f; // ���� �ӵ� ����

    private float targetProgress = 0f;

    void Start()
    {
        progressBar.value = 0f;
    }

    void Update()
    {
        // ���� ������ ��ǥ ������ õõ�� �̵���Ű��
        progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, fillSpeed * Time.deltaTime);
    }

    // �ܺο��� ���� ������ ������Ʈ�ϴ� �Լ�
    public void SetProgress(float progress)
    {
        targetProgress = Mathf.Clamp01(progress);
    }
}
