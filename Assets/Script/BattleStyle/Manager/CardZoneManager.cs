using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.Enemy;
using Script.Manager;
using UnityEngine;

namespace Script.BattleStyle.Manager
{
    public class CardZoneManager : ManagerBase<CardZoneManager>
    {
        [SerializeField] private Transform gridRoot;

        private CardZoneHandler[,] handlers;

        protected override void Awake()
        {
            base.Awake();
            handlers = new CardZoneHandler[5, 9];
            for (int i = 0; i < gridRoot.childCount; i++)
            {
                Transform raw = gridRoot.GetChild(i);
                for (int j = 0; j < raw.childCount; j++)
                {
                    handlers[i, j] = raw.GetChild(j).GetComponent<CardZoneHandler>();
                    handlers[i, j].Coordinate = new CardZoneCoordinate(i, j);
                }
            }
        }

        public void HitDamage(List<CardZoneCoordinate> range, int damage)
        {
            foreach (CardZoneCoordinate coordinate in range)
            {
                CardZoneHandler zone = GetZone(coordinate);
                if (zone.Enemies.Count > 0)
                {
                    foreach (EnemyHandler enemyHandler in zone.Enemies)
                    {
                        enemyHandler.Hit(damage);
                    }
                }
            }
        }

        public CardZoneHandler GetZone(CardZoneCoordinate coordinate)
        {
            return handlers[coordinate.Raw, coordinate.Col];
        }
    }
}