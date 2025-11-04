using System;
using System.Collections.Generic;
using Script.DataDefinitions.Enum;
using Script.DataDefinitions.ScriptableObjects;

namespace Script.Manager
{
    public class PlayerManager : ManagerBase<PlayerManager>
    {
        public CharacterName SelectedCharacter { get; set; }

        public List<CharacterData> SelectedCreatures { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SelectedCreatures = new List<CharacterData>();
        }

        private const int MAX_CARDS = 4;

        public bool IsAllCardSelected()
        {
            return SelectedCreatures.Count == MAX_CARDS;
        }

        public bool AddCreature(CharacterData character)
        {
            if (SelectedCreatures.Count < MAX_CARDS)
            {
                SelectedCreatures.Add(character);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}