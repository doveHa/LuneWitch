using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueLogManager : MonoBehaviour
{
    public GameObject logPanel;                  // ��ȭ �α� ��ü �г�
    public Transform contentParent;              // �α� �׸���� �� Content
    public GameObject logItemPrefab;             // �α� �׸� ������
    public ScrollRect scrollRect;                // ��ũ�Ѻ� ������Ʈ

    public int maxLogCount = 35;                 // �ִ� �α� �� ����

    private Queue<GameObject> logItems = new Queue<GameObject>(); // �α� ������ ����

    private string currentSpeakerName;
    private string currentDialogue;

    // �ܺο��� �̸� ����
    public void SetSpeakerName(string name)
    {
        currentSpeakerName = name;
    }

    // �ܺο��� ��� �ؽ�Ʈ ����
    public void SetDialogue(string dialogue)
    {
        currentDialogue = dialogue;
    }

    // ��� ������ ���� (Bolt���� ȣ��)
    public void ApplyDialogue()
    {
        if (string.IsNullOrEmpty(currentDialogue))
        {
            Debug.LogWarning("��簡 ����ֽ��ϴ�.");
            return;
        }

        GameObject newItem = Instantiate(logItemPrefab, contentParent);
        DialogueLogItem logItem = newItem.GetComponent<DialogueLogItem>();
        logItem.SetData(currentSpeakerName, currentDialogue);

        logItems.Enqueue(newItem);

        // �ִ� �� �ʰ� �� ������ �α� ����
        if (logItems.Count > maxLogCount)
        {
            GameObject oldItem = logItems.Dequeue();
            Destroy(oldItem);
        }

        StartCoroutine(ScrollToBottom());

        // ���� �ʱ�ȭ
        currentSpeakerName = null;
        currentDialogue = null;
    }

    // ��ũ���� ���� �Ʒ��� ����
    private IEnumerator ScrollToBottom()
    {
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentParent.GetComponent<RectTransform>());
        Canvas.ForceUpdateCanvases();

        if (scrollRect != null)
        {
            scrollRect.verticalNormalizedPosition = 0f;
        }
        else
        {
            Debug.LogWarning("ScrollRect�� ������� �ʾҽ��ϴ�.");
        }
    }

    // ��ȭ �α�â ���� �ݱ�
    public void ToggleLog()
    {
        bool isActive = !logPanel.activeSelf;
        logPanel.SetActive(isActive);

        if (isActive)
        {
            StartCoroutine(ScrollToBottom());
        }
    }

    // �α� �ʱ�ȭ (�� ��ȯ ��� ȣ�� ����)
    public void ClearLog()
    {
        foreach (var item in logItems)
        {
            Destroy(item);
        }
        logItems.Clear();
    }
}
