using UnityEngine;

public class FadeInSprite : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float fadeDuration = 1f;

    private float fadeTimer = 0f;
    private Color originalColor;
    private bool isFading = false;

    void Awake()
    {
        if (sprite == null) sprite = GetComponent<SpriteRenderer>();
        originalColor = sprite.color;
    }

    void OnEnable()
    {
        // ������Ʈ ���� �� �����ϰ� ����� ���̵� �� ����
        Color transparent = originalColor;
        transparent.a = 0f;
        sprite.color = transparent;

        fadeTimer = 0f;
        isFading = true;
    }

    void Update()
    {
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, fadeTimer / fadeDuration);
            Color c = originalColor;
            c.a = alpha;
            sprite.color = c;

            if (fadeTimer >= fadeDuration)
            {
                isFading = false;
            }
        }
    }
}
