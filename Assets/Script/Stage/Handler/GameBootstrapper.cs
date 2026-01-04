using Script.Manager;
using Script.Stage.Handler;
using TMPro;
using UnityEngine;

namespace Script.Stage.Manager
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
            else
            {
                StageHandler handler = managerObject.AddComponent<StageHandler>();
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