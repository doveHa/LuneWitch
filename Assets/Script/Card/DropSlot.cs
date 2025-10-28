using Script.Creature;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DropSlot : MonoBehaviour
{
    public bool IsOnCreature { get; set; }
    
    public CreatureStat Creature { get; set; }

    void Update()
    {
    }
    private void OnDrawGizmos()
    {
        // 슬롯이 보이도록 표시용
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.3f, 1.3f, 0));
    }
}