using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // ��Ʈ���� ȣ���� �Լ�
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
        Debug.Log("�����Ѵ�!");
        // ���� ���� ���� �ۼ�
    }
}
