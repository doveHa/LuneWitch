using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class TickDamageEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        // SpriteRenderer가 비어 있으면 자동으로 할당
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    // 외부에서 이 메서드를 호출하면 틱 데미지 적용 시작
    public void ApplyTickDamage()
    {
        StartCoroutine(TickDamageCoroutine());
    }

    private IEnumerator TickDamageCoroutine()
    {
        int tickCount = 3;
        float tickInterval = 1.0f;
        int damagePerTick = -5;

        for (int i = 0; i < tickCount; i++)
        {
            // Bolt Custom Event를 호출해서 Bolt 그래프 흐름 실행
            CustomEvent.Trigger(gameObject, "ApplyTickDamageFlow", damagePerTick);

            // 빨간색으로 피격 연출 후 원래 색상으로 복귀
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;

            // 다음 틱까지 대기
            yield return new WaitForSeconds(tickInterval);
        }
    }
}