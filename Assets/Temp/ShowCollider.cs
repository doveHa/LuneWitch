using UnityEngine;

public class ShowCollider : MonoBehaviour
{
    public Color color = new Color(0, 1, 0, 1); // 초록색 (원하는 색 변경 가능)

    private void OnDrawGizmos()
    {
        BoxCollider2D box2D = GetComponent<BoxCollider2D>();

        // 콜라이더가 없거나 컴포넌트가 꺼져있으면 그리지 않음
        if (box2D == null || !box2D.enabled) return;

        // 1. 색상 설정
        Gizmos.color = color;

        // 2. 오브젝트의 위치/회전/크기를 기즈모 매트릭스에 적용
        // 이렇게 하면 오브젝트가 회전해도 박스가 같이 회전합니다.
        Gizmos.matrix = transform.localToWorldMatrix;

        // 3. 2D 박스 그리기 (Offset과 Size 반영)
        // 로컬 좌표계 기준이므로 중심점은 box2D.offset, 크기는 box2D.size를 사용합니다.
        Gizmos.DrawWireCube(box2D.offset, box2D.size);

        // 4. 매트릭스 초기화 (다른 기즈모에 영향을 주지 않기 위해)
        Gizmos.matrix = Matrix4x4.identity;
    }
}