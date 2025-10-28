using UnityEngine;
using System.Collections;

public class CallPauseOnEnable : MonoBehaviour
{
    [SerializeField] private PauseTaggedObjects pauseScript;

    void OnEnable()
    {
        StartCoroutine(WaitAndPause());
    }

    IEnumerator WaitAndPause()
    {
        yield return null; // �� ������ ���

        if (pauseScript == null)
        {
            GameObject controller = GameObject.Find("Game Over Panel");

            if (controller != null && controller.activeInHierarchy)
            {
                pauseScript = controller.GetComponent<PauseTaggedObjects>();
            }
        }

        if (pauseScript != null && pauseScript.gameObject.activeInHierarchy)
        {
            pauseScript.Pause();
        }
        else
        {
            Debug.LogWarning(
                $"[CallPauseOnEnable] PauseTaggedObjects�� ã�� ���߰ų� ��Ȱ��ȭ �����Դϴ�. (���� ������Ʈ: {gameObject.name})");
        }
    }
}