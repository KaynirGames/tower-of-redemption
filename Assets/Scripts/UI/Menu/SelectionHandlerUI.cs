using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandlerUI : MonoBehaviour, IPointerDownHandler, IDeselectHandler
{
    public delegate void OnSelectionCursorCall(Vector3 position);

    public static event OnSelectionCursorCall OnCursorCall = delegate { };
    public static event Action OnSelectionCancel = delegate { };

    public void OnPointerDown(PointerEventData eventData)
    {
        OnCursorCall(transform.position);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            OnSelectionCancel();
        }
    }
}