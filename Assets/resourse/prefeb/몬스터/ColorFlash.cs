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

    // Bolt에서 호출할 함수 (커스텀 이벤트로 호출 가능)
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
        // 흰색 불투명으로 변경
        spriteRenderer.color = Color.white;

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 원본 색으로 복원
        spriteRenderer.color = originalColor;
    }
}
