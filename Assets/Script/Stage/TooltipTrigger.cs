using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject tooltipPanel;

    protected string tooltipText;
    private bool isTooltipActive;

    private void Awake()
    {
        tooltipPanel.SetActive(false);
    }

    void Update()
    {
        if (isTooltipActive)
        {
            AdjustTooltipText();
        }
    }

    protected abstract void SetTooltipText();

    private void AdjustTooltipText()
    {
        SetTooltipText();
        
        if (tooltipPanel.transform.GetChild(0).TryGetComponent(out TextMeshProUGUI textUGUI))
        {
            textUGUI.text = tooltipText;
        }
        else if (tooltipPanel.transform.GetChild(0).TryGetComponent(out TextMeshPro text))
        {
            text.text = tooltipText;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isTooltipActive = true; 
        AdjustTooltipText();
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isTooltipActive = false; 
        tooltipPanel.SetActive(false);
    }
}