using UnityEngine;
using TMPro;
using System.Collections;

public class TMPTextFadeInOut : MonoBehaviour
{
    public TMP_Text targetText;
    public float fadeDuration = 1f;
    public float visibleDuration = 2f;

    void Start()
    {
        if (targetText != null)
        {
            Color color = targetText.color;
            color.a = 0f;
            targetText.color = color;

            StartCoroutine(FadeInOut());
        }
    }

    private IEnumerator FadeInOut()
    {
        // 페이드 인
        yield return StartCoroutine(FadeTextToAlpha(1f));

        // 잠깐 보여주기
        yield return new WaitForSeconds(visibleDuration);

        // 페이드 아웃
        yield return StartCoroutine(FadeTextToAlpha(0f));
    }

    private IEnumerator FadeTextToAlpha(float targetAlpha)
    {
        if (targetText == null) yield break;

        Color color = targetText.color;
        float startAlpha = color.a;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            targetText.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        targetText.color = new Color(color.r, color.g, color.b, targetAlpha);
    }
}
