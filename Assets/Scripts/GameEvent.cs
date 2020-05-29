using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameEvent", menuName = "Scriptable Objects/Game Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<GameEventListener> subscribers = new List<GameEventListener>(); // Список подписчиков на игровое событие.

    /// <summary>
    /// Уведомить подписчиков о возникновении игрового события.
    /// </summary>
    public void NotifyEventSubs()
    {
        foreach (GameEventListener sub in subscribers)
        {
            sub.OnEventCalled();
        }
    }
    /// <summary>
    /// Добавить слушателя в список подписчиков.
    /// </summary>
    /// <param name="listener">Слушатель игрового события.</param>
    public void Subscribe(GameEventListener listener)
    {
        if (!subscribers.Contains(listener))
        {
            subscribers.Add(listener);
        }
    }
    /// <summary>
    /// Убрать слушателя из списка подписчиков.
    /// </summary>
    /// <param name="listener">Слушатель игрового события.</param>
    public void Unsubscribe(GameEventListener listener)
    {
        if (subscribers.Contains(listener))
        {
            subscribers.Remove(listener);
        }
    }
}
