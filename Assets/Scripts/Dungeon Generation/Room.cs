using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private int width = 20; // Ширина комнаты.
    [SerializeField] private int height = 12; // Высота комнаты.
    [SerializeField] private GameObject roomEnvironment = null; // Объекты в комнате.
    [SerializeField] private RoomType roomTypeData = null; // Данные о типе комнаты.

    [SerializeField] private GameEvent onRoomLoaded = null; // Cобытие при загрузке комнаты.
    [SerializeField] private GameEvent onActiveRoomChanged = null; // Cобытие при смене активной комнаты.

    [SerializeField] private RoomRuntimeSet loadedStageRooms = null; // Набор комнат, загруженных на этаже подземелья.
    [SerializeField] private RoomRuntimeSet activeRoom = null; // Активная комната, где находится игрок.
    [SerializeField] private DoorRuntimeSet possibleDoors = null; // Набор всех типов дверей в игре.

    /// <summary>
    /// Позиция комнаты в сетке координат подземелья.
    /// </summary>
    public Vector2Int DungeonGridPosition { get; set; }
    /// <summary>
    /// Данные о типе комнаты.
    /// </summary>
    public RoomType RoomTypeData => roomTypeData;
    /// <summary>
    /// Присвоено ли комнате место на этаже подземелья?
    /// </summary>
    public bool Initialized { get; set; }
    /// <summary>
    /// Начальный список дверей в текущей комнате.
    /// </summary>
    private List<Door> roomDoors = new List<Door>();
    /// <summary>
    /// Список обновленных дверей в текущей комнате.
    /// </summary>
    private List<Door> correctRoomDoors = new List<Door>();

    private void Start()
    {
        Initialized = false;

        // Заполнение списка дверей в комнате.
        Door[] doors = roomEnvironment.GetComponentsInChildren<Door>();
        foreach (Door door in doors)
        {
            roomDoors.Add(door);
        }

        // Выставляем точку старта на этаже в качестве активной комнаты.
        if (roomTypeData.IsStartingPoint)
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
    /// <summary>
    /// Установить двери, соответствующие соседним комнатам (отклик на событие по окончанию загрузки всего этажа).
    /// </summary>
    public void SetupCorrectDoors()
    {
        foreach (Door door in roomDoors)
        {
            switch (door.Direction)
            {
                case Direction.Up:
                    UpdateDoor(door, GetNextRoom(Vector2Int.up));
                    break;
                case Direction.Right:
                    UpdateDoor(door, GetNextRoom(Vector2Int.right));
                    break;
                case Direction.Down:
                    UpdateDoor(door, GetNextRoom(Vector2Int.down));
                    break;
                case Direction.Left:
                    UpdateDoor(door, GetNextRoom(Vector2Int.left));
                    break;
            }
        }
    }
    /// <summary>
    /// Открывает двери в комнате, не требующие ключа.
    /// </summary>
    public void OpenKeylessDoors()
    {
        foreach (Door door in correctRoomDoors)
        {
            if (!door.DoorType.IsRequireKey)
            {
                door.Open();
            }
        }
    }
    /// <summary>
    /// Найти соседнюю комнату в заданном направлении.
    /// </summary>
    /// <param name="direction">Направление (вверх, вниз, вправо, влево).</param>
    /// <returns></returns>
    private Room GetNextRoom(Vector2Int direction)
    {
        return loadedStageRooms.Find(room => room.DungeonGridPosition == DungeonGridPosition + direction);
    }
    /// <summary>
    /// Обновить дверь в зависимости от соседней комнаты.
    /// </summary>
    /// <param name="currentDoor">Обновляемая дверь.</param>
    /// <param name="nextRoom">Соседняя комната.</param>
    private void UpdateDoor(Door currentDoor, Room nextRoom)
    {
        if (nextRoom == null)
        {
            // Убираем дверь.
            currentDoor.Remove();
        }
        else
        {
            // Сверяем приоритет дверей с соседней комнатой и меняем текущую дверь при необходимости.
            if (roomTypeData.DoorType.PlacingPriority < nextRoom.RoomTypeData.DoorType.PlacingPriority)
            {
                Door newDoor = possibleDoors.Find(door => door.DoorType == nextRoom.RoomTypeData.DoorType);
                newDoor.Direction = currentDoor.Direction;
                newDoor = Instantiate(newDoor, currentDoor.transform.position, currentDoor.transform.rotation, currentDoor.transform.parent);
                correctRoomDoors.Add(newDoor);
                Destroy(currentDoor.gameObject);
            }
            else
            {
                correctRoomDoors.Add(currentDoor);
            }
        }
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
