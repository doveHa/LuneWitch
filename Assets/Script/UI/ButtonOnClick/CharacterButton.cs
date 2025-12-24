using Script.Character.DataDefinitions.ScriptableObjects;
using Script.UI.MainScene;
using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class CharacterButton : Base.ButtonOnClick
    {
        [SerializeField] private CharacterData characterData;

        protected override void OnClick()
        {
            GetComponentInParent<CharacterSelectHandler>().SetCharacter(characterData);
        }
    }
}