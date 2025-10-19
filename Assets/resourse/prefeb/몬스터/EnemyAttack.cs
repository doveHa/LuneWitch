using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // 볼트에서 호출할 함수
    public void TryAttack()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Attack();
        }
    }

    private void Attack()
    {
        Debug.Log("공격한다!");
        // 실제 공격 로직 작성
    }
}
