using UnityEngine;
using Unity.VisualScripting; // Bolt 사용 시 필요

public class ZombieHitEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float flashDuration = 0.1f;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // Bolt에서 직접 호출하거나, Custom Event로 호출할 수 있는 메서드
    //[ExposeMethodInEditor] // <- 선택사항 (Bolt에서 더 명확하게 노출됨)
    public void OnHit()
    {
        StopAllCoroutines(); // 중복 연출 방지
        StartCoroutine(FlashWhite());
    }

    System.Collections.IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
