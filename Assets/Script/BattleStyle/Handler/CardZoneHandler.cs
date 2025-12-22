using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Creature.Handler;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardZoneHandler : MonoBehaviour
    {
        public CardZoneCoordinate Coordinate { get; set; }
        public CreatureSummonHandler SummonedCreature { get; set; }
        public List<EnemyHandler> Enemies { get; private set; }
        private GameObject attackRangeMark, spawnRangeMark;

        void Awake()
        {
            Enemies = new List<EnemyHandler>();
            attackRangeMark = transform.GetChild(0).gameObject;
            spawnRangeMark = transform.GetChild(1).gameObject;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemies.Add(other.GetComponent<EnemyHandler>());
            }
        }


        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Enemies.Remove(other.GetComponent<EnemyHandler>());
            }
        }

        public void DeleteCreature()
        {
            SummonedCreature = null;
        }

        public void SpawnVisualization()
        {
            spawnRangeMark.SetActive(true);
        }

        public void AttackRangeVisualization()
        {
            attackRangeMark.SetActive(true);
        }

        public void SpawnNormalization()
        {
            spawnRangeMark.SetActive(false);
        }

        public void AttackNormalization()
        {
            attackRangeMark.SetActive(false);
        }

        public void Visualization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        public void Normalization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        public bool IsSummoned()
        {
            if (SummonedCreature != null && SummonedCreature.IsOnSummoned)
            {
                return true;
            }

            return false;
        }

        public bool IsOnEnemy()
        {
            if (Enemies.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}