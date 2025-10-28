using UnityEngine;

namespace Script.Stage
{
    [CreateAssetMenu(fileName = "StageInfo", menuName = "StageInfo")]
    public class StageInfoData : ScriptableObject
    {
        public int chapter;
        public int round;
        public int enemyCount;
        public string roundTitle;
        public Sprite backGroundImage;
        public string[] enemyNames;
    }
}