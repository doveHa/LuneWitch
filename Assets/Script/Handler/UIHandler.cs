using Script.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SceneClickHandler : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
        {
            // UI 위를 클릭했다면 return
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            SceneLoadManager.Manager.LoadMainScene();
        }
    }
}