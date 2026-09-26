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

            // Если оси инвертированы на новой модели, раскомментируй:
            // uv.x = 1f - uv.x; 
            // uv.y = 1f - uv.y; 

            float virtualX = uv.x * uiCamera.pixelWidth;
            float virtualY = uv.y * uiCamera.pixelHeight;
            pointerData.position = new Vector2(virtualX, virtualY);

            // Имитируем движение мыши для EventSystem
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            // Используем стандартный механизм Unity для обработки Hover (наведения)
            // Это заставит Event Trigger на любом объекте думать, что это реальная мышь
            pointerData.pointerCurrentRaycast = results.Count > 0 ? results[0] : new RaycastResult();

            // Заставляем Unity обработать Enter/Exit для всех элементов в иерархии под курсором
            HandlePointerExitAndEnter(pointerData, results.Count > 0 ? results[0].gameObject : null);

            // Обработка клика
            if (Input.GetMouseButtonDown(0) && results.Count > 0)
            {
                GameObject target = results[0].gameObject;

                // Проверяем обычную кнопку
                Button button = target.GetComponentInParent<Button>();
                if (button != null) target = button.gameObject;

                ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerClickHandler);
            }
        }
        else
        {
            // Если убрали мышь с монитора, очищаем все наведения
            HandlePointerExitAndEnter(pointerData, null);
        }
    }

    private void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject currentTarget)
    {
        // Функция полностью имитирует системный проход мыши по кнопкам,
        // дергая Event Trigger (Pointer Enter / Pointer Exit) у любого объекта автоматичеки
        if (currentTarget == null)
        {
            for (int i = 0; i < hoveredObjects.Count; i++)
            {
                ExecuteEvents.Execute(hoveredObjects[i], currentPointerData, ExecuteEvents.pointerExitHandler);
            }
            hoveredObjects.Clear();
            return;
        }

        if (hoveredObjects.Count > 0 && hoveredObjects[0] == currentTarget) return;

        // Очищаем старые
        for (int i = 0; i < hoveredObjects.Count; i++)
        {
            ExecuteEvents.Execute(hoveredObjects[i], currentPointerData, ExecuteEvents.pointerExitHandler);
        }
        hoveredObjects.Clear();

        // Добавляем новые элементы и всю их иерархию родителей (важно для Event Trigger)
        GameObject t = currentTarget;
        while (t != null)
        {
            hoveredObjects.Add(t);
            ExecuteEvents.Execute(t, currentPointerData, ExecuteEvents.pointerEnterHandler);
            t = t.transform.parent != null ? t.transform.parent.gameObject : null;
        }
    }
}
