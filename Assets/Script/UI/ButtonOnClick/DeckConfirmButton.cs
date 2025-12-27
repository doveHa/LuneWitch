using System.Collections;
using Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.ButtonOnClick
{
    [RequireComponent(typeof(Button))]
    public class DeckConfirmButton : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject sceneChangeEffect;

        private Button _targetButton;

        private void Start()
        {
            _targetButton = GetComponent<Button>();
        }

        private void Update()
        {
            if(_targetButton != null)
            {
                _targetButton.interactable = PlayerManager.Manager.IsAllCardSelected();
            }
        }

        protected override void OnClick()
        {
            if (PlayerManager.Manager.IsAllCardSelected())
            {
                StartCoroutine(HandleSceneTransition());
            }
        }

        private IEnumerator HandleSceneTransition()
        {
            sceneChangeEffect.SetActive(true);

            yield return new WaitForSeconds(0.5f);

            SceneLoadManager.Manager.LoadChapter();
        }
    }
}