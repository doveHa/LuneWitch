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
            var creatures = new List<CreatureData>();
            foreach (KeyValuePair<string, CreatureData> data in UnlockedCharacterManager.Manager.allCharacterData)
            {
                creatures.Add(data.Value);
            }

            CardPoolManager.Manager.InitialCreature(creatures);
        }
    }
}