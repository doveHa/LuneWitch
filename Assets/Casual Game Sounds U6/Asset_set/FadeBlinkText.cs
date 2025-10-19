using UnityEngine;
using TMPro;

public class FadeBlinkText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 2f; // 깜빡이는 속도 (높을수록 빠름)

    private Color originalColor;

    void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // 0~1 반복
        Color c = originalColor;
        c.a = alpha;
        text.color = c;
    }
}
