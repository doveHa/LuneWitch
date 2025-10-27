using System;
using System.Collections.Generic;
using Script.Enum;

namespace Script.Manager
{
    public class PlayerManager : ManagerBase<PlayerManager>
    {
        public CharacterName SelectedCharacter { get; private set; }

        public List<CharacterData> SelectedCreatures { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            SelectedCreatures = new List<CharacterData>();
        }

        public void SetCharacter(CharacterName character)
        {
            SelectedCharacter = character;
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