using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    
    public int rewardGacha = 5;

    public void OnExitButtonClicked()
    {
        // 0.5초 후에 실행되는 코루틴 시작
        StartCoroutine(ExitAfterDelay());
    }

    private IEnumerator ExitAfterDelay()
    {
        // 1. 재화 지급
        
        PlayerData.GachaTokens += rewardGacha;

        // 2. 0.5초 대기
        yield return new WaitForSeconds(0.5f);

        // 3. 메인 화면으로 이동
        SceneManager.LoadScene("Menu"); // 씬 이름에 맞게 변경
    }
}
