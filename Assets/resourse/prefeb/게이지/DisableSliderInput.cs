using UnityEngine;

using UnityEngine.EventSystems;

public class DisableSliderInput : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        eventData.Use(); // ���콺 Ŭ�� ����
    }

    public void OnDrag(PointerEventData eventData)
    {
        eventData.Use(); // �巡�׵� ����
    }
}

