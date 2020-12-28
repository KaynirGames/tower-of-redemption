using KaynirGames.Pathfinding;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    public delegate void OnRoomStatusChange(Room room);

    public static event OnRoomStatusChange OnActiveRoomChange = delegate { };

    public static List<Room> LoadedRooms { get; private set; } = new List<Room>();
    public static Room ActiveRoom { get; private set; }

    [SerializeField] private int _width = 20;
    [SerializeField] private int _height = 12;
    [SerializeField] private RoomType _roomType = null;
    [SerializeField] private GameObject _doorsParent = null;
    [SerializeField] private Pathfinder _pathfinder = null;
    [SerializeField] private GameObject _parentToDisableInBattle = null;
    [SerializeField] private ObjectSpawner _enemySpawner = null;
    [SerializeField] private Vector2Int _gridPosition = Vector2Int.zero;
    [SerializeField] private bool _isClear = false;
    [SerializeField] private UnityEvent _onRoomClear = null;

    public RoomType RoomType => _roomType;

    private List<Door> _doors = new List<Door>();

    private void Awake()
    {
        _doors.AddRange(_doorsParent.GetComponentsInChildren<Door>());
        LoadedRooms.Add(this);
    }

    public void SetRoomPosition(Vector2Int gridPosition)
    {
        _gridPosition = gridPosition;
        transform.position = new Vector3(gridPosition.x * _width, gridPosition.y * _height, 0);
    }

    public void ToggleBattleObstructingObjects(bool enable)
    {
        _parentToDisableInBattle.SetActive(enable);
    }

    public void PrepareRoom()
    {
        SetupCorrectDoors();
        SetupPathfinder();
        if (_isClear)
        {
            SetRoomStatus(true);
        }
    }

    public void SetRoomStatus(bool isClear)
    {
        _isClear = isClear;
        ToggleDoors(isClear);
        _onRoomClear?.Invoke();
    }

    private void ToggleDoors(bool open)
    {
        _doors.ForEach(door => door.ToggleDoor(open));
    }

    private void SetupCorrectDoors()
    {
        for (int i = _doors.Count - 1; i >= 0; i--)
        {
            switch (_doors[i].Direction)
            {
                case Direction.Up:
                    UpdateDoor(_doors[i], GetNextRoom(Vector2Int.up));
                    break;
                case Direction.Right:
                    UpdateDoor(_doors[i], GetNextRoom(Vector2Int.right));
                    break;
                case Direction.Down:
                    UpdateDoor(_doors[i], GetNextRoom(Vector2Int.down));
                    break;
                case Direction.Left:
                    UpdateDoor(_doors[i], GetNextRoom(Vector2Int.left));
                    break;
            }
        }
    }

    private Room GetNextRoom(Vector2Int direction)
    {
        return LoadedRooms.Find(room => room._gridPosition == _gridPosition + direction);
    }

    private void UpdateDoor(Door door, Room nextRoom)
    {
        if (nextRoom == null)
        {
            _doors.Remove(door);
            door.Remove();
        }
        else
        {
            if (_roomType.DoorType.PlacingPriority > nextRoom.RoomType.DoorType.PlacingPriority)
            {
                door.CreateDoorGFX(_roomType.DoorType);
            }
            else
            {
                door.CreateDoorGFX(nextRoom.RoomType.DoorType);
            }
        }
    }

    private void SetupPathfinder()
    {
        if (_pathfinder != null)
        {
            _pathfinder.Initialize();
            _pathfinder.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
        {
            _pathfinder?.gameObject.SetActive(true);
            OnActiveRoomChange.Invoke(this);
            ActiveRoom = this;

            if (!_isClear)
            {
                ToggleDoors(false);

                if (_enemySpawner != null)
                {
                    _enemySpawner.SpawnObjects();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerCharacter>() != null)
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
