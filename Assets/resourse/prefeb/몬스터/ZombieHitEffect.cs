using UnityEngine;
using Unity.VisualScripting; // Bolt ��� �� �ʿ�

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

    // Bolt���� ���� ȣ���ϰų�, Custom Event�� ȣ���� �� �ִ� �޼���
    //[ExposeMethodInEditor] // <- ���û��� (Bolt���� �� ��Ȯ�ϰ� �����)
    public void OnHit()
    {
        StopAllCoroutines(); // �ߺ� ���� ����
        StartCoroutine(FlashWhite());
    }

    System.Collections.IEnumerator FlashWhite()
    {
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }
}
