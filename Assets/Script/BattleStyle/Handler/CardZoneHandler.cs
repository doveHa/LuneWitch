using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.Enemy;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardZoneHandler : MonoBehaviour
    {
        public CardZoneCoordinate Coordinate { get; set; }
        public CreatureHandler SummonedCreature { get; set; }
        public List<EnemyHandler> Enemies { get; private set; }

        void Awake()
        {
            Enemies = new List<EnemyHandler>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log(Coordinate.ToString());
                Enemies.Add(other.GetComponent<EnemyHandler>());
            }
        }
        

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Debug.Log(Coordinate.ToString());
                Enemies.Remove(other.GetComponent<EnemyHandler>());
            }
        }

        public void SummonCreature(CreatureHandler creature)
        {
            SummonedCreature = creature;
        }

        public void DeleteCreature()
        {
            SummonedCreature = null;
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
            if (SummonedCreature == null)
            {
                return false;
            }

            return true;
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