using System.Collections.Generic;
using Script.UI.Pointer.Drag;
using Script.UI.Pointer.Hover;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.UI.Pointer
{
    [RequireComponent(typeof(Collider2D))]
    public class PointerHandler : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject target;

        public bool CanDrag { get; set; }

        private Vector3 offset, originalPos;
        private RectTransform rectTransform;
        private bool isDragging;
        private Canvas canvas;

        void Awake()
        {
            rectTransform = target.GetComponent<RectTransform>();
            canvas = GetComponentInParent<Canvas>();
            CanDrag = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isDragging)
            {
                return;
            }

            if (TryGetComponent(out IHover hover))
            {
                hover.Enter();
            }
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            if (TryGetComponent(out IHover hover))
            {
                hover.Exit();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (CanDrag && TryGetComponent(out IDrag drag))
            {
                isDragging = true;
                originalPos = target.transform.position;
                offset = rectTransform.localPosition - GetMousePos(eventData);

                drag.Click(target);
            }
            else
            {
                Debug.Log("IDrag Not Exist");
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging && TryGetComponent(out IDrag drag))
            {
                rectTransform.localPosition = GetMousePos(eventData) + offset;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);

                bool foundDropZone = false;

                foreach (var result in results)
                {
                    if (result.gameObject.transform.CompareTag("DropZone"))
                    {
                        drag.Drag(result.gameObject);
                        foundDropZone = true;
                        break;
                    }
                }

                if (!foundDropZone)
                {
                    drag.Drag(null);
                }
            }
            else
            {
                Debug.Log("IDrag Not Exist");
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!isDragging)
            {
                return;
            }

            isDragging = false;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.transform.CompareTag("DropZone"))
                {
                    GetComponent<IDrag>().Drop(result.gameObject);
                    MoveOriginalSpot();
                    return;
                }
            }

            MoveOriginalSpot();
        }

        public void OnlyClick()
        {
            isDragging = false;
            MoveOriginalSpot();
        }

        public void MoveOriginalSpot()
        {
            target.transform.position = originalPos;
        }

        private Vector3 GetMousePos(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 mousePos
            );
            return mousePos;
        }
    }
}