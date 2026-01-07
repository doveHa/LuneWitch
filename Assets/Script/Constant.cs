using System.IO;
using Script.Stage;
using Unity.Profiling;
using UnityEngine.Device;

namespace Script
{
    public static class Constant
    {
        public static class ResourcePath
        {
            public static string UI_IMAGE_PATH_BY_NAME(string uiName)
            {
                return "Images/UI/" + uiName;
            }

            public static string UI_OBJECT_PATH_BY_NAME(string uiName)
            {
                return "Prefabs/UI/" + uiName;
            }

            public const string TUTORIAL_IMAGES_PATH = "Images/Tutorial";

            public static string CHARACTER_DATA_PATH_BY_NAME(string characterName)
            {
                return "Character/" + characterName + "/CharacterData/" + characterName;
            }

            public static string CREATURE_DATA_PATH_BY_NAME(string creatureName)
            {
                return "Creature/" + creatureName + "/CreatureData/" + creatureName;
            }

            public static string ENEMY_PATH_BY_ENEMY_NAME(string enemyName)
            {
                return "Enemies/" + enemyName + "/EnemyData/" + enemyName;
            }

            public static string GAMEOBJECT_PATH_BY_CHARACTER_NAME(string characterName)
            {
                return "Prefabs/Character/" + characterName;
            }

            public static string STAGE_INFO_DATA_PATH_BY_CHAPTER_ROUND(int chapter, int round)
            {
                return "StageInfo/Chapter" + chapter + "Round" + round;
            }

            public static string INFINITY_STAGE_INFO_DATA_PATH = "StageInfo/InfinityStageData";
        }

        public static class BattleSystem
        {
            public const int MAX_CARDS = 4;

            public const float HIT_TIME = 0.3f;
        }

        public static class Scene
        {
            public const string MAIN_SCENE = "Main";
        }

        public static class Upgrade
        {
            public static class Attack
            {
                public static int FIRSTUPGRADE = 10;
                public static int SECONDUPGRADE = 20;
                public static int THIRDUPGRADE = 30;
            }

            public static float AttackTerm = 1.0f;

            public static class Health
            {
                public static int FIRSTUPGRADE = 20;
                public static int SECONDUPGRADE = 30;
                public static int THIRDUPGRADE = 50;
            }
        }
    }
}