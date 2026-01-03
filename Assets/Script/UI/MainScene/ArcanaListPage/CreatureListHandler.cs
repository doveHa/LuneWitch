using Script.Core.Manager;
using Script.Creature.DataDefinitions.ScriptableObjects;
using Script.Manager;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.UI.MainScene.ArcanaListPage
{
    public class CreatureListHandler : MonoBehaviour
    {
        void Start()
        {
            GameObject cardPrefab =
                ResourceManager.Load<GameObject>(Constant.ResourcePath.UI_OBJECT_PATH_BY_NAME("Card"));
            GameObject emptyCard =
                ResourceManager.Load<GameObject>(Constant.ResourcePath.UI_OBJECT_PATH_BY_NAME("EmptyCard"));
            
            foreach (var creature in PlayerManager.Manager.AllCreatureData)
            {
                var card = Instantiate(cardPrefab, transform);
                card.GetComponent<CardSlot>().CardInitialize(creature.Value, GetComponentInParent<CreaturePageHandler>().ShowDescription);
            }

            while (transform.childCount % 4 != 0)
            {
                Instantiate(emptyCard, transform);
            }
        }
    }
}