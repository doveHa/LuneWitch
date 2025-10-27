using System.Collections;
using Script.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    
    public int rewardGacha = 5;

    public void OnExitButtonClicked()
    {
        StartCoroutine(ExitAfterDelay());
    }

    private IEnumerator ExitAfterDelay()
    {
        // 1. ��ȭ ����
        
        PlayerData.GachaTokens += rewardGacha;

        // 2. 0.5�� ���
        yield return new WaitForSeconds(0.5f);

        // 3. ���� ȭ������ �̵�
        SceneLoadManager.Manager.LoadMainScene();
    }
}
