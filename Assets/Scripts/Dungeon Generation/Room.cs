using KaynirGames.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    /// <summary>
    /// Событие на завершение загрузки комнаты.
    /// </summary>
    public static event Action<Room> OnRoomLoadComplete = delegate { };
    /// <summary>
    /// Событие на смену активной комнаты.
    /// </summary>
    public static event Action<Room> OnActiveRoomChange = delegate { };

    [SerializeField] private int _width = 20; // Ширина комнаты.
    [SerializeField] private int _height = 12; // Высота комнаты.
    [SerializeField] private RoomType _roomTypeData = null; // Данные о типе комнаты.
    [SerializeField] private GameObject _roomEnvironment = null; // Объекты в комнате.
    [SerializeField] private Pathfinder _pathfinder = null; // Искатель оптимального пути для перемещения ИИ.
    /// <summary>
    /// Позиция комнаты в сетке координат подземелья.
    /// </summary>
    public Vector2Int DungeonGridPosition { get; set; }
    /// <summary>
    /// Данные о типе комнаты.
    /// </summary>
    public RoomType RoomTypeData => _roomTypeData;

    private List<Door> _roomDoors = new List<Door>(); // Список дверей в текущей комнате.

    private void Start()
    {
        GameMaster.Instance.LoadedRooms.Add(this);
        OnRoomLoadComplete?.Invoke(this);

        // Заполняем список дверей в комнате.
        _roomDoors.AddRange(_roomEnvironment.GetComponentsInChildren<Door>());

        DungeonStageManager.OnStageLoadComplete += PrepareRoom;
    }
    /// <summary>
    /// Установить глобальную позицию комнаты на основной сцене.
    /// </summary>
    public void SetWorldPosition()
    {
        Vector3 worldPos = new Vector3(DungeonGridPosition.x * _width, DungeonGridPosition.y * _height, 0);
        transform.position = worldPos;
        _roomEnvironment.transform.position = worldPos;
        if (_pathfinder != null) _pathfinder.transform.position = worldPos;
    }
    /// <summary>
    /// Открывает двери в комнате.
    /// </summary>
    public void OpenDoors()
    {
        foreach (Door door in _roomDoors)
        {
            door.Open();
        }
    }
    /// <summary>
    /// Подготовить комнату после загрузки этажа.
    /// </summary>
    private void PrepareRoom()
    {
        SetupCorrectDoors();
        OpenDoors();
        CreatePathfindingGrid();
    }
    /// <summary>
    /// Установить двери, соответствующие соседним комнатам.
    /// </summary>
    private void SetupCorrectDoors()
    {
        for (int i = _roomDoors.Count - 1; i >= 0; i--)
        {
            switch (_roomDoors[i].Direction)
            {
                case Direction.Up:
                    UpdateDoor(_roomDoors[i], GetNextRoom(Vector2Int.up));
                    break;
                case Direction.Right:
                    UpdateDoor(_roomDoors[i], GetNextRoom(Vector2Int.right));
                    break;
                case Direction.Down:
                    UpdateDoor(_roomDoors[i], GetNextRoom(Vector2Int.down));
                    break;
                case Direction.Left:
                    UpdateDoor(_roomDoors[i], GetNextRoom(Vector2Int.left));
                    break;
            }
        }
    }
    /// <summary>
    /// Найти соседнюю комнату в заданном направлении.
    /// </summary>
    private Room GetNextRoom(Vector2Int direction)
    {
        return GameMaster.Instance.LoadedRooms.Find(room => room.DungeonGridPosition == DungeonGridPosition + direction);
    }
    /// <summary>
    /// Обновить дверь согласно соседней комнаты.
    /// </summary>
    private void UpdateDoor(Door door, Room nextRoom)
    {
        if (nextRoom == null)
        {
            // Убираем дверь.
            _roomDoors.Remove(door);
            door.Remove();
        }
        else
        {
            // Сверяем приоритет дверей с соседней комнатой и меняем текущую дверь при необходимости.
            if (_roomTypeData.DoorType.PlacingPriority < nextRoom.RoomTypeData.DoorType.PlacingPriority)
            {
                _roomDoors.Remove(door);
                _roomDoors.Add(door.Swap(nextRoom.RoomTypeData.DoorType));
                Destroy(door.gameObject);
            }
        }
    }
    /// <summary>
    /// Иницилизировать систему поиска пути для комнаты.
    /// </summary>
    private void CreatePathfindingGrid()
    {
        if (_pathfinder != null)
        {
            _pathfinder.Initialize();
            _pathfinder.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _pathfinder?.gameObject.SetActive(true);
            OnActiveRoomChange?.Invoke(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _pathfinder?.gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(_width, _height, 0));
    }
}
