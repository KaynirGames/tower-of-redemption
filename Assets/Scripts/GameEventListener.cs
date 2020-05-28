using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent Event = null; // Игровое событие.
    [SerializeField] private UnityEvent Response = null; // Отклик на игровое событие.

    /// <summary>
    /// Осуществить отклик при возникновении игрового события.
    /// </summary>
    public void OnEventCalled()
    {
        Response.Invoke();
    }
    /// <summary>
    /// Подписаться на событие при активации объекта. 
    /// </summary>
    private void OnEnable()
    {
        Event.Subscribe(this);
    }
    /// <summary>
    /// Отписаться от события при деактивации объекта. 
    /// </summary>
    private void OnDisable()
    {
        Event.Unsubscribe(this);
    }
}
