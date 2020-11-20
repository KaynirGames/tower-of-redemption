using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New DungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    [Header("Текстовая информация об этаже:")]
    [SerializeField] private string _sceneNamePrefix = "Undefined";
    [SerializeField] private LocalizedString _displayName = null;
    [Header("Параметры создания этажа:")]
    [SerializeField] private int _minRouteLength = 0;
    [SerializeField] private int _maxRouteLength = 0;
    [SerializeField] private int _routeSpawnerCount = 0;
    [Header("Параметры комнат:")]
    [SerializeField] private RoomType _startRoom = null;
    [SerializeField] private RoomType _bossRoom = null;
    [SerializeField] private SpawnTable _optionalRooms = null;

    public string SceneNamePrefix => _sceneNamePrefix;
    public string DisplayName => _displayName.GetLocalizedString().Result;

    public int MinRouteLength => _minRouteLength;
    public int MaxRouteLength => _maxRouteLength;
    public int RouteSpawnerCount => _routeSpawnerCount;

    public RoomType StartRoom => _startRoom;
    public RoomType BossRoom => _bossRoom;
    public SpawnTable OptionalRooms => _optionalRooms;
}