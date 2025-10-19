using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public string curType; // 루미나 또는 아이린

    public void SelectCurrentCharacter()
    {
        // 선택한 캐릭터 저장
        PlayerPrefs.SetString("SelectedCharacter", curType);
        PlayerPrefs.Save(); // 저장 강제 수행

       
    }
}
