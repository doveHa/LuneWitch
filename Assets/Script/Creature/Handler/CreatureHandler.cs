using System;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Handler;
using Script.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.Creature.Handler
{
    public class CreatureHandler : MonoBehaviour
    {
        public bool IsOnSummoned { get; private set; } = false;
        public float SummonChance { get; set; }
        public Probability Rarity { get; private set; }
        public CardZoneHandler CardZone { get; set; }
        public CharacterData Card { get; private set; }

        public void SetCreatureSummonCard(CharacterData card)
        {
            Rarity = Probability.Common;
            Card = card;
        }

        public void SummonCreatureSetting(CharacterData cardData)
        {
            IsOnSummoned = true;
            SetCreatureSummonCard(cardData);
        }

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
    }
}