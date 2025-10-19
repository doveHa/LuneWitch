using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float fadeDuration = 1.5f;

    void Start()
    {
        // 시작 시 바로 페이드 인 (원하면 직접 호출해도 됨)
        StartCoroutine(FadeInText());
    }

    public IEnumerator FadeInText()
    {
        Color originalColor = tmpText.color;
        float alpha = 0f;

        // 시작할 때 완전히 투명하게 설정
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // 완전히 보이게 되었을 때 알파 값 1로 고정
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}
