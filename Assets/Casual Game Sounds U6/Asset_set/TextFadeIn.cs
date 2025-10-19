using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeIn : MonoBehaviour
{
    public TextMeshProUGUI tmpText;
    public float fadeDuration = 1.5f;

    void Start()
    {
        // ���� �� �ٷ� ���̵� �� (���ϸ� ���� ȣ���ص� ��)
        StartCoroutine(FadeInText());
    }

    public IEnumerator FadeInText()
    {
        Color originalColor = tmpText.color;
        float alpha = 0f;

        // ������ �� ������ �����ϰ� ����
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);

        while (alpha < 1f)
        {
            alpha += Time.deltaTime / fadeDuration;
            tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // ������ ���̰� �Ǿ��� �� ���� �� 1�� ����
        tmpText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
    }
}
