using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Handler;
using UnityEngine;

namespace Script.BattleStyle.ButtonOnClick
{
    public class UpgradeButton : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private UpgradeType upgradeType;

        protected override void OnClick()
        {
            GetComponentInParent<CardHandler>().UpgradeCard(upgradeType);
            transform.parent.gameObject.SetActive(false);
        }
    }
}