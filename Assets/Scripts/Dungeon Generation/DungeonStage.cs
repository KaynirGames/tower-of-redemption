using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    [Header("Текстовая информация об этаже:")]
    [SerializeField] private string _sceneNamePrefix = "Undefined";
    [SerializeField] private TranslatedText _displayName = null;
    [Header("Настройки для создания этажа:")]
    [SerializeField] private int _minRouteLength = 0;
    [SerializeField] private int _maxRouteLength = 0;
    [SerializeField] private int _routeSpawnerCount = 0;
    [SerializeField] private GameObject _battlefieldBackground = null;
    [Header("Типы комнат на этаже:")]
    [SerializeField] private RoomType _startRoom = null;
    [SerializeField] private RoomType _bossRoom = null;
    [SerializeField] private SpawnTable _optionalRooms = null;

    public string SceneNamePrefix => _sceneNamePrefix;
    public string DisplayName => _displayName.Value;

    public int MinRouteLength => _minRouteLength;
    public int MaxRouteLength => _maxRouteLength;
    public int RouteSpawnerCount => _routeSpawnerCount;
    public GameObject BattlefieldBackground => _battlefieldBackground;

    public RoomType StartRoom => _startRoom;
    public RoomType BossRoom => _bossRoom;
    public SpawnTable OptionalRooms => _optionalRooms;
}