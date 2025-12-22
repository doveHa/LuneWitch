using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class RangedAttackHandler : BaseAttackHandler
    {
        [SerializeField] private GameObject ammoPrefab;
        [SerializeField] private Transform shootPoint;
        [SerializeField] private float ammoSpeed = 1f;

        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            var attackRange = new HashSet<CardZoneCoordinate>();
            CardZoneCoordinate coordinate = RootCoordinate;

            for (int i = RootCoordinate.Col; i <= CardZoneCoordinate.MAXCOL; i++)
            {
                coordinate = coordinate.Right();
                attackRange.Add(coordinate);
            }

            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
        }

        public void ShootAmmo()
        {
            AmmoHandler ammoHandler = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity)
                .GetComponent<AmmoHandler>();
            ammoHandler.SetStat(Atk, ammoSpeed);
            ammoHandler.AddForce();
        }
    }
}