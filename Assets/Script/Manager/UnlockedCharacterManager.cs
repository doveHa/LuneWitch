using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Script.Manager
{
    public class UnlockedCharacterManager : ManagerBase<UnlockedCharacterManager>
    {
        public Dictionary<string, CharacterData> unlockCharacters { get; private set; }
        public Dictionary<string, CharacterData> allCharacterData { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            unlockCharacters = new Dictionary<string, CharacterData>();
            allCharacterData = new Dictionary<string, CharacterData>();
        }

        private void Start()
        {
            LoadCreatureData();

            UnlockIfNotAlready("Pumpy");
            UnlockIfNotAlready("Silum");
            UnlockIfNotAlready("ManaStone");
            UnlockIfNotAlready("Limeln");
        }

        private void UnlockIfNotAlready(string characterName)
        {
            if (!unlockCharacters.ContainsKey(characterName))
            {
                Unlock(characterName);
            }
        }

        public void Unlock(string characterName)
        {
            unlockCharacters.Add(characterName, allCharacterData[characterName]);
            allCharacterData[characterName].isUnlocked = true;
            SaveUnlockedCharacters();
        }

        private void SaveUnlockedCharacters()
        {
            List<string> saveList = new List<string>();
            foreach (KeyValuePair<string, CharacterData> pairs in unlockCharacters)
            {
                saveList.Add(pairs.Key);
            }

            PersistentDataReadWriteManager.Manager.Write(Constant.PersistentPath.UNLOCKED_CHARACTERS, JsonSerializer.Serialize(saveList));
        }

        private void LoadCreatureData()
        {
            foreach (CharacterData character in
                     ResourceManager.LoadAll<CharacterData>("CharacterData/SummonedCreature"))
            {
                allCharacterData.Add(character.name, character);
            }

            if (File.Exists(Constant.PersistentPath.UNLOCKED_CHARACTERS))
            {
                List<string> loadList = PersistentDataReadWriteManager.Manager.ReadJson<List<string>>(Constant.PersistentPath.UNLOCKED_CHARACTERS);
                foreach (string characterName in loadList)
                {
                    if (allCharacterData.ContainsKey(characterName))
                    {
                        unlockCharacters.Add(characterName, allCharacterData[characterName]);
                    }
                }
            }
        }
    }
}