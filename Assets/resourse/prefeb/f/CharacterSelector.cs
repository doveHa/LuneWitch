using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelector : MonoBehaviour
{
    public string curType; // ��̳� �Ǵ� ���̸�

    public void SelectCurrentCharacter()
    {
        // ������ ĳ���� ����
        PlayerPrefs.SetString("SelectedCharacter", curType);
        PlayerPrefs.Save(); // ���� ���� ����

       
    }
}
