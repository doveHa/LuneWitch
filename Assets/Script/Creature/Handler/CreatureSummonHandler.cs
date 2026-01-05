using System;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Handler;
using Script.BattleStyle.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureSummonHandler : MonoBehaviour
    {
        public bool IsOnSummoned { get; set; } = false;
        public float SummonChance { get; set; }
        public Probability Rarity { get; private set; } = Probability.Common;

        public bool IsTemplate { get; set; } = false;

        public void SetNextProbability()
        {
            Probability[] probabilities = Enum.GetValues(typeof(Probability)) as Probability[];
            Array.Reverse(probabilities);

            foreach (Probability probability in probabilities)
            {
                if ((int)Rarity > (int)probability)
                {
                    Rarity = probability;
                    return;
                }
            }
        }

        public CreatureSummonHandler FirstSummonInitialize()
        {
            SetNextProbability();
            IsOnSummoned = true;
            CardPoolManager.Manager.AddCardInPool(this);
            return this;
        }

        public void VisualizeCreature()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        public void NormalizeCreature()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
    }
}