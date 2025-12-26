using Script.Creature.AttackHandler;
using UnityEngine;

namespace Script.Creature.UpgradeHandler
{
    public abstract class BaseUpgradeHandler : MonoBehaviour
    {
        public abstract void Upgrade(BaseAttackHandler attackHandler);
    }
}