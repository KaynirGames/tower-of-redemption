using UnityEngine;

[CreateAssetMenu(fileName = "NewDungeonStage", menuName = "Scriptable Objects/Dungeon Generation/Dungeon Stage")]
public class DungeonStage : ScriptableObject
{
    [SerializeField] private string _name = "Undefined";
    [SerializeField] private int _minRouteLength = 0;
    [SerializeField] private int _maxRouteLength = 0;
    [SerializeField] private int _routeSpawnerCount = 0;
    [SerializeField] private RoomType _startRoom = null;
    [SerializeField] private RoomType _bossRoom = null;
    [SerializeField] private SpawnTable _optionalRooms = null;
    /// <summary>
    /// Название этажа подземелья.
    /// </summary>
    public string Name => _name;
    /// <summary>
    /// Минимальная длина коридора (количество комнат) на этаже.
    /// </summary>
    public int MinRouteLength => _minRouteLength;
    /// <summary>
    /// Максимальная длина коридора (количество комнат) на этаже.
    /// </summary>
    public int MaxRouteLength => _maxRouteLength;
    /// <summary>
    /// Количество создателей коридоров, распределяющих их на этаже.
    /// </summary>
    public int RouteSpawnerCount => _routeSpawnerCount;
    /// <summary>
    /// Данные об обязательной стартовой комнате на этаже.
    /// </summary>
    public RoomType StartRoom => _startRoom;
    /// <summary>
    /// Данные об обязательной босс-комнате на этаже.
    /// </summary>
    public RoomType BossRoom => _bossRoom;
    /// <summary>
    /// Таблица с вероятностями появления опциональных комнат.
    /// </summary>
    public SpawnTable OptionalRooms => _optionalRooms;
}
