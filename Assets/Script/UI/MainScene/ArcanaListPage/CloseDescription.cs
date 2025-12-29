namespace Script.UI.MainScene.ArcanaListPage
{
    public class CloseDescription : Core.OnButtonClick.ButtonOnClick
    {
        protected override void OnClick()
        {
            GetComponentInParent<CreaturePageHandler>(true).HideDescription();
        }
    }
}