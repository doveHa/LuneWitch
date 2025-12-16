using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using Script.DataDefinitions.ScriptableObjects;

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
            SaveUnlockedCharacters();
        }

        private void SaveUnlockedCharacters()
        {
            List<string> saveList = new List<string>();
            foreach (KeyValuePair<string, CharacterData> pairs in unlockCharacters)
            {
                saveList.Add(pairs.Key);
            }

            PersistentDataReadWriteManager.Manager.Write(Constant.PersistentPath.UNLOCKED_CHARACTERS,
                JsonSerializer.Serialize(saveList));
        }

        private string[] creatureNames =
        {
            "Balloon", "Bat", "Broomstick", "Frog", "Groot", "Hippocrates", "Limeln", "ManaStone", "Mandragora",
            "Muret", "Pumpy", "Shadow", "Silum", "Spider"
        };

        private void LoadCreatureData()
        {
            foreach (string creatureName in creatureNames)
            {
                CharacterData data =
                    ResourceManager.Load<CharacterData>(
                        Constant.ResourcePath.CHARACTER_DATA_PATH_BY_NAME(creatureName));
                allCharacterData.Add(data.characterName, data);
            }
/*
            if (File.Exists(Constant.PersistentPath.UNLOCKED_CHARACTERS))
            {
                List<string> loadList =
                    PersistentDataReadWriteManager.Manager.ReadJson<List<string>>(Constant.PersistentPath
                        .UNLOCKED_CHARACTERS);
                foreach (string characterName in loadList)
                {
                    if (allCharacterData.ContainsKey(characterName))
                    {
                        unlockCharacters.Add(characterName, allCharacterData[characterName]);
                    }
                }
            }*/
        }
    }
}