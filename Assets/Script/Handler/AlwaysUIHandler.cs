using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Handler
{
    public class AlwaysUIHandler : MonoBehaviour
    {
        [SerializeField] private GameObject exitMenu, optionMenu;
        private KeyInput _keyInput;
        private Canvas _canvas;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _keyInput = new KeyInput();
            _keyInput.Input.Enable();
            _keyInput.Input.Exit.performed += ShowExitMenu;
            
            _canvas = GetComponent<Canvas>();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // 새로 로드된 씬의 메인 카메라를 찾아서 Canvas에 다시 연결
            if (_canvas != null)
            {
                _canvas.worldCamera = Camera.main;
            }
        }
        
        public void ShowOptionMenu()
        {
            optionMenu.SetActive(true);
        }

        public void CloseOptionMenu()
        {
            optionMenu.SetActive(false);
        }
        
        private void ShowExitMenu(InputAction.CallbackContext ctx)
        {
            exitMenu.SetActive(!exitMenu.activeInHierarchy);
        }

        public void CloseExitMenu()
        {
            exitMenu.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}