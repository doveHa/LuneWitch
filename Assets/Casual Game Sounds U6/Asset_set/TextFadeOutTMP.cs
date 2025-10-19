using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeOutTMPAuto : MonoBehaviour
{
    public TextMeshProUGUI tmpText;    // �ڵ����� ���̵� �ƿ��� TMP �ؽ�Ʈ
    public float fadeDuration = 2f;      // ���̵� �ƿ��� �ɸ��� �ð� (��)
    public float delayBeforeFade = 0f;   // ���̵� ���� �� ������ (��)

    void Start()
    {
        if (tmpText != null)
        {
            // �ؽ�Ʈ�� ������ ���̴� ���·� �ʱ�ȭ
            Color color = tmpText.color;
            color.a = 1f;
            tmpText.color = color;

            // �ڷ�ƾ�� �����Ͽ� �ڵ����� ���̵� �ƿ�
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        // �����̰� �����Ǿ� �ִٸ� �ش� �ð���ŭ ���
        if (delayBeforeFade > 0f)
        {
            yield return new WaitForSeconds(delayBeforeFade);
        }

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // 1���� 0���� ���� ����
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            // TMP �ؽ�Ʈ�� ���� ���� ����
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;

            yield return null;
        }
    }
}
