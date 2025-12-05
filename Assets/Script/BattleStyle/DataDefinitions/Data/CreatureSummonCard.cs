using Script.BattleStyle.DataDefinitions.Enum;
using Script.BattleStyle.Handler;
using Script.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.BattleStyle.DataDefinitions.Data
{
    public class CreatureSummonCard
    {
        public string CreatureName { get; set; }
        public GameObject CreaturePrefab { get; set; }
        public Probability Rarity { get; set; } = Probability.Common;
        public Sprite CreatureImage { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public CreatureSummonCard(CharacterData characterData)
        {
            CreatureName = characterData.characterName;
            CreaturePrefab = characterData.creaturePrefab;
            CreatureImage = characterData.characterImage;
            Description = characterData.description;
            Cost = characterData.cost;
        }

        public CreatureSummonCard(CreatureSummonCard cardData, CardZoneHandler cardZone)
        {
            CreatureName = cardData.CreatureName;
            CreaturePrefab = cardData.CreaturePrefab;
            CreatureImage = cardData.CreatureImage;
            Description = cardData.Description;
            Rarity = cardData.Rarity;
        }
    }
}