using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyProgressTracker : MonoBehaviour
{
    [Header("적 처치 관련")]
    public int maxEnemies = 27; // 총 처치해야 하는 적 수
    private int currentKillCount = 0; // 현재 처치 수

    [Header("UI 요소")]
    public Slider progressBar; // 진행 바
    public TextMeshProUGUI levelText; // TMP 텍스트: ex) "Level 1-3"

    [Header("레벨 정보")]
    public int stage = 1;
  

    void Start()
    {
        UpdateLevelText();
        UpdateProgressBar();
    }

    // 적이 처치될 때 이 함수 호출
    public void OnEnemyKilled()
    {
        currentKillCount++;
        currentKillCount = Mathf.Clamp(currentKillCount, 0, maxEnemies);

        UpdateProgressBar();
    }

    // 진행 바 업데이트
    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            float progress = (float)currentKillCount / maxEnemies;
            progressBar.value = progress;
        }
    }

    // 텍스트 업데이트
    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"Round {stage}";
        }
    }
}
