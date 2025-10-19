using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class TickDamageEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        // SpriteRenderer�� ��� ������ �ڵ����� �Ҵ�
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;
    }

    // �ܺο��� �� �޼��带 ȣ���ϸ� ƽ ������ ���� ����
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
            // Bolt Custom Event�� ȣ���ؼ� Bolt �׷��� �帧 ����
            CustomEvent.Trigger(gameObject, "ApplyTickDamageFlow", damagePerTick);

            // ���������� �ǰ� ���� �� ���� �������� ����
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = originalColor;

            // ���� ƽ���� ���
            yield return new WaitForSeconds(tickInterval);
        }
    }
}