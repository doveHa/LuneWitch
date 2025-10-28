using Script.Manager;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    [SerializeField] private GameObject moveSprite;
    public GameObject SpawnPrefab { private get; set; }
    private Vector3 offset, originalPos;
    private bool isDragging = false, canDrag = false;
    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;
    }

    public void SetDraggable(bool canDrag)
    {
        this.canDrag = canDrag;
    }


    void OnMouseDown()
    {
        if (!canDrag)
        {
            return;
        }

        isDragging = true;
        originalPos = moveSprite.transform.position;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = moveSprite.transform.position - (Vector3)mousePos;
        moveSprite.SetActive(true);
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        moveSprite.transform.position = mousePos + (Vector2)offset;
    }

    void OnMouseUp()
    {
        if (!isDragging) return;
        isDragging = false;

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.TryGetComponent<DropSlot>(out var slot))
        {
            if (!slot.IsOnCreature)
            {
                SpawnAt(slot);
            }
        }

        moveSprite.SetActive(false);
        moveSprite.transform.position = originalPos;
    }

    private void SpawnAt(DropSlot slot)
    {
        GetComponent<CardSlot>().UseCard();
        GameObject creature = Instantiate(SpawnPrefab, slot.transform.position, Quaternion.identity);
        creature.name = SpawnPrefab.name;
        creature.SetActive(true);
        creature.transform.SetParent(slot.transform);
        slot.IsOnCreature = true;
        canDrag = false;
    }
}