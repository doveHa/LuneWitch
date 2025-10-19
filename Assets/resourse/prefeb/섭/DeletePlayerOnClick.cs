using UnityEngine;

public class BookCollision : MonoBehaviour
{
    // 책 오브젝트는 Collider에 "Is Trigger" 체크되어 있어야 함
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player와 책이 접촉했습니다: " + other.name);

            // 예: 플레이어 오브젝트 삭제
            Destroy(other.gameObject);

            // 필요시 추가 작업 가능 (점수 증가, 효과 등)
        }
    }
}
