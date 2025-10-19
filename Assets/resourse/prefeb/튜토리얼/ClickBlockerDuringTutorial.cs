using UnityEngine;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class SimpleClickBlocker : MonoBehaviour, IPointerClickHandler
{
    public GameObject tutorialPanel;  // Ʃ�丮�� ������Ʈ ����

    public void OnPointerClick(PointerEventData eventData)
    {
        if (tutorialPanel != null && tutorialPanel.activeSelf)
        {
            Debug.Log("Ʃ�丮�� �����־ Ŭ�� ����");
            return;
        }

        CustomEvent.Trigger(gameObject, "OnCustomClick");
    }
}
