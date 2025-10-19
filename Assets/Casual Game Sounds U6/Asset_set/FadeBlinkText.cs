using UnityEngine;
using TMPro;

public class FadeBlinkText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float speed = 2f; // �����̴� �ӵ� (�������� ����)

    private Color originalColor;

    void Start()
    {
        if (text == null) text = GetComponent<TextMeshProUGUI>();
        originalColor = text.color;
    }

    void Update()
    {
        float alpha = (Mathf.Sin(Time.time * speed) + 1f) / 2f; // 0~1 �ݺ�
        Color c = originalColor;
        c.a = alpha;
        text.color = c;
    }
}
