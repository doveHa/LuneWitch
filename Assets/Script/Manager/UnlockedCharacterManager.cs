using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text.Json;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.DataDefinitions.ScriptableObjects;

namespace Script.Manager
{
    public class UnlockedCharacterManager : ManagerBase<UnlockedCharacterManager>
    {
        public Dictionary<string, CreatureData> unlockCharacters { get; private set; }
        public Dictionary<string, CreatureData> allCharacterData { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            unlockCharacters = new Dictionary<string, CreatureData>();
            allCharacterData = new Dictionary<string, CreatureData>();
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
            foreach (KeyValuePair<string, CreatureData> pairs in unlockCharacters)
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
                CreatureData data =
                    ResourceManager.Load<CreatureData>(
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