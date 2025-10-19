using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    public Image fadeImage;         // UI Image (검정색 이미지)
    public float fadeDuration = 1.5f;
    public float delayBeforeFade = 1f; // 추가된 딜레이 시간 (초)

    private void Start()
    {
        StartCoroutine(FadeInEffect());
    }

    IEnumerator FadeInEffect()
    {
        if (delayBeforeFade > 0f)
            yield return new WaitForSeconds(delayBeforeFade); // 딜레이 후 시작

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
