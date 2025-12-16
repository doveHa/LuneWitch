using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.DataDefinitions.ScriptableObjects;
using Script.Manager;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class TestManager : ManagerBase<TestManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        void Start()
        {
            var creatures = new List<CharacterData>();
            foreach (KeyValuePair<string, CharacterData> data in UnlockedCharacterManager.Manager.allCharacterData)
            {
                creatures.Add(data.Value);
            }

            CardPoolManager.Manager.InitialCreature(creatures);
        }
    }
}