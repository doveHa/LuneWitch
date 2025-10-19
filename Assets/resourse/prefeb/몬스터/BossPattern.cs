using UnityEngine;
using Unity.VisualScripting;
using System.Collections;

public class BossPattern : MonoBehaviour
{
    [Header("���� ���� ����")]
    public float attack1Interval = 3f;
    public float attack2Interval = 5f;
    public float attack3RandomInterval = 8f;

    [Header("���� �ҿ� �ð�")]
    public float attack1Duration = 2f; // ���� 1�� 2��
    public float attackOtherDuration = 1f; // ���� 2, 3�� 1��

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

        // ���� 1: �ֱ���
        if (attack1Timer >= attack1Interval)
        {
            StartCoroutine(PerformAttack("Attack1"));
            return;
        }

        // ���� 2: �ֱ���
        if (attack2Timer >= attack2Interval)
        {
            StartCoroutine(PerformAttack("Attack2"));
            return;
        }

        // ���� 3: ����
        if (attack3Timer >= attack3RandomInterval)
        {
            if (Random.Range(0, 2) == 0) // 50% Ȯ��
            {
                StartCoroutine(PerformAttack("Attack3"));
                return;
            }

            // �����ص� Ÿ�̸� �ʱ�ȭ
            attack3Timer = 0f;
        }
    }

    IEnumerator PerformAttack(string eventName)
    {
        isAttacking = true;

        TriggerBoltAttack(eventName);

        // ���ݺ� �ҿ� �ð� ����
        float delay = (eventName == "Attack1") ? attack1Duration : attackOtherDuration;

        yield return new WaitForSeconds(delay);

        isAttacking = false;

        // ���� Ÿ�̸� �ʱ�ȭ
        if (eventName == "Attack1") attack1Timer = 0f;
        else if (eventName == "Attack2") attack2Timer = 0f;
        else if (eventName == "Attack3") attack3Timer = 0f;
    }

    void TriggerBoltAttack(string eventName)
    {
        CustomEvent.Trigger(gameObject, eventName);
        Debug.Log("Triggered: " + eventName);
    }

    // �� �ܺο��� ȣ�� (TriggerDetector�� ȣ��)
    public void SetPlayerInRange(bool isInRange)
    {
        isPlayerInRange = isInRange;
    }
}
