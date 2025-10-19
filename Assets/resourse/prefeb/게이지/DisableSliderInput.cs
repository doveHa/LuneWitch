using UnityEngine;

using UnityEngine.EventSystems;

public class DisableSliderInput : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.Use(); // 마우스 클릭 무시
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.Use(); // 드래그도 무시
    }
}

