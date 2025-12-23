using System.Collections.Generic;
using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.DataDefinitions.Enum;

namespace Script.Manager
{
    public class PlayerManager : ManagerBase<PlayerManager>
    {
        public CharacterName SelectedCharacter { get; set; }

        public List<CreatureData> SelectedCreatures { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SelectedCreatures = new List<CreatureData>();
        }

        private const int MAX_CARDS = 4;

        public bool IsAllCardSelected()
        {
            return SelectedCreatures.Count == MAX_CARDS;
        }

        public void AddCreature(CreatureData creature)
        {
            if (SelectedCreatures.Count < MAX_CARDS)
            {
                SelectedCreatures.Add(creature);
            }
        }
    }
}