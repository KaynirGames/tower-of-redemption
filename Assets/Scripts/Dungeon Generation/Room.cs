using KaynirGames.Pathfinding;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public static event Action<Room> OnRoomLoadComplete = delegate { };
    public static event Action<Room> OnActiveRoomChange = delegate { };

    [SerializeField] private int _width = 20;
    [SerializeField] private int _height = 12;
    [SerializeField] private RoomType _roomType = null;
    [SerializeField] private GameObject _roomEnvironment = null;
    [SerializeField] private Pathfinder _pathfinder = null;

    public Vector2Int DungeonGridPosition { get; set; }

    public RoomType RoomType => _roomType;

    private List<Door> _roomDoors = new List<Door>();

    private DungeonStage _currentStage;

    private void Start()
    {
        GameMaster.Instance.LoadedRooms.Add(this);
        OnRoomLoadComplete?.Invoke(this);

        _roomDoors.AddRange(_roomEnvironment.GetComponentsInChildren<Door>());

        DungeonStageManager.OnStageLoadComplete += PrepareRoom;
    }

    public void SetWorldPosition()
    {
        Vector3 worldPos = new Vector3(DungeonGridPosition.x * _width, DungeonGridPosition.y * _height, 0);
        transform.position = worldPos;
        _roomEnvironment.transform.position = worldPos;
        if (_pathfinder != null) _pathfinder.transform.position = worldPos;
    }

    public void OpenDoors()
    {
        foreach (Door door in _roomDoors)
        {
            door.Open();
        }
    }

    private void PrepareRoom(DungeonStage dungeonStage)
    {
        _currentStage = dungeonStage;

        SetupCorrectDoors();
        OpenDoors();
        CreatePathfindingGrid();
    }

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

    private Room GetNextRoom(Vector2Int direction)
    {
        return GameMaster.Instance.LoadedRooms.Find(room => room.DungeonGridPosition == DungeonGridPosition + direction);
    }

    private void UpdateDoor(Door door, Room nextRoom)
    {
        if (nextRoom == null)
        {
            _roomDoors.Remove(door);
            door.Remove();
        }
        else
        {
            if (_roomType.DoorType.PlacingPriority < nextRoom.RoomType.DoorType.PlacingPriority)
            {
                Door newDoor = _currentStage.SwapDoor(nextRoom.RoomType.DoorType, door);

                _roomDoors.Remove(door);
                _roomDoors.Add(newDoor);

                Destroy(door.gameObject);
            }
        }
    }

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
