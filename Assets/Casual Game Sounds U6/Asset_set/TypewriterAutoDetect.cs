using System.Collections;
using UnityEngine;
using TMPro; // TextMeshPro용

public class TypewriterAutoDetect : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private string lastText = "";
    private bool isTyping = false;

    public TextMeshProUGUI textUI;

    private void Update()
    {
        if (!isTyping && textUI.text != lastText)
        {
            StopAllCoroutines(); // 혹시 타이핑 중이면 멈추고
            StartCoroutine(TypeText(textUI.text));
        }
    }

    IEnumerator TypeText(string targetText)
    {
        isTyping = true;
        lastText = targetText;
        textUI.text = "";

        foreach (char c in targetText)
        {
            textUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }
}