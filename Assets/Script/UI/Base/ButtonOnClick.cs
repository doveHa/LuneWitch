using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Base
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