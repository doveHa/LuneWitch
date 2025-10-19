using UnityEngine;

public class InGameCharacterLoader : MonoBehaviour
{
    public GameObject characterObject;
    public GameObject character1Object;

    private string currentCharacter;

    void Start()
    {
        ApplySelection();
    }

    void Update()
    {
        if (CharacterSelectionManager.Instance != null &&
            CharacterSelectionManager.Instance.SelectedCharacter != currentCharacter)
        {
            ApplySelection();
        }
    }

    void ApplySelection()
    {
        string selected = CharacterSelectionManager.Instance != null
            ? CharacterSelectionManager.Instance.SelectedCharacter.Trim()
            : "Lumina";

        characterObject.SetActive(false);
        //character1Object.SetActive(false);

        if (selected == "Lumina")
            characterObject.SetActive(true);
        else if (selected == "Irene")
            character1Object.SetActive(true);

        currentCharacter = selected;
    }
}
