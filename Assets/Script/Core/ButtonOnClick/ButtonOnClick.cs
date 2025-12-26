using UnityEngine;
using UnityEngine.UI;

namespace Script.Core.OnButtonClick
{
    public abstract class ButtonOnClick : MonoBehaviour
    {
        protected void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        protected abstract void OnClick();
    }
}