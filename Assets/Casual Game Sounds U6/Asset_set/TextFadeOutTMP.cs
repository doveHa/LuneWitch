using UnityEngine;
using TMPro;
using System.Collections;

public class TextFadeOutTMPAuto : MonoBehaviour
{
    public TextMeshProUGUI tmpText;    // 자동으로 페이드 아웃할 TMP 텍스트
    public float fadeDuration = 2f;      // 페이드 아웃에 걸리는 시간 (초)
    public float delayBeforeFade = 0f;   // 페이드 시작 전 딜레이 (초)

    void Start()
    {
        if (tmpText != null)
        {
            // 텍스트를 완전히 보이는 상태로 초기화
            Color color = tmpText.color;
            color.a = 1f;
            tmpText.color = color;

            // 코루틴을 시작하여 자동으로 페이드 아웃
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        // 딜레이가 설정되어 있다면 해당 시간만큼 대기
        if (delayBeforeFade > 0f)
        {
            yield return new WaitForSeconds(delayBeforeFade);
        }

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            // 1에서 0까지 선형 보간
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);

            // TMP 텍스트의 알파 값을 갱신
            Color color = tmpText.color;
            color.a = alpha;
            tmpText.color = color;

            yield return null;
        }
    }
}
