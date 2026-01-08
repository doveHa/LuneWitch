using System.Collections.Generic;
using Script.BattleStyle.Handler;
using Script.UI.Pointer.Drag;
using Script.UI.Pointer.Hover;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
        private Image targetImage;
        private RectTransform rectTransform;
        private bool isDragging;
        private Canvas canvas;
        private Vector2 originalImageSize;

        void Awake()
        {
            rectTransform = target.GetComponent<RectTransform>();
            targetImage = target.GetComponent<Image>();
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
            if (TryGetComponent<CardHandler>(out var handler))
            {
                if (handler.IsUsed)
                {
                    return;
                }
            }

            if (CanDrag && TryGetComponent(out IDrag drag))
            {
                isDragging = true;
                originalPos = target.transform.position;
                offset = rectTransform.localPosition - GetMousePos(eventData);

                if (targetImage != null)
                {
                    originalImageSize = rectTransform.sizeDelta;
                    targetImage.SetNativeSize();
                }

                drag.Click(this, target);
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
                        drag.Drag(this, result.gameObject);
                        foundDropZone = true;
                        break;
                    }
                }

                if (!foundDropZone)
                {
                    drag.Drag(this, null);
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
            
            if (targetImage != null)
            {
                rectTransform.sizeDelta = originalImageSize;
            }
            
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (var result in results)
            {
                if (result.gameObject.transform.CompareTag("DropZone"))
                {
                    GetComponent<IDrag>().Drop(this, result.gameObject);
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
                rectTransform.parent as RectTransform,
                eventData.position,
                canvas.worldCamera,
                out Vector2 mousePos
            );
            return mousePos;
        }
    }
}