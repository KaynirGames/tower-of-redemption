using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int width = 20; // Ширина комнаты.
    [SerializeField] private int height = 12; // Высота комнаты.
    [SerializeField] private GameObject roomEnvironment = null; // Объекты в комнате.
    [SerializeField] private RoomTypeData roomTypeData = null; // Данные о типе комнаты.
    [SerializeField] private GameEvent onRoomLoaded = null; // Cобытие при загрузке комнаты.
    [SerializeField] private GameEvent onActiveRoomChanged = null; // Cобытие при смене активной комнаты.
    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.
    [SerializeField] private RoomRuntimeSet activeRoom = null; // Активная комната, где находится игрок.

    /// <summary>
    /// Позиция комнаты в сетке координат подземелья.
    /// </summary>
    public Vector2Int DungeonGridPosition { get; set; }
    /// <summary>
    /// Данные о типе комнаты.
    /// </summary>
    public RoomTypeData RoomTypeData => roomTypeData;
    /// <summary>
    /// Присвоено ли комнате место на этаже подземелья?
    /// </summary>
    public bool IsInitialized { get; set; }

    private void Start()
    {
        IsInitialized = false;

        if (roomTypeData.RoomType == RoomType.Start)
            activeRoom.Add(this);

        loadedStageRooms.Add(this);
        onRoomLoaded.NotifyEventSubs();
    }

    /// <summary>
    /// Установить глобальную позицию комнаты на основной сцене.
    /// </summary>
    public void SetWorldPosition()
    {
        Vector3 worldPos = new Vector3(DungeonGridPosition.x * width, DungeonGridPosition.y * height, 0);
        transform.position = worldPos;
        roomEnvironment.transform.position = worldPos;
    }
    /// <summary>
    /// Получить глобальную позицию комнаты на основной сцене.
    /// </summary>
    /// <returns></returns>
    public Vector3 GetWorldPosition()
    {
        return transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            onActiveRoomChanged.NotifyEventSubs();
            activeRoom.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activeRoom.Remove(this);
        }
    }

    private void OnDestroy()
    {
        loadedStageRooms.Remove(this);
        activeRoom.Remove(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }
}
