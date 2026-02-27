using System;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureSummonHandler : MonoBehaviour
    {
        public bool IsOnSummoned { get; set; } = false;
        public float SummonChance { get; set; }
        public Probability Rarity { get; private set; } = Probability.Common;
        public int Cost { get; private set; }

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

        public void SetCost()
        {
            switch (Rarity)
            {
                case Probability.Rare:
                    Cost = Constant.Upgrade.Cost.TO_SUPERRARE_COST;
                    break;
                case Probability.SuperRare:
                    Cost = Constant.Upgrade.Cost.TO_ULTRARARE_COST;
                    break;
                case Probability.UltraRare:
                    Cost = Constant.Upgrade.Cost.UPPER_ULTRARERE_COST;
                    break;
            }
        }

        public void Initialize(int cost)
        {
            Cost = cost;
        }

        public CreatureSummonHandler FirstSummonInitialize()
        {
            SetNextProbability();
            SetCost();
            CardPoolManager.Manager.AddCardInPool(this);
            //업그레이드 파트
            IsOnSummoned = true;
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