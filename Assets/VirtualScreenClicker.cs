using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class VirtualScreenClicker : MonoBehaviour
{
    public Camera mainCamera;       // Main Camera (1)
    public Camera uiCamera;         // Game Camera
    public Collider monitorCollider;// Коллайдер экрана

    private PointerEventData pointerData;
    private List<GameObject> hoveredObjects = new List<GameObject>();
    private GameObject draggingObject;

    void Start()
    {
        pointerData = new PointerEventData(EventSystem.current);
    }

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.collider == monitorCollider)
        {
            Vector2 uv = hit.textureCoord;

            // Если оси инвертированы на модели, раскомментируй:
            // uv.x = 1f - uv.x; 
            // uv.y = 1f - uv.y; 

            float virtualX = uv.x * uiCamera.pixelWidth;
            float virtualY = uv.y * uiCamera.pixelHeight;
            Vector2 newPosition = new Vector2(virtualX, virtualY);

            if (hoveredObjects.Count == 0)
            {
                pointerData.position = newPosition;
            }

            pointerData.delta = newPosition - pointerData.position;
            pointerData.position = newPosition;

            // Собираем элементы интерфейса под виртуальной мышью
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            // Записываем результат рейкаста в PointerEventData
            if (raycastResults.Count > 0)
            {
                pointerData.pointerCurrentRaycast = raycastResults[0];
            }
            else
            {
                pointerData.pointerCurrentRaycast = new RaycastResult();
            }

            // Обрабатываем наведение (Hover), берём первый объект из списка результатов
            GameObject currentTarget = raycastResults.Count > 0 ? raycastResults[0].gameObject : null;
            HandlePointerExitAndEnter(pointerData, currentTarget);

            // 1. Нажатие мыши (PointerDown)
            if (Input.GetMouseButtonDown(0) && raycastResults.Count > 0)
            {
                GameObject target = raycastResults[0].gameObject;

                pointerData.pressPosition = newPosition;
                pointerData.pointerPressRaycast = pointerData.pointerCurrentRaycast;

                pointerData.pointerPress = ExecuteEvents.GetEventHandler<IPointerDownHandler>(target);

                if (pointerData.pointerPress != null)
                {
                    ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerDownHandler);
                }

                pointerData.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(target);
                if (pointerData.pointerDrag != null)
                {
                    ExecuteEvents.Execute(pointerData.pointerDrag, pointerData, ExecuteEvents.initializePotentialDrag);
                    ExecuteEvents.Execute(pointerData.pointerDrag, pointerData, ExecuteEvents.beginDragHandler);
                    pointerData.dragging = true;
                    draggingObject = pointerData.pointerDrag;
                }
            }

            // 2. Удерживание и движение (Drag)
            if (Input.GetMouseButton(0) && draggingObject != null)
            {
                ExecuteEvents.Execute(draggingObject, pointerData, ExecuteEvents.dragHandler);
            }

            // 3. Отпускание мыши (PointerUp)
            if (Input.GetMouseButtonUp(0))
            {
                if (pointerData.pointerPress != null)
                {
                    ExecuteEvents.Execute(pointerData.pointerPress, pointerData, ExecuteEvents.pointerUpHandler);

                    GameObject target = raycastResults.Count > 0 ? raycastResults[0].gameObject : null;
                    if (target != null)
                    {
                        GameObject clickHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(target);
                        if (clickHandler != null)
                        {
                            ExecuteEvents.Execute(clickHandler, pointerData, ExecuteEvents.pointerClickHandler);
                        }
                    }
                }

                if (draggingObject != null)
                {
                    ExecuteEvents.Execute(draggingObject, pointerData, ExecuteEvents.endDragHandler);
                    draggingObject = null;
                }

                pointerData.pointerPress = null;
                pointerData.pointerDrag = null;
                pointerData.dragging = false;
            }
        }
        else
        {
            HandlePointerExitAndEnter(pointerData, null);
            if (Input.GetMouseButtonUp(0) || !Input.GetMouseButton(0))
            {
                if (draggingObject != null)
                {
                    ExecuteEvents.Execute(draggingObject, pointerData, ExecuteEvents.endDragHandler);
                    draggingObject = null;
                }
                pointerData.pointerPress = null;
                pointerData.pointerDrag = null;
                pointerData.dragging = false;
            }
        }
    }

    private void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject currentTarget)
    {
        if (currentTarget == null)
        {
            for (int i = 0; i < hoveredObjects.Count; i++)
            {
                if (hoveredObjects[i] != null)
                    ExecuteEvents.Execute(hoveredObjects[i], currentPointerData, ExecuteEvents.pointerExitHandler);
            }
            hoveredObjects.Clear();
            return;
        }

        if (hoveredObjects.Count > 0 && hoveredObjects[0] == currentTarget) return;

        for (int i = 0; i < hoveredObjects.Count; i++)
        {
            if (hoveredObjects[i] != null)
                ExecuteEvents.Execute(hoveredObjects[i], currentPointerData, ExecuteEvents.pointerExitHandler);
        }
        hoveredObjects.Clear();

        GameObject t = currentTarget;
        while (t != null)
        {
            hoveredObjects.Add(t);
            ExecuteEvents.Execute(t, currentPointerData, ExecuteEvents.pointerEnterHandler);
            t = t.transform.parent != null ? t.transform.parent.gameObject : null;
        }
    }
}
