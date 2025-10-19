using Script.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Handler
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject optionMenu, exitMenu;

        private KeyInput _keyInput;

        void Start()
        {
            _keyInput = new KeyInput();
            _keyInput.Input.Enable();
            _keyInput.Input.Exit.performed += ShowExitMenu;
            _keyInput.TitleScene.Enable();
            _keyInput.TitleScene.NextScene.performed += SceneChange;
        }

        public void ShowOptionMenu()
        {
            _keyInput.TitleScene.Disable();
            optionMenu.SetActive(true);
        }

        public void CloseOptionMenu()
        {
            _keyInput.TitleScene.Enable();
            optionMenu.SetActive(false);
        }

        public void CloseExitMenu()
        {
            exitMenu.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void ShowExitMenu(InputAction.CallbackContext ctx)
        {
            exitMenu.SetActive(!exitMenu.activeInHierarchy);
        }

        private void SceneChange(InputAction.CallbackContext ctx)
        {
            if (!IsPointerOverUI())
            {
                SceneLoadManager.Manager.LoadMainScene();
            }

            _keyInput.Disable();
        }

        private bool IsPointerOverUI()
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue()
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);
            return results.Count > 0;
        }
    }
}