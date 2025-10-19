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
        yield return null; // 한 프레임 대기

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
            Debug.LogWarning($"[CallPauseOnEnable] PauseTaggedObjects를 찾지 못했거나 비활성화 상태입니다. (실행 오브젝트: {gameObject.name})");
        }
    }
}
