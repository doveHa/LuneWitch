using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.Manager;
using Script.Core.Manager;
using Script.Enemy.DataDefinitions.ScriptableObjects;
using Script.Manager;
using Script.Stage.Handler;
using UnityEngine;

namespace Script.Stage.Manager
{
    public abstract class StageHandlerBase : MonoBehaviour
    {
        public int SpawnCount { get; protected set; }
        public List<EnemyData> EnemyData { get; private set; }

        protected SceneReferences sceneReferences;

        protected void Start()
        {
            CardPoolManager.Manager.InitialCreature(PlayerManager.Manager.SelectedCreatures);
            CardPoolManager.Manager.ReRoll();
        }

        public virtual void Setup(SceneReferences references)
        {
            sceneReferences = references;

            sceneReferences.waveTitle = references.waveTitle;
            sceneReferences.roundPanelRound = references.roundPanelRound;
            sceneReferences.roundPanelTitle = references.roundPanelTitle;
            sceneReferences.backGroundImage = references.backGroundImage;
            sceneReferences.player = references.player;
        }

        public abstract IEnumerator StartGame();

        public GameObject Player()
        {
            return sceneReferences.player;
        }

        public EnemySpawnHandler Spawner()
        {
            return sceneReferences.spawner;
        }

        protected void SetPlayer()
        {
            Instantiate(PlayerManager.Manager.SelectedCharacter.prefab, sceneReferences.player.transform).name =
                PlayerManager.Manager.SelectedCharacter.name;
        }

        protected void SetEnemyData(EnemyData[] enemies)
        {
            EnemyData = new List<EnemyData>(enemies);
        }
    }
}