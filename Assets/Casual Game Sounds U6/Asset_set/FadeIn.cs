using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;         // UI Image (������ �̹���)
    public float fadeDuration = 1.5f;
    public float delayBeforeFade = 1f; // �߰��� ������ �ð� (��)

    private void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    IEnumerator FadeInEffect()
    {
        if (delayBeforeFade > 0f)
            yield return new WaitForSeconds(delayBeforeFade); // ������ �� ����

        float time = 0f;
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(true);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }
}
