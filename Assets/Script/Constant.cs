using System.IO;
using UnityEngine.Device;

namespace Script
{
    public static class Constant
    {
        public static class ResourcePath
        {
            public static string SPRITE_PATH_BY_CHARACTER_NAME(string spriteName)
            {
                return "Images/Character/" + spriteName;
            }

            public static string SPRITE_PATH_BY_CREATURE_NAME(string spriteName)
            {
                return "Images/Creature/" + spriteName;
            }
        }

        public static class PersistentPath
        {
            public static readonly string UNLOCKED_CHARACTERS =
                Path.Combine(Application.persistentDataPath + "/UnlockedCharacters.json");
        }

        public static class SummonedCreatures
        {
            public static string[] DEFAULT_CREATURES = { "MagicStone", "Pumpy", "Shilrum" };
        }
    }
}