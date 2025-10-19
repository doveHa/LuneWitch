using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public static CharacterSelectionManager Instance;
    public string SelectedCharacter;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectLumina()
    {
        SelectedCharacter = "Lumina";
    }

    public void SelectIrene()
    {
        SelectedCharacter = "Irene";
    }
}
