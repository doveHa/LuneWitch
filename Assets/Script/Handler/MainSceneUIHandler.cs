using System.Collections;
using UnityEngine;

public class MainSceneUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject topUI;
    [SerializeField] private GameObject rightButtonLayout;
    [SerializeField] private GameObject characterDialogueSet;
    
    void Start()
    {
        StartCoroutine(UIMove());
    }

    private IEnumerator UIMove()
    {
        yield return new WaitForSeconds(1f);
        topUI.GetComponent<Animator>().SetTrigger("TopUIDown");
        rightButtonLayout.GetComponent<Animator>().SetTrigger("RightButtonLayout");
        characterDialogueSet.GetComponent<Animator>().SetTrigger("CharacterDialogueSet");
    }

    public void ShowPanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    
}