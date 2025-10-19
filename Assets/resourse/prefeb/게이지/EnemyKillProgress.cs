using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyProgressTracker : MonoBehaviour
{
    [Header("�� óġ ����")]
    public int maxEnemies = 27; // �� óġ�ؾ� �ϴ� �� ��
    private int currentKillCount = 0; // ���� óġ ��

    [Header("UI ���")]
    public Slider progressBar; // ���� ��
    public TextMeshProUGUI levelText; // TMP �ؽ�Ʈ: ex) "Level 1-3"

    [Header("���� ����")]
    public int stage = 1;
  

    void Start()
    {
        UpdateLevelText();
        UpdateProgressBar();
    }

    // ���� óġ�� �� �� �Լ� ȣ��
    public void OnEnemyKilled()
    {
        currentKillCount++;
        currentKillCount = Mathf.Clamp(currentKillCount, 0, maxEnemies);

        UpdateProgressBar();
    }

    // ���� �� ������Ʈ
    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            float progress = (float)currentKillCount / maxEnemies;
            progressBar.value = progress;
        }
    }

    // �ؽ�Ʈ ������Ʈ
    void UpdateLevelText()
    {
        if (levelText != null)
        {
            levelText.text = $"Round {stage}";
        }
    }
}
