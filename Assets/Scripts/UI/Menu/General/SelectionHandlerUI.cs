using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionHandlerUI : MonoBehaviour, IPointerDownHandler, IDeselectHandler
{
    public static event Action OnSelectionCancel = delegate { };

    [SerializeField] private bool _isButton = true;

    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = EventSystem.current;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!_eventSystem.IsPointerOverGameObject())
        {
            OnSelectionCancel();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isButton)
        {
            _eventSystem.SetSelectedGameObject(gameObject);
        }
    }
}