using Script.Card;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour
{
    [SerializeField] private GameObject moveSprite;

    private Vector3 offset, originalPos;
    private bool isDragging = false, canDrag = true; //canDrag 초기 값은 false
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
        Debug.Log("DraggableObject");
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
        Collider2D[] hit = Physics2D.OverlapPointAll(mousePos);

        moveSprite.GetComponent<IMouseUp>().MouseUp(hit);

        canDrag = true;
        moveSprite.SetActive(false);
        moveSprite.transform.position = originalPos;
    }
}