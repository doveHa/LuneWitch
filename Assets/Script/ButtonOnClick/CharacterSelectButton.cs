using Script.DataDefinitions.Enum;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.ButtonOnClick
{
    public class CharacterSelectButton : ButtonOnClick
    {
        [SerializeField] private CharacterName characterName;
        
        protected override void OnClick()
        {
            PlayerManager.Manager.SetCharacter(characterName);
        }
    }
}