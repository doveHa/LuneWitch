using Script.Character.DataDefinitions.ScriptableObjects;
using Script.Core.Manager;
using Script.UI.MainScene;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.ButtonOnClick
{
    public class CharacterButton : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] public CharacterData characterData;

        private Sprite onSelect, offSelect;

        void Start()
        {
            onSelect = ResourceManager.Load<Sprite>(Constant.ResourcePath.UI_IMAGE_PATH_BY_NAME("OnSelect"));
            offSelect = ResourceManager.Load<Sprite>(Constant.ResourcePath.UI_IMAGE_PATH_BY_NAME("OffSelect"));
        }

        protected override void OnClick()
        {
            GetComponentInParent<CharacterSelectButtonHandler>().SetCharacter(characterData);
        }

        public void SetOnSelect()
        {
            GetComponent<Image>().sprite = onSelect;
        }

        public void SetOffSelect()
        {
            GetComponent<Image>().sprite = offSelect;
        }
    }
}