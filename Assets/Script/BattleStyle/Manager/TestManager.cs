using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.DataDefinitions.ScriptableObjects;
using Script.Manager;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class TestManager : ManagerBase<TestManager>
    {
        void Start()
        {
            CardPoolManager.Manager.InitialCreature(LoadCreatureData());
        }

        [SerializeField] private string[] creatureNames =
        {
            "Balloon", "Bat", "Broomstick", "Spider", "Shadow", "Groot", "Frog", "Hippocrates", "Limeln", "ManaStone",
            "Mandragora", "Muret", "Pumpy", "Silum"
        };

        private List<CreatureData> LoadCreatureData()
        {
            var creatureDataList = new List<CreatureData>();

            foreach (string creatureName in creatureNames)
            {
                CreatureData data =
                    ResourceManager.Load<CreatureData>(
                        Constant.ResourcePath.CHARACTER_DATA_PATH_BY_NAME(creatureName));
                creatureDataList.Add(data);
            }

            return creatureDataList;
        }
    }
}