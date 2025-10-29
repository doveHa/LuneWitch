using Script.Creature;
using UnityEngine;

namespace Script.Card
{
    public class CreatureMouseUp : IMouseUp
    {
        public GameObject SpawnPrefab { private get; set; }

        public override void MouseUp(Collider2D[] hits)
        {
            if (hits != null)
            {
                foreach (Collider2D hit in hits)
                {
                    if (hit.TryGetComponent<DropSlot>(out var slot))
                    {
                        if (!slot.IsOnCreature)
                        {
                            SpawnAt(slot);
                            return;
                        }
                    }
                }
            }
        }


        private void SpawnAt(DropSlot slot)
        {
            GetComponentInParent<CardSlot>().UseCard();
            GameObject creature = Instantiate(SpawnPrefab, slot.transform.position, Quaternion.identity);
            creature.name = SpawnPrefab.name;
            creature.SetActive(true);
            creature.transform.SetParent(slot.transform);
            slot.IsOnCreature = true;
            slot.Creature = creature.GetComponent<CreatureStat>();
            slot.Creature.Initialize(GetComponentInParent<CardSlot>().characterData);
        }
    }
}