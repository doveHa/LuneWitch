using Script.Creature.DataDefinitions.ScriptableObjects;
using UnityEngine;

namespace Script.UI.MainScene.ArcanaListPage
{
    public class CreaturePageHandler : MonoBehaviour
    {
        public void ShowDescription(CreatureData creature)
        {
            GetComponentInChildren<CreatureDescriptionHandler>().SetDescription(creature);
            GetComponentInChildren<Animator>().SetBool("IsShow", true);
        }

        public void HideDescription()
        {
            GetComponentInChildren<Animator>().SetBool("IsShow", false);
            GetComponentInChildren<CreatureDescriptionHandler>().PositionInitialize();
        }
    }
}