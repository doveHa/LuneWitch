using Script.Character.DataDefinitions.ScriptableObjects;
using Script.UI.ButtonOnClick;
using UnityEngine;

namespace Script.UI.MainScene
{
    public class CharacterSelectButtonHandler : MonoBehaviour
    {
        [SerializeField] private CharacterButton[] characterSelectButton;

        public void SetCharacter(CharacterData characterData)
        {
            GetComponentInParent<CharacterSelectHandler>().SetCharacter(characterData);
            foreach (CharacterButton characterButton in characterSelectButton)
            {
                if (characterButton.characterData == characterData)
                {
                    characterButton.SetOnSelect();
                }
                else
                {
                    characterButton.SetOffSelect();
                }
            }
        }
    }
}