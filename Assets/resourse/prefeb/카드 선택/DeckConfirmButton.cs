using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeckConfirmButton : MonoBehaviour
{
    public GameObject transitionObject; // 활성화할 오브젝트
    public DeckSelectManager deckSelectManager; // 덱 선택 상태 확인용

    public void OnClick_DeckConfirm()
    {
        if (deckSelectManager != null && deckSelectManager.AreAllSlotsFilled())
        {
            // 1. 오브젝트 켜고 2. 씬 이동은 코루틴으로 딜레이
            StartCoroutine(HandleSceneTransition());
        }
        else
        {
            Debug.Log("카드 4개가 모두 선택되지 않았습니다.");
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        if (transitionObject != null)
            transitionObject.SetActive(true); // 오브젝트 켜기

        yield return new WaitForSeconds(0.5f); // 0.5초 기다리기

        string chapter = PlayerPrefs.GetString("SelectedChapter", "Chapter1");

        if (chapter == "Chapter1")
        {
            SceneManager.LoadScene("Chapter 1 Story");
        }
        else if (chapter == "Chapter2")
        {
            SceneManager.LoadScene("Chapter 2 Story");
        }
    }
}
