using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class SimpleClickBlocker : MonoBehaviour, IPointerClickHandler
{
    public GameObject tutorialPanel;  // 튜토리얼 오브젝트 참조

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tutorialPanel != null && tutorialPanel.activeSelf)
        {
            Debug.Log("튜토리얼 켜져있어서 클릭 무시");
            return;
        }

        CustomEvent.Trigger(gameObject, "OnCustomClick");
    }
}
