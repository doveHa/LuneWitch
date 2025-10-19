using UnityEngine;

public class BookCollision : MonoBehaviour
{
    // å ������Ʈ�� Collider�� "Is Trigger" üũ�Ǿ� �־�� ��
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player�� å�� �����߽��ϴ�: " + other.name);

            // ��: �÷��̾� ������Ʈ ����
            Destroy(other.gameObject);

            // �ʿ�� �߰� �۾� ���� (���� ����, ȿ�� ��)
        }
    }
}
