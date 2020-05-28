using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    [SerializeField] private int width = 20; // Ширина комнаты.
    [SerializeField] private int height = 12; // Высота комнаты.
    [SerializeField] private RoomType roomType = null; // Тип комнаты.
    [SerializeField] private GameEvent onRoomLoaded = null; // Cобытие при загрузке комнаты.
    
    /// <summary>
    /// Позиция комнаты в локальной сетке координат подземелья.
    /// </summary>
    public Vector2Int LocalGridPosition { get; set; }

    /// <summary>
    /// Тип комнаты.
    /// </summary>
    public RoomType RoomType => roomType;

    private void Start()
    {
        onRoomLoaded.NotifyEventSubs();
    }

    /// <summary>
    /// Устанавливает глобальную позицию комнаты на сцене.
    /// </summary>
    public void SetWorldPosition()
    {
        transform.position = new Vector3(LocalGridPosition.x * width, LocalGridPosition.y * height, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
