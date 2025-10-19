using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class WaveProgress : MonoBehaviour
{
    public Slider progressBar;

    public float preDelay = 6f;            // 적 등장 전 대기 시간
    public float autoFillSpeed = 5f;       // 초당 퍼센트 증가량

    private float progress = 0f;           // 0 ~ 100 (퍼센트 단위)
    private int killCount = 0;

    private float timer = 0f;
    private bool autoFillStarted = false;
    private bool waveStarted = false;

    void Start()
    {
        progressBar.value = 0f;
    }

    void Update()
    {
        if (waveStarted) return;

        if (!autoFillStarted)
        {
            timer += Time.deltaTime;
            if (timer >= preDelay)
            {
                autoFillStarted = true;
            }
            return;
        }

        // 진행이 100에 도달했으면 자동 증가 중단
        if (progress >= 100f) return;

        // 킬 수에 따른 최대 진행 가능치 설정
        float maxAllowedProgress;

        if (killCount >= 10)
            maxAllowedProgress = 100f;
        else if (killCount >= 5)
            maxAllowedProgress = 90f;
        else
            maxAllowedProgress = 50f;

        // 자동 진행바 증가
        progress += autoFillSpeed * Time.deltaTime;
        progress = Mathf.Min(progress, maxAllowedProgress);

        progressBar.value = progress / 100f;

        if (progress >= 100f)
        {
            StartNextWave();
        }
    }

    // ✅ 내부 로직: 좀비가 죽었을 때 호출되는 함수
    public void OnZombieKilled()
    {
        if (waveStarted) return;

        killCount++;

        Debug.Log($"적 처치 수: {killCount}마리, 최대 게이지 제한: {(killCount >= 10 ? 100 : killCount >= 5 ? 90 : 50)}%");

        // 10마리 다 잡았으면 즉시 100% 채우기
        if (killCount >= 10)
        {
            progress = 100f;
            progressBar.value = 1f;
            StartNextWave();
            return;
        }

        // 일반 진행 상황에서도 100% 도달 여부 확인
        if (progress >= 100f)
        {
            StartNextWave();
        }
    }

    // ✅ Bolt에서 호출할 수 있도록 만든 함수
    public void ZombieKilled()
    {
        OnZombieKilled();
    }

    void StartNextWave()
    {
        waveStarted = true;
        Debug.Log("웨이브 시작!");
        CustomEvent.Trigger(gameObject, "OnWaveStart");
    }

    public void ResetWave()
    {
        waveStarted = false;
        killCount = 0;
        timer = 0f;
        autoFillStarted = false;

        // ✅ 진행도는 유지하므로 초기화 제거
        // progress = 0f;
        // progressBar.value = 0f;
    }

    // ✅ Bolt에서 직접 다음 웨이브 시작도 가능하도록 한 함수
    public void GoToNextWave()
    {
        StartNextWave();
    }
}
