using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUI : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI nameText;
    public Button button;
    public GameObject tooltipPanel;
    public TextMeshProUGUI tooltipText;

    private CharacterData characterData;
    private System.Action<CharacterData> onClickCallback;

    private bool showTooltip = true;

    public void Setup(CharacterData data, System.Action<CharacterData> onClick, bool showTooltip = true)
    {
        characterData = data;
        image.sprite = data.characterImage;
        nameText.text = data.characterName;
        tooltipText.text = data.description;

        onClickCallback = onClick;
        this.showTooltip = showTooltip;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onClickCallback?.Invoke(characterData));

        button.interactable = data.isUnlocked;
        image.color = data.isUnlocked ? Color.white : new Color(1f, 1f, 1f, 0.4f);
    }

    public void OnPointerEnter()
    {
        if (showTooltip)
            tooltipPanel.SetActive(true);
    }

    public void OnPointerExit()
    {
        tooltipPanel.SetActive(false);
    }
}
