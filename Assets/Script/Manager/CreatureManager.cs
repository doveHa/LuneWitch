using System.Collections.Generic;
using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;

namespace Script.Manager
{
    public class CreatureManager : ManagerBase<CreatureManager>
    {
        public Dictionary<string, CreatureData> AllCreatureData;

        protected override void Awake()
        {
            base.Awake();
            AllCreatureData = new Dictionary<string, CreatureData>();
        }

        private void Start()
        {
            foreach (CreatureData data in ResourceManager.LoadAll<CreatureData>("Creature"))
            {
                AllCreatureData.Add(data.name, data);
            }
        }
    }
}