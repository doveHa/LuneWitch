using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class SacaboomAttackHandler : BaseAttackHandler
    {
        [SerializeField] private float speed;
        private float endLineX;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            return new HashSet<CardZoneCoordinate>();
        }

        public override void Initialize(CreatureData data)
        {
            base.Initialize(data);
            if (RootCoordinate != null)
            {
                CardZoneCoordinate endLineZone = RootCoordinate;

                gameObject.transform.position = CardZoneManager.Manager
                    .GetZone(new CardZoneCoordinate(endLineZone.Row, CardZoneCoordinate.MINCOL - 1)).transform.position;

                for (int i = endLineZone.Col; i < CardZoneCoordinate.MAXCOL; i++)
                {
                    endLineZone = endLineZone.Right();
                }

                endLineX = CardZoneManager.Manager.GetZone(endLineZone).gameObject.transform.position.x;
            }
        }

        protected override void Update()
        {
            if (RootCoordinate != null)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }

            if (gameObject.transform.position.x >= endLineX)
            {
                for (int i = 0; i < CardZoneCoordinate.MAXCOL; i++)
                {
                    CardZoneHandler handler =
                        CardZoneManager.Manager.GetZone(new CardZoneCoordinate(RootCoordinate.Row, i));
                    if (handler.IsOnEnemy())
                    {
                        Attack(handler.Enemies);
                    }
                }

                Explore();
            }
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
            foreach (EnemyHandler handler in enemies)
            {
                handler.Hit(Atk);
            }
        }

        private void Explore()
        {
            Destroy(gameObject);
        }
    }
}