using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Script.Manager
{
    public class KeyInputManager : ManagerBase<KeyInputManager>
    {
        private KeyInput _keyInput;
        [SerializeField] private GameObject escPanel;

        protected override void Awake()
        {
            base.Awake();
            _keyInput = new KeyInput();
        }

        void Start()
        {
            _keyInput.Input.Enable();
            _keyInput.Input.Exit.performed += ToggleESCPanel;
        }

        private void ToggleESCPanel(InputAction.CallbackContext ctx)
        {
            escPanel.SetActive(!escPanel.activeSelf);
        }
    }
}