using System.Collections.Generic;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.Handler;
using Script.UI.Pointer.Hover;
using UnityEngine;

namespace Script.UI.Pointer.Drag
{
    public class CreatureCardDrag : MonoBehaviour, IDrag
    {
        private bool canSpawn = true;

        private List<CardZoneCoordinate> currentSpawnVisualList = new List<CardZoneCoordinate>();
        private CardZoneCoordinate lastHoveredCoord = null;

        public void Click(PointerHandler pointer, GameObject target)
        {
            if (TryGetComponent(out IHover hover))
            {
                hover.Exit();
            }

            currentSpawnVisualList.Clear();
            lastHoveredCoord = null;

            CardHandler cardHandler = target.GetComponentInParent<CardHandler>();
            if (cardHandler.IsSummoned())
            {
                pointer.CanDrag = false;
                pointer.OnlyClick();
            }
        }

        public void Drag(PointerHandler pointer, GameObject target)
        {
            if (target == null)
            {
                ClearVisuals();
                lastHoveredCoord = null;
                canSpawn = false;
                return;
            }

            CardZoneHandler cardZoneHandler = target.GetComponent<CardZoneHandler>();
            CardZoneCoordinate currentCoord = cardZoneHandler.Coordinate;

            if (lastHoveredCoord != null && lastHoveredCoord == currentCoord)
            {
                return;
            }

            ClearVisuals();
            var spawnTileList = GetComponent<CardHandler>().CreatureHandler.GetSpawnTiles(currentCoord);
            lastHoveredCoord = currentCoord;

            if (spawnTileList == null)
            {
                canSpawn = false;
            }
            else
            {
                canSpawn = true;
                CardZoneManager.Manager.SpawnVisuals(spawnTileList);

                currentSpawnVisualList = new List<CardZoneCoordinate>(spawnTileList);
            }
        }

        public void Drop(PointerHandler pointer, GameObject target)
        {
            ClearVisuals();
            lastHoveredCoord = null;

            if (target.TryGetComponent(out CardZoneHandler cardZoneHandler))
            {
                if (canSpawn)
                {
                    CreatureHandler creatureHandler =
                        GetComponent<CardHandler>().CreatureHandler;

                    cardZoneHandler.SummonedCreature = creatureHandler.SummonCreature(cardZoneHandler.Coordinate);
                    GetComponent<CardHandler>().UseCard();
                }
            }
        }

        private void ClearVisuals()
        {
            if (currentSpawnVisualList != null && currentSpawnVisualList.Count > 0)
            {
                CardZoneManager.Manager.SpawnNormalize(currentSpawnVisualList);
                currentSpawnVisualList.Clear();
            }
        }
    }
}