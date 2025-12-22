using UnityEngine;
using TMPro;

public class LogItem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI contentText;

    public void SetLog(string name, string content)
    {
        nameText.text = name;
        contentText.text = content;
    }
}
