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

        public CardZoneHandler GetZone(CardZoneCoordinate coordinate)
        {
            return handlers[coordinate.Row, coordinate.Col];
        }
    }
}