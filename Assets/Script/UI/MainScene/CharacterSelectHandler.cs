using System.Resources;
using Script.Character.DataDefinitions.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ResourceManager = Script.Core.Manager.ResourceManager;

namespace Script.UI.MainScene
{
    public class CharacterSelectHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI characterName, introduction, skillIntroduction, dialogueCharacterName, dialogueText;

        [SerializeField] private Image characterIcon, mainCharacterImage;
        [SerializeField] private Transform characterButtonScrollContent;

        void Start()
        {
            SetCharacter(
                ResourceManager.Load<CharacterData>(Constant.ResourcePath.CHARACTER_DATA_PATH_BY_NAME("Lumina")));
        }

        public CharacterData SelectedCharacter { get; private set; }

        public void SetCharacter(CharacterData characterData)
        {
            characterName.text = characterData.name;
            introduction.text = characterData.introduction;
            skillIntroduction.text = characterData.skillIntroduction;
            characterIcon.sprite = characterData.sdSprite;

            dialogueCharacterName.text = characterData.name;
            dialogueText.text = characterData.dialogueText;

            mainCharacterImage.sprite = characterData.ldSprite;
            
            SelectedCharacter = characterData;
        }
    }
}