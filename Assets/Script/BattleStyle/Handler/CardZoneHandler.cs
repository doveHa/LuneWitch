using System;
using Script.BattleStyle.DataDefinitions.Data;
using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Manager;
using UnityEngine;

namespace Script.BattleStyle.Handler
{
    public class CardZoneHandler : MonoBehaviour
    {
        public bool IsSpawned { get; set; } = false;

        public CreatureSummonCard CardData { get; private set; }

        public void Initialize(CreatureSummonCard cardData)
        {
            CardData = new CreatureSummonCard(cardData, this);
            IsSpawned = true;

            GameObject creature =
                Instantiate(CardData.CreaturePrefab, transform.position, Quaternion.identity);
            creature.name = CardData.CreatureName;
            creature.transform.SetParent(transform);

            CardData.IsOnSummoned = true;
            CardData.Rarity = NextProbability();
            CardPoolManager.Manager.AddCardInPool(CardData);
        }

        public void Visualization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.red;
        }

        public void Normalization()
        {
            GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }

        private Probability NextProbability()
        {
            foreach (Probability probability in Enum.GetValues(typeof(Probability)))
            {
                if ((int)CardData.Rarity > (int)probability)
                {
                    return probability;
                }
            }

            return CardData.Rarity;
        }
    }
}