using System;
using System.Collections.Generic;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Boss.Handler
{
    public class BossAttackHandler : MonoBehaviour
    {
        [SerializeField] private GameObject[] tentacles;
        [SerializeField] private int atk = 50;

        private List<GameObject> spawnedTentacles;

        [SerializeField] private SpriteRenderer pattern3Sprite;
        private CreatureHandler pattern3Target;
        
        void Awake()
        {
            spawnedTentacles = new List<GameObject>();
        }


        public Action ActiveBossPattern(int patternIndex)
        {
            switch (patternIndex)
            {
                case 1:
                    return Pattern1;
                case 2:
                    return Pattern2;
                case 3:
                    return Pattern3;
            }

            return null;
        }

        public int GetAttackPatternIndex()
        {
            return Random.Range(1, 4);
        }

        public void SpawnTentacles()
        {
            CreatureHandler[] handlers =
                CardZoneManager.Manager.GridRootTransform().GetComponentsInChildren<CreatureHandler>();
            int tentacleCount = Mathf.Min(4, handlers.Length);

            for (int i = 0; i < tentacleCount; i++)
            {
                int tentacleIndex = Random.Range(0, tentacles.Length);
                int creatureIndex = Random.Range(0, handlers.Length);

                spawnedTentacles.Add(Instantiate(tentacles[tentacleIndex], handlers[creatureIndex].transform.position,
                    Quaternion.identity));
                handlers[creatureIndex].Hit(atk);
            }
        }

        public void DestroyTentacles()
        {
            foreach (GameObject spawnedTentacle in spawnedTentacles)
            {
                Destroy(spawnedTentacle);
            }
        }

        private void Pattern1()
        {
            Debug.Log("Pattern 1");
        }

        public void AllCreatureDeal()
        {
            CreatureHandler[] handlers =
                CardZoneManager.Manager.GridRootTransform().GetComponentsInChildren<CreatureHandler>();

            foreach (CreatureHandler creatureHandler in handlers)
            {
                creatureHandler.Hit(atk);
            }
            
        }
        private void Pattern2()
        {
            Debug.Log("Pattern 2");
        }

        public void GrabCreature()
        {
            pattern3Target.Dead();
            pattern3Sprite.gameObject.SetActive(true);
        }

        public void KillCreature()
        {
            pattern3Sprite.gameObject.SetActive(false);
        }

        private void Pattern3()
        {
            CreatureHandler[] handlers =
                CardZoneManager.Manager.GridRootTransform().GetComponentsInChildren<CreatureHandler>();

            int killCreatureIndex = Random.Range(0, handlers.Length);
            pattern3Sprite.sprite = handlers[killCreatureIndex].CreatureData.characterImage;
            pattern3Target = handlers[killCreatureIndex];
        }
    }
}