using UnityEngine;
using UnityEngine.UI;

public class CharacterCooldown : MonoBehaviour
{
    [Header("쿨타임 설정")]
    public float cooldownMax = 60f;

    [Header("UI")]
    public Slider cooldownSlider;

    private float cooldownTimer = 0f;
    private bool isReady = false;

    // Bolt에서 상태 확인할 수 있도록 public 읽기 전용 프로퍼티 제공
    public bool IsSkillReady => isReady;

    void Start()
    {
        if (cooldownSlider == null)
        {
            Debug.LogError("⚠️ Cooldown Slider가 연결되지 않았습니다!");
        }
        else
        {
            cooldownSlider.value = 0f;
        }
    }

    void Update()
    {
        if (!isReady)
        {
            cooldownTimer += Time.deltaTime;
            float ratio = Mathf.Clamp01(cooldownTimer / cooldownMax);

            if (cooldownSlider != null)
                cooldownSlider.value = ratio;

            if (cooldownTimer >= cooldownMax)
            {
                cooldownTimer = cooldownMax;
                isReady = true;
                Debug.Log("✅ 스킬 사용 가능!");
            }
        }
        else
        {
            if (cooldownSlider != null)
                cooldownSlider.value = 1f;
        }
    }

    // Bolt에서 호출 가능하도록 public으로 유지
    public void UseSkill()
    {
        if (isReady)
        {
            Debug.Log("🔥 스킬 발동!");

            cooldownTimer = 0f;
            isReady = false;

            if (cooldownSlider != null)
                cooldownSlider.value = 0f;
        }
        else
        {
            Debug.Log("⏳ 아직 쿨타임 진행 중...");
        }
    }
}
