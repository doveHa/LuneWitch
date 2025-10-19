using UnityEngine;
using UnityEngine.UI;

public class FadeInOutSprite : MonoBehaviour
{
    public float fadeDuration = 1.5f;
    public float waitBeforeFadeOut = 1f;

    private SpriteRenderer spriteRenderer;
    private Image image;
    private Color _color;

    void Awake()
    {
        if (TryGetComponent(out SpriteRenderer spriteRenderer))
        {
            this.spriteRenderer = spriteRenderer;
            _color = spriteRenderer.color;
        }
        else if (TryGetComponent(out Image image))
        {
            this.image = image;
            _color = image.color;
        }

        ChangeAlpha(0);
    }

    void Start()
    {
        
        StartCoroutine(FadeSequence());
    }

    System.Collections.IEnumerator FadeSequence()
    {
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(waitBeforeFadeOut);
        yield return StartCoroutine(FadeOut());
    }

    System.Collections.IEnumerator FadeIn()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            ChangeAlpha(Mathf.Lerp(0, 1, time / fadeDuration));
            yield return null;
        }

        ChangeAlpha(1);
    }

    System.Collections.IEnumerator FadeOut()
    {
        float time = 0f;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            ChangeAlpha(Mathf.Lerp(1, 0, time / fadeDuration));
            yield return null;
        }

        ChangeAlpha(0);
    }

    private void ChangeAlpha(float alpha)
    {
        _color.a = alpha;
        
        if (spriteRenderer != null)
        {
            spriteRenderer.color = _color;
        }

        if (image != null)
        {
            image.color = _color;
        }
    }
}