using UnityEngine;
using TMPro;

public class DialogueLogItem : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;

    public void SetData(string speakerName, string dialogue)
    {
        speakerNameText.text = speakerName;
        dialogueText.text = dialogue;
    }
}
