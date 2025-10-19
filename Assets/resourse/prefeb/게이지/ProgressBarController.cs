using UnityEngine;
using UnityEngine.UI;

public class ProgressBarController : MonoBehaviour
{
    public Slider progressBar;
    public float fillSpeed = 0.5f; // 진행 속도 조절

    private float targetProgress = 0f;

    void Start()
    {
        progressBar.value = 0f;
    }

    void Update()
    {
        // 현재 값에서 목표 값까지 천천히 이동시키기
        progressBar.value = Mathf.MoveTowards(progressBar.value, targetProgress, fillSpeed * Time.deltaTime);
    }

    // 외부에서 진행 정도를 업데이트하는 함수
    public void SetProgress(float progress)
    {
        targetProgress = Mathf.Clamp01(progress);
    }
}
