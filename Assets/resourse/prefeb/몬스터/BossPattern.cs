using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class BossPattern : MonoBehaviour
{
    [Header("공격 간격 설정")]
    public float attack1Interval = 3f;
    public float attack2Interval = 5f;
    public float attack3RandomInterval = 8f;

    [Header("공격 소요 시간")]
    public float attack1Duration = 2f; // 공격 1은 2초
    public float attackOtherDuration = 1f; // 공격 2, 3은 1초

    private float attack1Timer = 0f;
    private float attack2Timer = 0f;
    private float attack3Timer = 0f;

    private bool isPlayerInRange = false;
    private bool isAttacking = false;

    void Update()
    {
        if (!isPlayerInRange || isAttacking) return;

        attack1Timer += Time.deltaTime;
        attack2Timer += Time.deltaTime;
        attack3Timer += Time.deltaTime;

        // 공격 1: 주기적
        if (attack1Timer >= attack1Interval)
        {
            StartCoroutine(PerformAttack("Attack1"));
            return;
        }

        // 공격 2: 주기적
        if (attack2Timer >= attack2Interval)
        {
            StartCoroutine(PerformAttack("Attack2"));
            return;
        }

        // 공격 3: 랜덤
        if (attack3Timer >= attack3RandomInterval)
        {
            if (Random.Range(0, 2) == 0) // 50% 확률
            {
                StartCoroutine(PerformAttack("Attack3"));
                return;
            }

            // 실패해도 타이머 초기화
            attack3Timer = 0f;
        }
    }

    IEnumerator PerformAttack(string eventName)
    {
        isAttacking = true;

        TriggerBoltAttack(eventName);

        // 공격별 소요 시간 지정
        float delay = (eventName == "Attack1") ? attack1Duration : attackOtherDuration;

        yield return new WaitForSeconds(delay);

        isAttacking = false;

        // 공격 타이머 초기화
        if (eventName == "Attack1") attack1Timer = 0f;
        else if (eventName == "Attack2") attack2Timer = 0f;
        else if (eventName == "Attack3") attack3Timer = 0f;
    }

    void TriggerBoltAttack(string eventName)
    {
        CustomEvent.Trigger(gameObject, eventName);
        Debug.Log("Triggered: " + eventName);
    }

    // ▶ 외부에서 호출 (TriggerDetector가 호출)
    public void SetPlayerInRange(bool isInRange)
    {
        isPlayerInRange = isInRange;
    }
}
