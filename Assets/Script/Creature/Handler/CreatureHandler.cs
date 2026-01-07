using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using Script.Core.DataDefinitions.ScriptableObjects;
using Script.Core.Handler;
using Script.Creature.AttackHandler;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Creature.UpgradeHandler;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureHandler : BaseHandler
    {
        public CreatureData CreatureData { get; private set; }
        public CreatureSummonHandler CreatureSummonHandler { get; private set; }
        public BaseAttackHandler AttackHandler { get; private set; }

        void Update()
        {
            if (AttackHandler.IsCoolOn() && AttackHandler.HasTarget())
            {
                AnimationHandler.PlayAttackAnimation();
                AttackHandler.Cooldown();
            }
        }

        public override void Initialize(BaseData data)
        {
            CreatureData = data as CreatureData;
            HealthHandler = new CreatureHealthHandler(CreatureData.health, CreatureData.cost);
            AnimationHandler = GetComponent<CreatureAnimationHandler>();
            CreatureSummonHandler = GetComponent<CreatureSummonHandler>();
            AttackHandler = GetComponent<BaseAttackHandler>();
            AttackHandler.Initialize(CreatureData);
        }

        public override void Dead()
        {
            CardPoolManager.Manager.RemoveCardInPool(CreatureSummonHandler);
            AnimationHandler.PlayDeathAnimation();
            Destroy(gameObject);
        }

        public CreatureSummonHandler SummonCreature(CardZoneCoordinate coordinate)
        {
            Transform summonTransform = CardZoneManager.Manager.GetZone(coordinate).gameObject.transform;

            GameObject creatureObject =
                Instantiate(CreatureData.prefab, summonTransform.position, Quaternion.identity);
            creatureObject.name = CreatureData.name;
            creatureObject.transform.SetParent(summonTransform);
            creatureObject.GetComponent<BaseAttackHandler>().RootCoordinate = coordinate;
            creatureObject.GetComponent<CreatureHandler>().Initialize(CreatureData);
            if (creatureObject.GetComponent<CreatureHandler>().CreatureData.isActive)
            {
                return creatureObject.GetComponent<CreatureSummonHandler>();
            }

            return creatureObject.GetComponent<CreatureSummonHandler>().FirstSummonInitialize();
        }

        public void UpgradeCreature(UpgradeType upgradeType)
        {
            CardPoolManager.Manager.UpgradeCard(CreatureSummonHandler);
            switch (upgradeType)
            {
                case UpgradeType.Attack:
                    BaseUpgradeHandler.UpgradeAttack(AttackHandler, CreatureSummonHandler.Rarity);
                    break;
                case UpgradeType.Health:
                    BaseUpgradeHandler.UpgradeHealth(HealthHandler, CreatureSummonHandler.Rarity);
                    break;
                case UpgradeType.AttackTerm:
                    BaseUpgradeHandler.UpgradeAttackTerm(AttackHandler);
                    break;
            }
        }

        public List<CardZoneCoordinate> GetSpawnTiles(CardZoneCoordinate coordinate)
        {
            List<CardZoneCoordinate> spawnTiles = new List<CardZoneCoordinate>();
            for (int x = 0; x < CreatureData.unitSize.x; x++)
            {
                for (int y = 0; y < CreatureData.unitSize.y; y++)
                {
                    int targetCol = coordinate.Col + x;
                    int targetRow = coordinate.Row - y;

                    if (targetCol < 0 || targetCol > CardZoneCoordinate.MAXCOL ||
                        targetRow < 0 || targetRow > CardZoneCoordinate.MAXROW)
                    {
                        return null;
                    }

                    spawnTiles.Add(new CardZoneCoordinate(targetRow, targetCol));
                }
            }

            return spawnTiles;
        }
    }
}