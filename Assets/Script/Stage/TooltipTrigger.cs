using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltipPanel;

    protected string tooltipText;

    private void Awake()
    {
        tooltipPanel.SetActive(false);
    }

    void Start()
    {
    }

    protected abstract void SetTooltipText();
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetTooltipText();
        
        if (tooltipPanel.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI textUGUI))
        {
            textUGUI.text = tooltipText;
        }else if (tooltipPanel.transform.GetChild(0).TryGetComponent(out TextMeshPro text))
        {
            text.text = tooltipText;
        }
        
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}