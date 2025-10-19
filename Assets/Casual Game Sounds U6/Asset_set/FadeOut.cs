using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOut : MonoBehaviour
{
    public Image fadeImage;      // UI Image (검정색 이미지)
    public float fadeDuration = 1.5f;

    private void Start()
    {
        StartCoroutine(FadeOutEffect());
    }

    IEnumerator FadeOutEffect()
    {
        float time = 0f;
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(true);

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
    }
}
