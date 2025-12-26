using System.Collections;
using Script.Manager;
using UnityEngine;

namespace Script.UI.ButtonOnClick
{
    public class DeckConfirmButton : Core.OnButtonClick.ButtonOnClick
    {
        [SerializeField] private GameObject sceneChangeEffect;

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