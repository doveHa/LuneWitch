using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultSlot : MonoBehaviour
{
    public Image characterImage;
    public TMP_Text characterNameText;

    public void SetData(CharacterData data)
    {
        characterImage.sprite = data.characterImage;
        characterNameText.text = data.characterName;
    }

    public void Clear()
    {
        characterImage.sprite = null;
        characterNameText.text = "";
    }
}
