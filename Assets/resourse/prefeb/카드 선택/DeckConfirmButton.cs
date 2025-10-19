using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeckConfirmButton : MonoBehaviour
{
    public GameObject transitionObject; // Ȱ��ȭ�� ������Ʈ
    public DeckSelectManager deckSelectManager; // �� ���� ���� Ȯ�ο�

    public void OnClick_DeckConfirm()
    {
        if (deckSelectManager != null && deckSelectManager.AreAllSlotsFilled())
        {
            // 1. ������Ʈ �Ѱ� 2. �� �̵��� �ڷ�ƾ���� ������
            StartCoroutine(HandleSceneTransition());
        }
        else
        {
            Debug.Log("ī�� 4���� ��� ���õ��� �ʾҽ��ϴ�.");
        }
    }

    private IEnumerator HandleSceneTransition()
    {
        if (transitionObject != null)
            transitionObject.SetActive(true); // ������Ʈ �ѱ�

        yield return new WaitForSeconds(0.5f); // 0.5�� ��ٸ���

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
