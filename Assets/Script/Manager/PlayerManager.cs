using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Script.Character.DataDefinitions.ScriptableObjects;
using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.DataDefinitions.Enum;

namespace Script.Manager
{
    public class PlayerManager : ManagerBase<PlayerManager>
    {
        public CharacterData SelectedCharacter { get; set; }

        public Dictionary<string, CreatureData> AllCreatureData;
        public List<CreatureData> SelectedCreatures { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            SelectedCharacter =
                ResourceManager.Load<CharacterData>(Constant.ResourcePath.CHARACTER_DATA_PATH_BY_NAME("Lumina"));
            AllCreatureData = new Dictionary<string, CreatureData>();
            SelectedCreatures = new List<CreatureData>();
        }

        void Start()
        {
            foreach (CreatureData data in ResourceManager.LoadAll<CreatureData>("Creature"))
            {
                AllCreatureData.Add(data.name, data);
            }
        }

        public bool IsAllCardSelected()
        {
            return SelectedCreatures.Count == Constant.BattleSystem.MAX_CARDS;
        }

        public bool AddCreature(CreatureData creature)
        {
            if (SelectedCreatures.Count < Constant.BattleSystem.MAX_CARDS)
            {
                SelectedCreatures.Add(creature);
                return true;
            }

            return false;
        }
    }
}