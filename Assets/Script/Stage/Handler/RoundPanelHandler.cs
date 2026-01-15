using Script.UI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Stage.Handler
{
    public class RoundPanelHandler : MonoBehaviour
    {
        public void RoundPanelActive()
        {
            foreach (FadeInOutHandler handler in GetComponentsInChildren<FadeInOutHandler>(true))
            {
                if (handler.TryGetComponent(out Image image))
                {
                    Color color = image.color;
                    color.a = 1;
                    image.color = color;
                }
                else if (TryGetComponent(out TextMeshProUGUI textMesh))
                {
                    Color color = textMesh.color;
                    color.a = 1;
                    textMesh.color = color;
                }
            }

            GetComponent<Animator>().SetTrigger("ReStart");

            
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
                foreach (Transform child2 in child)
                {
                    child2.gameObject.SetActive(true);
                }
            }
        }
    }
}