using Script.Manager;
using Script.UI.MainScene;

namespace Script.UI.ButtonOnClick
{
    public class CharacterSelectButton : Base.ButtonOnClick
    {
        protected override void OnClick()
        {
            PlayerManager.Manager.SelectedCharacter = GetComponentInParent<CharacterSelectHandler>(true).SelectedCharacter;
        }
    }
}