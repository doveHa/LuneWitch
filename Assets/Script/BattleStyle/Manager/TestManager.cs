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
            CharacterData[] allPrefabs =
                ResourceManager.LoadAll<CharacterData>(Constant.ResourcePath.ALL_CREATURES_PATH);
            foreach (CharacterData characterData in allPrefabs)
            {
                creatures.Add(characterData);
            }

            CardPoolManager.Manager.InitialCreature(creatures);
        }
    }
}