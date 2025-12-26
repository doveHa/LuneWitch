using Script.BattleStyle.Manager;

namespace Script.BattleStyle.ButtonOnClick
{
    public class ReRollButton : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            CardPoolManager.Manager.ReRoll();
        }
    }
}