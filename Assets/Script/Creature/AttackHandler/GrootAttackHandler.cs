using System.Collections;
using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Enemy.Handler;
using UnityEngine;

namespace Script.Creature.AttackHandler
{
    public class GrootAttackHandler : BaseAttackHandler
    {
        [SerializeField] private float slowTime = 2;
        [SerializeField] private float disSpeedRate = 0.5f;
        [SerializeField] private GameObject ammoPrefab;
        [SerializeField] private Transform shootPoint;


        public override HashSet<CardZoneCoordinate> AttackRanges()
        {
            HashSet<CardZoneCoordinate> attackRange = new HashSet<CardZoneCoordinate>();
            attackRange.Add(new CardZoneCoordinate(RootCoordinate.Row, CardZoneCoordinate.MAXCOL - 1));
            return attackRange;
        }

        protected override void Attack(List<EnemyHandler> enemies)
        {
        }

        public void ShootAmmo()
        {
            GrootAmmoHandler ammoHandler = Instantiate(ammoPrefab, shootPoint.position, Quaternion.identity)
                .GetComponent<GrootAmmoHandler>();
            CardZoneHandler cardZone =
                CardZoneManager.Manager.GetZone(new CardZoneCoordinate(RootCoordinate.Row,
                    CardZoneCoordinate.MAXCOL - 1));
            
            ammoHandler.ShootAmmo(shootPoint.transform,cardZone.transform);
            ammoHandler.SetStat(Atk, disSpeedRate, slowTime);
        }
    }
}