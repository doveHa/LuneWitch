using System.IO;
using Script.Stage;
using UnityEngine.Device;

namespace Script
{
    public static class Constant
    {
        public static class ResourcePath
        {
            public static string GAMEOBJECT_PATH_BY_CREATURE_NAME(string creatureName)
            {
                return "Prefabs/Creatures/" + creatureName;
            }

            public static string UI_PATH_BY_NAME(string uiName)
            {
                return "Images/UI/" + uiName;
            }

            public const string TUTORIAL_IMAGES_PATH = "Images/Tutorial";

            public const string ALL_CREATURES_PATH = "CharacterData/SummonedCreature";

            public static string GAMEOBJECT_PATH_BY_ENEMY_NAME(string enemyName)
            {
                return "Prefabs/Enemy/" + enemyName;
            }

            public static string GAMEOBJECT_PATH_BY_CHARACTER_NAME(string characterName)
            {
                return "Prefabs/Character/" + characterName;
            }

            public static string STAGE_INFO_DATA_PATH_BY_CHAPTER_ROUND(int chapter, int round)
            {
                return "StageInfo/Chapter" + chapter + "Round" + round;
            }
        }

        public static class PersistentPath
        {
            public static readonly string UNLOCKED_CHARACTERS =
                Path.Combine(Application.persistentDataPath + "/UnlockedCharacters.json");
        }

        public static class Scene
        {
            public const string MAIN_SCENE = "Main";

        }
    }
}