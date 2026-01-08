using Script.Manager;
using Script.Stage.Handler;
using Script.Stage.StageHandler;
using TMPro;
using UnityEngine;

namespace Script.Stage.StageHandler
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private SceneReferences sceneReferences;

        void Awake()
        {
            GameObject managerObject = new GameObject("StageManager");
            if (SceneLoadManager.isInfinityMode)
            {
                InfinityStageHandler handler = managerObject.AddComponent<InfinityStageHandler>();
                handler.Setup(sceneReferences);
            }
            else if(SceneLoadManager.SelectedChapterNo == 2 && SceneLoadManager.SelectedRoundNo == 3)
            {
                BossStageHandler handler = managerObject.AddComponent<BossStageHandler>();
                handler.Setup(sceneReferences);
            }
            else
            {
                StoryStageHandler handler = managerObject.AddComponent<StoryStageHandler>();
                handler.Setup(sceneReferences);
            }
        }
    }

    [System.Serializable]
    public class SceneReferences
    {
        public EnemySpawnHandler spawner;
        public TextMeshProUGUI waveTitle;
        public TextMeshProUGUI roundPanelRound;
        public TextMeshProUGUI roundPanelTitle;
        public SpriteRenderer backGroundImage;
        public GameObject player;
    }
}