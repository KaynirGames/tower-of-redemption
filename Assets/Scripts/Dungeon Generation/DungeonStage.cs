using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New DungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    [Header("Текстовая информация об этаже:")]
    [SerializeField] private string _sceneNamePrefix = "Undefined";
    [SerializeField] private TranslatedText _displayName = null;
    [Header("Параметры создания этажа:")]
    [SerializeField] private int _minRouteLength = 0;
    [SerializeField] private int _maxRouteLength = 0;
    [SerializeField] private int _routeSpawnerCount = 0;
    [Header("Параметры комнат:")]
    [SerializeField] private RoomType _startRoom = null;
    [SerializeField] private RoomType _bossRoom = null;
    [SerializeField] private SpawnTable _optionalRooms = null;
    [SerializeField] private List<Door> _doorPrefabs = null;

    public string SceneNamePrefix => _sceneNamePrefix;
    public string DisplayName => _displayName.Value;

    public int MinRouteLength => _minRouteLength;
    public int MaxRouteLength => _maxRouteLength;
    public int RouteSpawnerCount => _routeSpawnerCount;

    public RoomType StartRoom => _startRoom;
    public RoomType BossRoom => _bossRoom;
    public SpawnTable OptionalRooms => _optionalRooms;
    
    public Door SwapDoor(DoorType newDoorType, Door prevDoor)
    {
        Door newDoor = Instantiate(_doorPrefabs.Find(door => door.DoorType == newDoorType),
                                   prevDoor.transform.position,
                                   prevDoor.transform.rotation,
                                   prevDoor.transform.parent);

        newDoor.SetDirection(prevDoor.Direction);

        return newDoor;
    }
}