
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualScreenClicker : MonoBehaviour
{
    public Camera mainCamera; // Main Camera (1)
    public Camera uiCamera; // Game Camera
    public Collider monitorCollider;// Plane

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.collider == monitorCollider)
            {
                // Трансформируем глобальную точку клика в локальные координаты самого Plane
                Vector3 localHitPoint = monitorCollider.transform.InverseTransformPoint(hit.point);

                // Стандартный Plane в Unity имеет размер 10х10 единиц в плоскости XZ
                // Переводим локальные X и Z в диапазон от 0 до 1
                float normalizedX = localHitPoint.x + 5f; // из -5..5 делаем 0..10
                float normalizedY = localHitPoint.z + 5f; // Plane лежит в плоскости XZ, поэтому берем Z вместо Y

                normalizedX /= 10f; // Делаем диапазон 0..1
                normalizedY /= 10f;

                // Если картинка отзеркалена по горизонтали или вертикали, 
                // можно раскомментировать строчки ниже для инверсии:
                 normalizedX = 1f - normalizedX; 
                 normalizedY = 1f - normalizedY;

                // Переводим в пиксели виртуального экрана
                float virtualX = normalizedX * uiCamera.pixelWidth;
                float virtualY = normalizedY * uiCamera.pixelHeight;
                Debug.Log($"Клик в пикселях UI: X = {virtualX} (Ширина экрана: {uiCamera.pixelWidth}), Y = {virtualY} (Высота экрана: {uiCamera.pixelHeight})");

                Vector3 virtualMousePos = new Vector3(virtualX, virtualY, 0);

                // Отправляем клик в UI
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = virtualMousePos;

                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (RaycastResult result in results)
                {
                    GameObject target = result.gameObject;
                    Button button = target.GetComponentInParent<Button>();
                    if (button != null) target = button.gameObject;

                    ExecuteEvents.Execute(target, pointerData, ExecuteEvents.pointerClickHandler);
                    break;
                }
            }
        }
    }
}