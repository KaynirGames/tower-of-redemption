using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int width = 20; // Ширина комнаты.
    [SerializeField] private int height = 12; // Высота комнаты.
    [SerializeField] private GameObject roomEnvironment = null; // Объекты в комнате.
    [SerializeField] private RoomType roomType = null; // Тип комнаты.
    [SerializeField] private GameEvent onRoomLoaded = null; // Cобытие при загрузке комнаты.
    [SerializeField] private GameEvent onActiveRoomChanged = null; // Cобытие при смене активной комнаты.
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.

    /// <summary>
    /// Позиция комнаты в сетке координат подземелья.
    /// </summary>
    public Vector2Int DungeonGridPosition { get; set; }
    /// <summary>
    /// Тип комнаты.
    /// </summary>
    public RoomType RoomType => roomType;
    /// <summary>
    /// Присвоено ли комнате место на этаже подземелья?
    /// </summary>
    public bool IsInitialized { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool IsActiveRoom { get; set; }

    private void Start()
    {
        IsInitialized = false;
        IsActiveRoom = false;

        loadedStageRooms.Add(this);
        onRoomLoaded.NotifyEventSubs();
    }
    /// <summary>
    /// Устанавливает глобальную позицию комнаты на основной сцене.
    /// </summary>
    public void SetWorldPosition()
    {
        Vector3 worldPos = new Vector3(DungeonGridPosition.x * width, DungeonGridPosition.y * height, 0);
        transform.position = worldPos;
        roomEnvironment.transform.position = worldPos;
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(DungeonGridPosition.x * width, DungeonGridPosition.y * height, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsActiveRoom = true;
            onActiveRoomChanged.NotifyEventSubs();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IsActiveRoom = false;
        }
    }

    private void OnDestroy()
    {
        loadedStageRooms.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
