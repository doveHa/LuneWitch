using Script.BattleStyle.DataDefinitions.Enum;
using Script.Core.Handler;
using Script.Creature.AttackHandler;
using UnityEngine;

namespace Script.Creature.UpgradeHandler
{
    public static class BaseUpgradeHandler
    {
        public static void UpgradeAttack(BaseAttackHandler attackHandler, Probability rarity)
        {
            attackHandler.UpgradeAttack(AttackUpgradeStat(rarity));
        }

        public static void UpgradeAttackTerm(BaseAttackHandler attackHandler)
        {
            attackHandler.UpgradeAttackTerm(Constant.Upgrade.AttackTerm);
        }

        public static void UpgradeHealth(HealthHandler healthHandler, Probability rarity)
        {
            healthHandler.HealthAddUpgrade(HealthUpgradeStat(rarity));
        }

        private static int AttackUpgradeStat(Probability rarity)
        {
            int stat = 0;
            switch (rarity)
            {
                case Probability.Rare:
                    stat = Constant.Upgrade.Attack.FIRSTUPGRADE;
                    break;
                case Probability.SuperRare:
                    stat = Constant.Upgrade.Attack.SECONDUPGRADE;
                    break;
                case Probability.UltraRare:
                    stat = Constant.Upgrade.Attack.THIRDUPGRADE;
                    break;
            }

            return stat;
        }

        private static int HealthUpgradeStat(Probability rarity)
        {
            int stat = 0;
            switch (rarity)
            {
                case Probability.Rare:
                    stat = Constant.Upgrade.Health.FIRSTUPGRADE;
                    break;
                case Probability.SuperRare:
                    stat = Constant.Upgrade.Health.SECONDUPGRADE;
                    break;
                case Probability.UltraRare:
                    stat = Constant.Upgrade.Health.THIRDUPGRADE;
                    break;
            }

            return stat;
        }
        //public abstract void Upgrade(BaseAttackHandler attackHandler);
    }
}