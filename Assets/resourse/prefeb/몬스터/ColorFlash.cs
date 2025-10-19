using System.Collections;
using UnityEngine;

public class ColorFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    // Bolt���� ȣ���� �Լ� (Ŀ���� �̺�Ʈ�� ȣ�� ����)
    public void FlashWhite()
    {
        if (spriteRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashWhiteRoutine());
        }
    }

    private IEnumerator FlashWhiteRoutine()
    {
        // ��� ���������� ����
        spriteRenderer.color = Color.white;

        // 1�� ���
        yield return new WaitForSeconds(1f);

        // ���� ������ ����
        spriteRenderer.color = originalColor;
    }
}
